









// Generated on 12/11/2014 19:01:14
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class SelectedServerDataMessage : Message
    {
        public const uint ID =42;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public bool ssl;
        public bool canCreateNewCharacter;
        public ushort serverId;
        public string address;
        public ushort port;
        public List<int> ticket;
        
        public SelectedServerDataMessage()
        {
        }
        
        public SelectedServerDataMessage(bool canCreateNewCharacter, ushort serverId, string address, ushort port, List<int> ticket)
        {
            this.canCreateNewCharacter = canCreateNewCharacter;
            this.serverId = serverId;
            this.address = address;
            this.port = port;
            this.ticket = ticket;
        }

        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort(serverId);
            writer.WriteUTF(address);
            writer.WriteUShort(port);
            writer.WriteBoolean(canCreateNewCharacter);
            for (int i = 0; i < ticket.Count; i++)
            {
                writer.WriteByte((byte)ticket[i]);
            }
        }

        public override void Deserialize(BigEndianReader reader)
        {
            serverId = (ushort)reader.ReadVarShort();
            address = reader.ReadUTF();
            port = reader.ReadUShort();
            canCreateNewCharacter = reader.ReadBoolean();
            int size = reader.ReadVarInt();
            ticket = new List<int>();
            for (int i = 0; i < size; i++)
            {
                ticket.Add(reader.ReadByte());
            }
        }

    }
    
}