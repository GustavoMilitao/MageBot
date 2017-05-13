using System;
using System.Collections.Generic;

using System.Net;
using System.Net.Sockets;

using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using BlueSheep.Protocol.Messages;
using BlueSheep.Core.Frame;
using BlueSheep.Util.Text.Log;
using BlueSheep.Util.IO;
using BlueSheep.Protocol.Messages.Common.Basic;
using BlueSheep.Engine.Network;
using BlueSheep.Util.Enums.Internal;
using BotForgeAPI.Network.Messages;
using BotForgeAPI.Protocol.Messages;

namespace BlueSheep.Engine.Network
{
    #region Enums
    public enum SocketState
    {
        Closed,
        Closing,
        Connected,
        Connecting,
        Listening,
    }
    #endregion

    #region Event Args
    public class NetSocketConnectedEventArgs : EventArgs
    {
        public IPAddress SourceIP;
        public NetSocketConnectedEventArgs(IPAddress ip)
        {
            SourceIP = ip;
        }
    }

    public class NetSocketDisconnectedEventArgs : EventArgs
    {
        public string Reason;
        public NetSocketDisconnectedEventArgs(string reason)
        {
            Reason = reason;
        }
    }

    public class NetSockStateChangedEventArgs : EventArgs
    {
        public SocketState NewState;
        public SocketState PrevState;
        public NetSockStateChangedEventArgs(SocketState newState, SocketState prevState)
        {
            NewState = newState;
            PrevState = prevState;
        }
    }

    public class NetSockDataArrivalEventArgs : EventArgs
    {
        public byte[] Data;
        public NetSockDataArrivalEventArgs(byte[] data)
        {
            Data = data;
        }
    }

    public class NetSockErrorReceivedEventArgs : EventArgs
    {
        public string Function;
        public Exception Exception;
        public NetSockErrorReceivedEventArgs(string function, Exception ex)
        {
            Function = function;
            Exception = ex;
        }
    }

    public class NetSockConnectionRequestEventArgs : EventArgs
    {
        public Socket Client;
        public NetSockConnectionRequestEventArgs(Socket client)
        {
            Client = client;
        }
    }
    #endregion

    #region Socket Classes
    public abstract class NetBase
    {
        #region Fields
        /// <summary>Current socket state</summary>
        protected SocketState state = SocketState.Closed;
        /// <summary>The socket object, obviously</summary>
        protected Socket socket;

        /// <summary>Keep track of when data is being sent</summary>
        protected bool isSending = false;

        /// <summary>Queue of objects to be sent out</summary>
        protected Queue<byte[]> sendBuffer = new Queue<byte[]>();

        /// <summary>Store incoming bytes to be processed</summary>
        protected byte[] byteBuffer = new byte[8192];

        /// <summary>Position of the bom header in the rxBuffer</summary>
        protected int rxHeaderIndex = -1;
        /// <summary>Expected length of the message from the bom header</summary>
        protected int rxBodyLen = -1;
        /// <summary>Buffer of received data</summary>
        protected MemoryStream rxBuffer = new MemoryStream();

        /// <summary>TCP inactivity before sending keep-alive packet (ms)</summary>
        protected uint KeepAliveInactivity = 500;
        /// <summary>Interval to send keep-alive packet if acknowledgement was not received (ms)</summary>
        protected uint KeepAliveInterval = 100;

        /// <summary>Threaded timer checks if socket is busted</summary>
        protected Timer connectionTimer;
        /// <summary>Interval for socket checks (ms)</summary>
        protected int ConnectionCheckInterval = 1000;
        #endregion

        #region Public Properties
        /// <summary>Current state of the socket</summary>
        public SocketState State { get { return state; } }

        /// <summary>Port the socket control is listening on.</summary>
        public int LocalPort
        {
            get
            {
                try
                {
                    return ((IPEndPoint)socket.LocalEndPoint).Port;
                }
                catch
                {
                    return -1;
                }
            }
        }

        /// <summary>IP address enumeration for local computer</summary>
        public static string[] LocalIP
        {
            get
            {
                IPHostEntry h = Dns.GetHostEntry(Dns.GetHostName());
                List<string> s = new List<string>(h.AddressList.Length);
                foreach (IPAddress i in h.AddressList)
                    s.Add(i.ToString());
                return s.ToArray();
            }
        }
        #endregion

