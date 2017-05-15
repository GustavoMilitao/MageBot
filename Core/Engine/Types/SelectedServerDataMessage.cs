
using BotForgeAPI.Network;
using System.Collections.Generic;
using BotForgeAPI.IO;
using System;

namespace Core.Engine.Types
{
    public class SelectedServerDataMessage : NetworkMessage
    {
        public const int Id = 42;

        public override int ProtocolId { get { return Id; } }

        public ushort ServerId;
        public string Address;
        public ushort Port;
        public bool CanCreateNewCharacter = false;
        public List<int> Ticket;

        public SelectedServerDataMessage() { }

        public SelectedServerDataMessage(bool canCreateNewCharacter, ushort serverId, string address, ushort port, List<int> ticket)
        {
            CanCreateNewCharacter = canCreateNewCharacter;
            ServerId = serverId;
            Address = address;
            Port = port;
            Ticket = ticket;
        }

        public SelectedServerDataMessage(ushort serverId, string address, ushort port, bool canCreateNewCharacter, List<int> ticket)
        {
            ServerId = serverId;
            Address = address;
            Port = port;
            CanCreateNewCharacter = canCreateNewCharacter;
            Ticket = ticket;
        }

        public override void Serialize(BotForgeAPI.IO.IDataWriter writer)
        {
            writer.WriteVarUhShort(ServerId);
            writer.WriteUTF(Address);
            writer.WriteUShort(Port);
            writer.WriteBoolean(CanCreateNewCharacter);
            for (int i = 0; i < Ticket.Count; i++)
            {
                writer.WriteByte((byte)Ticket[i]);
            }
        }

        public override void Deserialize(BotForgeAPI.IO.IDataReader reader)
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
