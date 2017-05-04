using DofusBot.Core.Network;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace DofusBot.Network
{
    public class DofusBotSocket
    {
        private Socket _socket;
        private Thread _listeningThread;
        private IPEndPoint _endPoint;
        private DofusBotBuffer _buffer;
        private DofusBotPacketDeserializer _deserializer;

        public DofusBotSocket(DofusBotPacketDeserializer deserializer, IPEndPoint endPoint)
        {
            _endPoint = endPoint;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _listeningThread = new Thread(new ThreadStart(Listening));

            _buffer = new DofusBotBuffer();
            _deserializer = deserializer;
            _buffer.ReceivePacketBuffer += _deserializer.GetPacket;
        }

        private void Listening()
        {
            byte[] receiverBuffer;
            byte[] buffer;
            int bufferSize;
            while (true)
            {
                receiverBuffer = new byte[500];
                bufferSize = _socket.Receive(receiverBuffer);
                /*if (bufferSize == 0)
                    Environment.Exit(0);*/
                buffer = new byte[bufferSize];
                Array.Copy(receiverBuffer, 0, buffer, 0, bufferSize);
                _buffer.Enqueu(buffer);
            }
            
        }

        public void ConnectEndListen()
        {
            _socket.Connect(_endPoint);
            _listeningThread.Start();
        }

        public void CloseSocket()
        {
            _listeningThread.Abort();
            _socket.Close();
        }

        public void Send(NetworkMessage packet)
        {
            BigEndianWriter bigEndianWriter = new BigEndianWriter();
            packet.Pack(bigEndianWriter);
            _socket.Send(bigEndianWriter.Data);
        }

    }
}