        #region Events
        /// <summary>Socket is connected</summary>
        public event EventHandler<NetSocketConnectedEventArgs> Connected;
        /// <summary>Socket connection closed</summary>
        public event EventHandler<NetSocketDisconnectedEventArgs> Disconnected;
        /// <summary>Socket state has changed</summary>
        /// <remarks>This has the ability to fire very rapidly during connection / disconnection.</remarks>
        public event EventHandler<NetSockStateChangedEventArgs> StateChanged;
        /// <summary>Recived a new object</summary>
        public event EventHandler<NetSockDataArrivalEventArgs> DataArrived;
        /// <summary>An error has occurred</summary>
        public event EventHandler<NetSockErrorReceivedEventArgs> ErrorReceived;
        #endregion

        #region Constructor
        /// <summary>Base constructor sets up buffer and timer</summary>
        public NetBase()
        {
            connectionTimer = new Timer(
                new TimerCallback(connectedTimerCallback),
                null, Timeout.Infinite, Timeout.Infinite);
        }
        #endregion

        #region Send
        /// <summary>Send data</summary>
        /// <param name="bytes">Bytes to send</param>
        public void Send(byte[] data)
        {
            try
            {
                if (data == null)
                    throw new NullReferenceException("data cannot be null");
                else if (data.Length == 0)
                    throw new NullReferenceException("data cannot be empty");
                else
                {
                    lock (sendBuffer)
                    {
                        sendBuffer.Enqueue(data);
                    }

                    if (!isSending)
                    {
                        isSending = true;
                        SendNextQueued();
                    }
                }
            }
            catch (Exception ex)
            {
                OnErrorReceived("Send", ex);
            }
        }

        /// <summary>Send data for real</summary>
        private void SendNextQueued()
        {
            try
            {
                if (IsConnected(socket))
                {
                    List<ArraySegment<byte>> send = new List<ArraySegment<byte>>(3);
                    int length = 0;
                    lock (sendBuffer)
                    {
                        if (sendBuffer.Count == 0)
                        {
                            isSending = false;
                            return; // nothing more to send
                        }

                        byte[] data = sendBuffer.Dequeue();
                        send.Add(new ArraySegment<byte>(data));

                        length = data.Length;
                    }
                    socket.BeginSend(send, SocketFlags.None, new AsyncCallback(SendCallback), socket);
                }
                else
                {
                    Close("Lost sync with server. Try to reconnect.");
                }
            }
            catch (Exception ex)
            {
                OnErrorReceived("Sending", ex);
            }
        }

        /// <summary>Callback for BeginSend</summary>
        /// <param name="ar"></param>
        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket sock = (Socket)ar.AsyncState;
                int didSend = sock.EndSend(ar);

                if (socket != sock)
                {
                    Close("Async Connect Socket mismatched");
                    return;
                }

