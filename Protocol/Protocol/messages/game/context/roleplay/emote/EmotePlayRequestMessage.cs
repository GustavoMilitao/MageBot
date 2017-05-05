









// Generated on 12/11/2014 19:01:32
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class EmotePlayRequestMessage : Message
    {
        public new const uint ID =5685;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public byte emoteId;
        
        public EmotePlayRequestMessage()
        {
        }
        
        public EmotePlayRequestMessage(byte emoteId)
        {
            this.emoteId = emoteId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteByte(emoteId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            emoteId = reader.ReadByte();
            if (emoteId < 0 || emoteId > 255)
                throw new Exception("Forbidden value on emoteId = " + emoteId + ", it doesn't respect the following condition : emoteId < 0 || emoteId > 255");
        }
        
    }
    
}