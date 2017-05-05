









// Generated on 12/11/2014 19:01:58
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class CinematicMessage : Message
    {
        public new const uint ID =6053;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int cinematicId;
        
        public CinematicMessage()
        {
        }
        
        public CinematicMessage(int cinematicId)
        {
            this.cinematicId = cinematicId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort((short)cinematicId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            cinematicId = reader.ReadVarUhShort();
            if (cinematicId < 0)
                throw new Exception("Forbidden value on cinematicId = " + cinematicId + ", it doesn't respect the following condition : cinematicId < 0");
        }
        
    }
    
}