                SendNextQueued();
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.ConnectionReset)
                    Close("Remote Socket Closed");
                else
                    throw;
            }
            catch (Exception ex)
            {
                Close("Socket Send Exception");
                OnErrorReceived("Socket Send", ex);
            }
        }
        #endregion

        #region Close
        /// <summary>Disconnect the socket</summary>
        /// <param name="reason"></param>
        public void Close(string reason)
        {
            try
            {
                if (state == SocketState.Closing || state == SocketState.Closed)
                    return; // already closing/closed

                OnChangeState(SocketState.Closing);

                if (socket != null)
                {
                    socket.Close();
                    socket = null;
                }
            }
            catch (Exception ex)
            {
                OnErrorReceived("Close", ex);
            }

            try
            {
                //lock (this.rxBuffer)
                //{
                rxBuffer.SetLength(0);
                //}
                //lock (this.sendBuffer)
                //{
                sendBuffer.Clear();
                isSending = false;
                //}
                OnChangeState(SocketState.Closed);
                if (Disconnected != null)
                    Disconnected(this, new NetSocketDisconnectedEventArgs(reason));
            }
            catch (Exception ex)
            {
                OnErrorReceived("Close Cleanup", ex);
            }
        }
        #endregion

        #region Receive
        /// <summary>Receive data asynchronously</summary>
        protected void Receive()
        {
            try
            {
                socket.BeginReceive(byteBuffer, 0, byteBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            }
            catch (Exception ex)
            {
                OnErrorReceived("Receive", ex);
            }
        }

        /// <summary>Callback for BeginReceive</summary>
        /// <param name="ar"></param>
        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                Socket sock = (Socket)ar.AsyncState;
                int size = sock.EndReceive(ar);
                if (socket != sock)
                {
                    Close("Async Receive Socket mismatched");
                    return;
                }
                if (size < 1)
                {
                    //this.Close("No Bytes Received");
                    return;
                }

                lock (rxBuffer)
                {
                    // put at the end for safe writing
                    rxBuffer.Position = rxBuffer.Length;
                    rxBuffer.Write(byteBuffer, 0, size);

                    bool more = false;
                    do
                    {
                        try
                        {
                            //this.rxBuffer.Position = this.rxHeaderIndex + this.bomBytes.Count + sizeof(int);
                            rxBuffer.Position = 0;
                            byte[] data = new byte[rxBuffer.Length];
                            rxBuffer.Read(data, 0, data.Length);
                            if (DataArrived != null)
                                DataArrived(this, new NetSockDataArrivalEventArgs(data));
                        }
                        catch (Exception ex)
                        {
                            OnErrorReceived("Receiving", ex);
                        }

                        if (rxBuffer.Position == rxBuffer.Length)
                        {
                            // no bytes left
                            // just resize buffer
                            rxBuffer.SetLength(0);
                            rxBuffer.Capacity = byteBuffer.Length;
                            more = false;
                        }
                        else
                        {
                            // leftover bytes after current message
                            // copy these bytes to the beginning of the rxBuffer
                            CopyBack();
                            more = true;
                        }

                        // reset header info
                        rxHeaderIndex = -1;
                        rxBodyLen = -1;

                    } while (more);
                }
                if (socket != null)
                    socket.BeginReceive(byteBuffer, 0, byteBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            }
            catch (ObjectDisposedException)
            {
                return; // socket disposed, let it die quietly
            }
            catch (SocketException ex)
            {
                if (ex.SocketErrorCode == SocketError.ConnectionReset)
                    Close("Remote Socket Closed");
                else
                    Close("Unknown error. Code : " + ex.ErrorCode);
            }
            catch (Exception ex)
            {
                Close("Socket Receive Exception");
                OnErrorReceived("Socket Receive", ex);
            }
        }

        /// <summary>
        /// Copies the stuff after the current position, back to the start of the stream,
        /// resizes the stream to only include the new content, and
        /// limits the capacity to length + another buffer.
        /// </summary>
        private void CopyBack()
        {
            int count;
            long readPos = rxBuffer.Position;
            long writePos = 0;
            do
            {
                count = rxBuffer.Read(byteBuffer, 0, byteBuffer.Length);
                readPos = rxBuffer.Position;
                rxBuffer.Position = writePos;
                rxBuffer.Write(byteBuffer, 0, count);
                writePos = rxBuffer.Position;
                rxBuffer.Position = readPos;
            }
            while (count > 0);
            rxBuffer.SetLength(writePos);
            rxBuffer.Capacity = (int)rxBuffer.Length + byteBuffer.Length;
        }

        /// <summary>
        /// Return if we are connected or not
        /// </summary>
        private bool IsConnected(Socket socket)
        {
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException) { return false; }
        }

        /// <summary>Find first position the specified byte within the stream, or -1 if not found</summary>
        /// <param name="ms"></param>
        /// <param name="find"></param>
        /// <returns></returns>
        private int IndexOfByteInStream(MemoryStream ms, byte find)
        {
            int b;
            do
            {
                b = ms.ReadByte();
            } while (b > -1 && b != find);

            if (b == -1)
                return -1;
            else
                return (int)ms.Position - 1; // position is +1 byte after the byte we want
        }

        /// <summary>Find first position the specified bytes within the stream, or -1 if not found</summary>
        /// <param name="ms"></param>
        /// <param name="find"></param>
        /// <returns></returns>
        private int IndexOfBytesInStream(MemoryStream ms, byte[] find)
        {
            int index;
            do
            {
                index = IndexOfByteInStream(ms, find[0]);

                if (index > -1)
                {
                    bool found = true;
                    for (int i = 1; i < find.Length; i++)
                    {
                        if (find[i] != ms.ReadByte())
                        {
                            found = false;
                            ms.Position = index + 1;
                            break;
                        }
                    }
                    if (found)
                        return index;
                }
            } while (index > -1);
            return -1;
        }
        #endregion

        #region OnEvents
        protected void OnErrorReceived(string function, Exception ex)
        {
            if (ErrorReceived != null)
                ErrorReceived(this, new NetSockErrorReceivedEventArgs(function, ex));
        }

        protected void OnConnected(Socket sock)
        {
            if (Connected != null)
                Connected(this, new NetSocketConnectedEventArgs(((IPEndPoint)sock.RemoteEndPoint).Address));
        }

        protected void OnChangeState(SocketState newState)
        {
            SocketState prev = state;
            state = newState;
            if (StateChanged != null)
                StateChanged(this, new NetSockStateChangedEventArgs(state, prev));

            if (state == SocketState.Connected)
                connectionTimer.Change(0, ConnectionCheckInterval);
            else if (state == SocketState.Closed)
                connectionTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }
        #endregion

        #region Keep-alives
        /*
		 * Note about usage of keep-alives
		 * The TCP protocol does not successfully detect "abnormal" socket disconnects at both
		 * the client and server end. These are disconnects due to a computer crash, cable 
		 * disconnect, or other failure. The keep-alive mechanism built into the TCP socket can
		 * detect these disconnects by essentially sending null data packets (header only) and
		 * waiting for acks.
		 */

        /// <summary>Structure for settings keep-alive bytes</summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct tcp_keepalive
        {
            /// <summary>1 = on, 0 = off</summary>
            public uint onoff;
            /// <summary>TCP inactivity before sending keep-alive packet (ms)</summary>
            public uint keepalivetime;
            /// <summary>Interval to send keep-alive packet if acknowledgement was not received (ms)</summary>
            public uint keepaliveinterval;
        }

        /// <summary>Set up the socket to use TCP keep alive messages</summary>
        protected void SetKeepAlive()
        {
            try
            {
                tcp_keepalive sioKeepAliveVals = new tcp_keepalive();
                sioKeepAliveVals.onoff = 1; // 1 to enable 0 to disable
                sioKeepAliveVals.keepalivetime = KeepAliveInactivity;
                sioKeepAliveVals.keepaliveinterval = KeepAliveInterval;

                IntPtr p = Marshal.AllocHGlobal(Marshal.SizeOf(sioKeepAliveVals));
                Marshal.StructureToPtr(sioKeepAliveVals, p, true);
                byte[] inBytes = new byte[Marshal.SizeOf(sioKeepAliveVals)];
                Marshal.Copy(p, inBytes, 0, inBytes.Length);
                Marshal.FreeHGlobal(p);

                byte[] outBytes = BitConverter.GetBytes(0);
                if (IsConnected(socket))
                    socket.IOControl(IOControlCode.KeepAliveValues, inBytes, outBytes);
                else
                    Close("Currently disconnected. Try to reconnect.");
            }
            catch (Exception ex)
            {
                OnErrorReceived("Keep Alive", ex);
            }
        }
        #endregion

        #region Connection Sanity Check
        private void connectedTimerCallback(object sender)
        {
            try
            {
                if (state == SocketState.Connected &&
                    (socket == null || !socket.Connected))
                    Close("Connect Timer");
            }
            catch (Exception ex)
            {
                OnErrorReceived("ConnectTimer", ex);
                Close("Connect Timer Exception");
            }
        }
        #endregion
    }

    public class NetServer : NetBase
    {
        #region Events
        /// <summary>A socket has requested a connection</summary>
        public event EventHandler<NetSockConnectionRequestEventArgs> ConnectionRequested;
        #endregion

        #region Listen
        /// <summary>Listen for incoming connections</summary>
        /// <param name="port">Port to listen on</param>
        public void Listen(int port)
        {
            try
            {
                if (socket != null)
                {
                    try
                    {
                        socket.Close();
                    }
                    catch { }; // ignore problems with old socket
                }
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, port);
                socket.Bind(ipLocal);
                socket.Listen(1);
                socket.BeginAccept(new AsyncCallback(AcceptCallback), socket);
                OnChangeState(SocketState.Listening);
            }
            catch (Exception ex)
            {
                OnErrorReceived("Listen", ex);
            }
        }

        /// <summary>Callback for BeginAccept</summary>
        /// <param name="ar"></param>
        private void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                Socket listener = (Socket)ar.AsyncState;
                Socket sock = listener.EndAccept(ar);

                if (state == SocketState.Listening)
                {
                    if (socket != listener)
                    {
                        Close("Async Listen Socket mismatched");
                        return;
                    }

                    if (ConnectionRequested != null)
                        ConnectionRequested(this, new NetSockConnectionRequestEventArgs(sock));
                }

                if (state == SocketState.Listening)
                    socket.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                else
                {
                    try
                    {
                        listener.Close();
                    }
                    catch (Exception ex)
                    {
                        OnErrorReceived("Close Listen Socket", ex);
                    }
                }
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            catch (SocketException ex)
            {
                Close("Listen Socket Exception");
                OnErrorReceived("Listen Socket", ex);
            }
            catch (Exception ex)
            {
                OnErrorReceived("Listen Socket", ex);
            }
        }
        #endregion

        #region Accept
        /// <summary>Accept the connection request</summary>
        /// <param name="client">Client socket to accept</param>
        public void Accept(Socket client)
        {
            try
            {
                if (state != SocketState.Listening)
                    throw new Exception("Cannot accept socket is " + state.ToString());

                if (socket != null)
                {
                    try
                    {
                        socket.Close(); // close listening socket
                    }
                    catch { } // don't care if this fails
                }

                socket = client;

                socket.ReceiveBufferSize = byteBuffer.Length;
                socket.SendBufferSize = byteBuffer.Length;

                SetKeepAlive();

                OnChangeState(SocketState.Connected);
                OnConnected(socket);

                Receive();
            }
            catch (Exception ex)
            {
                OnErrorReceived("Accept", ex);
            }
        }
        #endregion
    }

    public class NetClient : NetBase
    {
        #region Constructor
        public NetClient() : base() { }
        #endregion

        #region Connect
        /// <summary>Connect to the computer specified by Host and Port</summary>
        public void Connect(IPEndPoint endPoint)
        {
            if (state == SocketState.Connected)
                return; // already connecting to something

            try
            {
                if (state != SocketState.Closed)
                    throw new Exception("Cannot connect socket is " + state.ToString());

                OnChangeState(SocketState.Connecting);

                if (socket == null)
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.BeginConnect(endPoint, new AsyncCallback(ConnectCallback), socket);
            }
            catch (Exception ex)
            {
                OnErrorReceived("Connect", ex);
                Close("Connect Exception");
            }
        }

        /// <summary>Callback for BeginConnect</summary>
        /// <param name="ar"></param>
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket sock = (Socket)ar.AsyncState;
                sock.EndConnect(ar);

                if (socket != sock)
                {
                    Close("Async Connect Socket mismatched");
                    return;
                }

                if (state != SocketState.Connecting)
                    throw new Exception("Cannot connect socket is " + state.ToString());

                socket.ReceiveBufferSize = byteBuffer.Length;
                socket.SendBufferSize = byteBuffer.Length;

                SetKeepAlive();

                OnChangeState(SocketState.Connected);
                OnConnected(socket);

                Receive();
            }
            catch (Exception ex)
            {
                Close("Socket Connect Exception");
                OnErrorReceived("Socket Connect", ex);
            }
        }
        #endregion
    }
    #endregion
}

