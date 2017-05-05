









// Generated on 12/11/2014 19:01:29
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ChallengeTargetsListRequestMessage : Message
    {
        public new const uint ID =5614;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int challengeId;
        
        public ChallengeTargetsListRequestMessage()
        {
        }
        
        public ChallengeTargetsListRequestMessage(int challengeId)
        {
            this.challengeId = challengeId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort((short)challengeId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            challengeId = reader.ReadVarUhShort();
            if (challengeId < 0)
                throw new Exception("Forbidden value on challengeId = " + challengeId + ", it doesn't respect the following condition : challengeId < 0");
        }
        
    }
    
}