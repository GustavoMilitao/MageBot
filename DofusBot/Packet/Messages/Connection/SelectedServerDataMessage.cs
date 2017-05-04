using DofusBot.Core.Network;
using System.Collections.Generic;

namespace DofusBot.Packet.Messages.Connection
{
    public class SelectedServerDataMessage : NetworkMessage
    {
        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.SelectedServerDataMessage; }
        }

        public ushort ServerId;
        public string Address;
        public ushort Port;
        public bool CanCreateNewCharacter = false;
        public List<int> Ticket;

        public const uint ProtocolId = 42;
        public override uint MessageID { get { return ProtocolId; } }

        public SelectedServerDataMessage() { }

        public SelectedServerDataMessage(ushort serverId, string address, ushort port, bool canCreateNewCharacter, List<int> ticket)
        {
            ServerId = serverId;
            Address = address;
            Port = port;
            CanCreateNewCharacter = canCreateNewCharacter;
            Ticket = ticket;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(ServerId);
            writer.WriteUTF(Address);
            writer.WriteUShort(Port);
            writer.WriteBoolean(CanCreateNewCharacter);
            for (int i = 0; i < Ticket.Count; i++)
            {
                writer.WriteByte((byte)Ticket[i]);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            ServerId = reader.ReadVarUhShort();
            Address = reader.ReadUTF();
            Port = reader.ReadUShort();
            CanCreateNewCharacter = reader.ReadBoolean();
            int size = reader.ReadVarInt();
            Ticket = new List<int>();
            for (int i = 0; i < size; i++)
            {
                Ticket.Add(reader.ReadByte());
            }
        }
    }
}
