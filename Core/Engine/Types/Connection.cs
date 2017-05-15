using BlueSheep.Util.IO;
using BotForge.Core.Server;
using BotForgeAPI.Network;
using BotForgeAPI.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Engine.Types
{
    public class Connection
    {
        public Account Conta { get; set; }
        private int? m_Header;
        private int? m_Length;
        public byte[] m_Data;
        private int? m_LenghtType;
        private int? m_ProtocolID;
        private List<int> ForbiddenHeaders = new List<int>() { 42, 6469 };

        public Connection(Account conta, string addr, int port)
        {
            Conta = conta;
            MessageReceiver mr = new MessageReceiver();
            mr.Initialize();
            ServerConnection sc = new ServerConnection(mr);
            sc.MessageSent += Sc_MessageSent;
            sc.MessageReceived += OnMessageReceived;
            sc.DataReceived += OnDataReceived;
            sc.Connected += OnConnected;
            sc.Disconnecting += OnDisconnecting;
            sc.Disconnected += OnDisconnected;
            conta.Treatment = new BlueSheep.Engine.Treatment.Treatment(conta);
            sc.Connect(addr, port);
            conta.Network.Connection = new ConnectionServer(sc, mr);
        }

        private void OnDataReceived(ServerConnection arg1, int arg2, byte[] arg3)
        {
            if (arg2 == 6469)
            {
                BigEndianReader m_Reader = new BigEndianReader(arg3);
                if (Build(m_Reader))
                {
                    using (BigEndianReader r = new BigEndianReader(m_Data))
                    {
                        SelectedServerDataExtendedMessage msg = new SelectedServerDataExtendedMessage();
                        msg.Deserialize(r);
                        OnMessageReceived(arg1, msg);
                    }
                }
            }
        }

        private void OnDisconnected(ServerConnection obj)
        {
            Console.WriteLine("Disconnected from server");
        }

        private void OnDisconnecting(object sender, EventArgs e)
        {
            Console.WriteLine("Disconnecting from the server");
        }

        private void OnConnected(ServerConnection obj)
        {
            obj.Send(new BasicPingMessage());
        }

        private void OnMessageReceived(ServerConnection arg1, NetworkMessage arg2)
        {
            //Console.WriteLine(" [RCV] (" + arg2.ProtocolId + ") " + arg2.GetType().Name);
            Conta.Treatment.Treat(arg2);
        }

        private void Sc_MessageSent(ServerConnection arg1, NetworkMessage arg2)
        {
            Console.WriteLine(" [SND] (" + arg2.ProtocolId + ") " + arg2.GetType().Name);
        }

        private bool Build(BotForgeAPI.IO.IDataReader m_Reader)
        {
            m_Header = m_Reader.ReadShort();
            m_ProtocolID = m_Header >> 2;
            m_LenghtType = m_Header & 0x3;
            if ((m_LenghtType.HasValue) &&
            (m_Reader.BytesAvailable >= m_LenghtType) && (!m_Length.HasValue))
            {
                if ((m_LenghtType < 0) || (m_LenghtType > 3))
                    throw new Exception("Malformated Message Header, invalid bytes number to read message length (inferior to 0 or superior to 3)");
                m_Length = 0;
                for (int i = m_LenghtType.Value - 1; i >= 0; i--)
                    m_Length |= m_Reader.ReadByte() << (i * 8);
            }
            if ((m_Data == null) && (m_Length.HasValue))
            {
                if (m_Length == 0)
                    m_Data = new byte[0];
                if (m_Reader.BytesAvailable >= m_Length)
                    m_Data = m_Reader.ReadBytes(m_Length.Value);
                else if (m_Length > m_Reader.BytesAvailable)
                    m_Data = m_Reader.ReadBytes((int)m_Reader.BytesAvailable);
            }
            if ((m_Data != null) && (m_Length.HasValue) && (m_Data.Length < m_Length))
            {
                int bytesToRead = 0;
                if (m_Data.Length + m_Reader.BytesAvailable < m_Length)
                    bytesToRead = (int)m_Reader.BytesAvailable;
                else if (m_Data.Length + m_Reader.BytesAvailable >= m_Length)
                    bytesToRead = m_Length.Value - m_Data.Length;
                if (bytesToRead != 0)
                {
                    int oldLength = m_Data.Length;
                    Array.Resize(ref m_Data, m_Data.Length + bytesToRead);
                    Array.Copy(m_Reader.ReadBytes(bytesToRead), 0, m_Data, oldLength, bytesToRead);
                }
            }
            return m_Data != null && ((m_Header.HasValue) && (m_Length.HasValue) && (m_Length == m_Data.Length));
        }
    }
}
