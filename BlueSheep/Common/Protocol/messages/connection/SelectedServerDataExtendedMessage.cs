









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

        public List<int> serverIds;

        public SelectedServerDataExtendedMessage(bool canCreateNewCharacter, int serverId, string address, int port, List<int> ticket, List<int> serverIds)
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
            writer.WriteShort((short)(int)serverIds.Count);
            for (int i = 0; i < serverIds.Count; i++)
            {
                writer.WriteVarShort((short)serverIds[i]);
            }
        }

        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            int length = reader.ReadUShort();
            serverIds = new List<int>();
            for (int i = 0; i < length; i++)
            {
                serverIds.Add((int)reader.ReadVarUhShort());
            }
        }

    }
    
}