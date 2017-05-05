









// Generated on 12/11/2014 19:01:54
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class LivingObjectMessageMessage : Message
    {
        public new const uint ID =6065;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int msgId;
        public int timeStamp;
        public string owner;
        public int objectGenericId;
        
        public LivingObjectMessageMessage()
        {
        }
        
        public LivingObjectMessageMessage(int msgId, int timeStamp, string owner, int objectGenericId)
        {
            this.msgId = msgId;
            this.timeStamp = timeStamp;
            this.owner = owner;
            this.objectGenericId = objectGenericId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort((short)msgId);
            writer.WriteInt(timeStamp);
            writer.WriteUTF(owner);
            writer.WriteVarShort((short)objectGenericId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            msgId = reader.ReadVarUhShort();
            if (msgId < 0)
                throw new Exception("Forbidden value on msgId = " + msgId + ", it doesn't respect the following condition : msgId < 0");
            timeStamp = reader.ReadInt();
            if (timeStamp < 0)
                throw new Exception("Forbidden value on timeStamp = " + timeStamp + ", it doesn't respect the following condition : timeStamp < 0");
            owner = reader.ReadUTF();
            objectGenericId = reader.ReadVarUhShort();
            if (objectGenericId < 0)
                throw new Exception("Forbidden value on objectGenericId = " + objectGenericId + ", it doesn't respect the following condition : objectGenericId < 0");
        }
        
    }
    
}