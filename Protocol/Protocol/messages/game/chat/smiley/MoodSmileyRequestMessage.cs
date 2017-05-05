









// Generated on 12/11/2014 19:01:25
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class MoodSmileyRequestMessage : Message
    {
        public new const uint ID =6192;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte smileyId;
        
        public MoodSmileyRequestMessage()
        {
        }
        
        public MoodSmileyRequestMessage(sbyte smileyId)
        {
            this.smileyId = smileyId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteSByte(smileyId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            smileyId = reader.ReadSByte();
        }
        
    }
    
}