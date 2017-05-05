









// Generated on 12/11/2014 19:01:46
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ServerExperienceModificatorMessage : Message
    {
        public new const uint ID =6237;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int experiencePercent;
        
        public ServerExperienceModificatorMessage()
        {
        }
        
        public ServerExperienceModificatorMessage(int experiencePercent)
        {
            this.experiencePercent = experiencePercent;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort((short)experiencePercent);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            experiencePercent = reader.ReadVarUhShort();
            if (experiencePercent < 0)
                throw new Exception("Forbidden value on experiencePercent = " + experiencePercent + ", it doesn't respect the following condition : experiencePercent < 0");
        }
        
    }
    
}