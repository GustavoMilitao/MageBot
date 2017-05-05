









// Generated on 12/11/2014 19:01:26
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameContextRefreshEntityLookMessage : Message
    {
        public new const uint ID =5637;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public ulong id;
        public Types.EntityLook look;
        
        public GameContextRefreshEntityLookMessage()
        {
        }
        
        public GameContextRefreshEntityLookMessage(ulong id, Types.EntityLook look)
        {
            this.id = id;
            this.look = look;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteULong(id);
            look.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            id = reader.ReadULong();
            look = new Types.EntityLook();
            look.Deserialize(reader);
        }
        
    }
    
}