namespace BlueSheep.Core.Network
{
    public class SocketManager
    {
        #region Fields
        private MessageInformations m_MessageInformations;
        private Account.Account account;
        private NetClient client;
        private bool m_IsChangingServer;
        public SocketState State;
        public bool IsChangingServer
        {
            get { return m_IsChangingServer; }
            set { m_IsChangingServer = value; }
        }
        private List<string> DisconnectReasons = new List<string>() { "Alerte au modo ! Alerte au modo !", "Try Reconnect.", "Wait before next meal.", "Changing server.", "User forced." };

        #region MITM
        private NetServer server;
        #endregion
        #endregion

        #region Constructors
        public SocketManager(Account.Account account)
        {
            this.account = account;
            if (account != null)
                account.LatencyFrame = new LatencyFrame(account);
            client = new NetClient();
            client.Connected += new EventHandler<NetSocketConnectedEventArgs>(client_Connected);
            client.DataArrived += new EventHandler<NetSockDataArrivalEventArgs>(client_DataArrived);
            client.Disconnected += new EventHandler<NetSocketDisconnectedEventArgs>(client_Disconnected);
            client.ErrorReceived += new EventHandler<NetSockErrorReceivedEventArgs>(client_ErrorReceived);
            client.StateChanged += new EventHandler<NetSockStateChangedEventArgs>(client_StateChanged);
        }

