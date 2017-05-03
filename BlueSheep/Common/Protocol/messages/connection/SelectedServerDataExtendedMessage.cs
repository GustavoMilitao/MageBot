









// Generated on 12/11/2014 19:01:14
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;
using BlueSheep.Util.Enums.Servers;

namespace BlueSheep.Common.Protocol.Messages
{
    public class SelectedServerDataExtendedMessage : SelectedServerDataMessage
    {
        public new const uint ID =6469;
        public override uint ProtocolID
        {
            get { return ID; }
        }

        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.SelectedServerDataExtendedMessage; }
        }

        public List<ushort> serverIds;

        public SelectedServerDataExtendedMessage(bool canCreateNewCharacter, ushort serverId, string address, ushort port, List<int> ticket, List<ushort> serverIds)
         : base(canCreateNewCharacter, serverId, address, port, ticket)
        {
            this.serverIds = serverIds;
        }

        public SelectedServerDataExtendedMessage()
        {
        }

        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort((short)serverIds.Count);
            for (int i = 0; i < serverIds.Count; i++)
            {
                writer.WriteVarShort(serverIds[i]);
            }
        }

        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            ushort length = reader.ReadUShort();
            serverIds = new List<ushort>();
            for (int i = 0; i < length; i++)
            {
                serverIds.Add(reader.ReadVarUhShort());
            }
        }

    }
    
}