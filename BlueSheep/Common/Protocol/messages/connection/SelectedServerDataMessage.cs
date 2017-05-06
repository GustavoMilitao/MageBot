 
using System.Collections.Generic;

namespace BlueSheep.Common.Protocol.Messages.Connection
{
    public class SelectedServerDataMessage : Message
    {
        public const int ProtocolId = 42;
        public override int MessageID { get { return ProtocolId; } }

        public ushort ServerId;
        public string Address;
        public ushort Port;
        public bool CanCreateNewCharacter = false;
        public List<int> Ticket;

        public SelectedServerDataMessage() { }

        public SelectedServerDataMessage(bool canCreateNewCharacter, ushort serverId, string address, ushort port, List<int> ticket)
        {
            this.CanCreateNewCharacter = canCreateNewCharacter;
            this.ServerId = serverId;
            this.Address = address;
            this.Port = port;
            this.Ticket = ticket;
        }

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