        #endregion

        /// <summary>
        /// Connect the socket.
        /// </summary>
        public void Connect(ConnectionInformations connectionInformations)
        {
            //if (m_RawSocket != null && !m_RawSocket.Connected)
            //    RawConnect();
            m_MessageInformations = new MessageInformations(account);
            if (m_IsChangingServer)
            {
                client.Close("Changing server.");
                client = new NetClient();
                client.Connected += new EventHandler<NetSocketConnectedEventArgs>(client_Connected);
                client.DataArrived += new EventHandler<NetSockDataArrivalEventArgs>(client_DataArrived);
                client.Disconnected += new EventHandler<NetSocketDisconnectedEventArgs>(client_Disconnected);
                client.ErrorReceived += new EventHandler<NetSockErrorReceivedEventArgs>(client_ErrorReceived);
                client.StateChanged += new EventHandler<NetSockStateChangedEventArgs>(client_StateChanged);
            }
            try
            {
                client.Connect(new IPEndPoint(IPAddress.Parse(connectionInformations.Address), connectionInformations.Port));
            }
            catch (SocketException sockEx)
            {
                account.Log(new ErrorTextInformation("[Socket Exception] " + sockEx.Message), 0);
                account.TryReconnect(10);
            }
        }

        /// <summary>
        /// Initialize the MITM server.
        /// </summary>
        public void InitMITM()
        {
            server = new NetServer();
            server.Connected += new EventHandler<NetSocketConnectedEventArgs>(server_Connected);
            server.ConnectionRequested += new EventHandler<NetSockConnectionRequestEventArgs>(server_ConnectionRequested);
            server.DataArrived += new EventHandler<NetSockDataArrivalEventArgs>(server_DataArrived);
            server.Disconnected += new EventHandler<NetSocketDisconnectedEventArgs>(server_Disconnected);
            server.ErrorReceived += new EventHandler<NetSockErrorReceivedEventArgs>(server_ErrorReceived);
            server.StateChanged += new EventHandler<NetSockStateChangedEventArgs>(server_StateChanged);
            ListenDofus();
        }

