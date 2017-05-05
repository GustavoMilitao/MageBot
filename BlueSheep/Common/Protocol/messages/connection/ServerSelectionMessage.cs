









// Generated on 12/11/2014 19:01:14
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ServerSelectionMessage : Message
    {
        public new const uint ID =40;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int serverId;
        
        public ServerSelectionMessage()
        {
        }
        
        public ServerSelectionMessage(int serverId)
        {
            this.serverId = serverId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort((short)serverId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            serverId = reader.ReadVarUhShort();
            if (serverId < 0)
                throw new Exception("Forbidden value on serverId = " + serverId + ", it doesn't respect the following condition : serverId < 0");
        }
        
    }
    
}