        /// <summary>
        /// Disconnect the socket and set the reason as "User forced".
        /// </summary>
        public void DisconnectFromGUI()
        {
            client.Close("User forced.");
        }

        /// <summary>
        /// Disconnect the socket and set the specified reason.
        /// </summary>
        public void Disconnect(string reason)
        {
            client.Close(reason);
        }


        /// <summary>
        /// Send the byte array.
        /// </summary>
        public void Send(byte[] content)
        {
            client.Send(content);
        }

        /// <summary>
        /// Serialize and pack the message, and send it.
        /// </summary>
        public void Send(Message msg)
        {
            using (BigEndianWriter writer = new BigEndianWriter())
            {
                //msg.Serialize(writer);
                //MessagePackaging pack = new MessagePackaging(writer);
                //pack.Pack((int)msg.MessageID);
                msg.Pack(writer);
                account.SocketManager.Send(writer.Content);
                account.Log(new DebugTextInformation("[SND] " + msg.MessageID), 0);
            }
        }


        #region Private Methods
        private void client_StateChanged(object sender, NetSockStateChangedEventArgs e)
        {
            switch (e.NewState)
            {
                case SocketState.Closed:
                    account.SetStatus(Status.Disconnected);
                    break;
                case SocketState.Connected:
                    account.SetStatus(Status.None);
                    break;
            }
            State = e.NewState;
        }

        private void client_ErrorReceived(object sender, NetSockErrorReceivedEventArgs e)
        {
            if (e.Exception.GetType() == typeof(System.Net.Sockets.SocketException))
            {
                System.Net.Sockets.SocketException s = (System.Net.Sockets.SocketException)e.Exception;
                account.Log(new ErrorTextInformation("Error: " + e.Function + " - " + s.SocketErrorCode.ToString() + "\r\n" + s.ToString()), 0);
            }
            else
                account.Log(new ErrorTextInformation("Error: " + e.Function + "\r\n" + e.Exception.ToString()), 4);
        }

        private void client_Disconnected(object sender, NetSocketDisconnectedEventArgs e)
        {
            account.Log(new ConnectionTextInformation("Disconnected: " + e.Reason), 2);
            if (!DisconnectReasons.Contains(e.Reason))
                account.TryReconnect(10);
        }

        private void client_DataArrived(object sender, NetSockDataArrivalEventArgs e)
        {
            m_MessageInformations.ParseBuffer(e.Data);
            account.LatencyFrame.UpdateLatency();
        }

        private void client_Connected(object sender, NetSocketConnectedEventArgs e)
        {
            account.Log(new ConnectionTextInformation("Connected: " + e.SourceIP), 0);
            Send(new BasicPingMessage());
        }

        #region MITM

        #region Public Methods
        public void SendToDofusClient(byte[] p)
        {
            server.Send(p);
        }

        public void DisconnectServer(string reason)
        {
            server.Close(reason);
        }

        public void ListenDofus()
        {
            account.Log(new ConnectionTextInformation("Listening on port 5555"), 0);
            server.Listen(5555);
        }
        #endregion

        #region Private Methods
        private void server_StateChanged(object sender, NetSockStateChangedEventArgs e)
        {
            account.Log(new ConnectionTextInformation("New server state : " + e.NewState), 0);
        }

        private void server_ErrorReceived(object sender, NetSockErrorReceivedEventArgs e)
        {
            if (e.Exception.GetType() == typeof(System.Net.Sockets.SocketException))
            {
                System.Net.Sockets.SocketException s = (System.Net.Sockets.SocketException)e.Exception;
                account.Log(new ErrorTextInformation("Error: " + e.Function + " - " + s.SocketErrorCode.ToString() + "\r\n" + s.ToString()), 0);
            }
            else
                account.Log(new ErrorTextInformation("Error: " + e.Function + "\r\n" + e.Exception.ToString()), 0);
        }

        private void server_Disconnected(object sender, NetSocketDisconnectedEventArgs e)
        {
            account.Log(new ConnectionTextInformation("Disconnected: " + e.Reason), 4);
        }

        private void local_Disconnected(object sender, NetSocketDisconnectedEventArgs e)
        {
            //this.server.Listen(5555);
        }

        private void server_DataArrived(object sender, NetSockDataArrivalEventArgs e)
        {
            client.Send(e.Data);
        }

        private void local_DataArrived(object sender, NetSockDataArrivalEventArgs e)
        {
            server.Send(e.Data);
        }

        private void server_ConnectionRequested(object sender, NetSockConnectionRequestEventArgs e)
        {
            account.Log(new ConnectionTextInformation("Connection Requested: " + ((System.Net.IPEndPoint)e.Client.RemoteEndPoint).Address.ToString()), 4);
            server.Accept(e.Client);
            account.Init();
        }

        private void local_ConnectionRequested(object sender, NetSockConnectionRequestEventArgs e)
        {
            server.Accept(e.Client);
            account.Log(new ConnectionTextInformation("Accepted client !"), 0);
            account.Init();
        }

        private void server_Connected(object sender, NetSocketConnectedEventArgs e)
        {
            account.Log(new ConnectionTextInformation("Connected: " + e.SourceIP), 4);
        }
        #endregion


        #endregion
        #endregion
    }
}

