









// Generated on 12/11/2014 19:02:01
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class KrosmasterPlayingStatusMessage : Message
    {
        public new const uint ID =6347;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public bool playing;
        
        public KrosmasterPlayingStatusMessage()
        {
        }
        
        public KrosmasterPlayingStatusMessage(bool playing)
        {
            this.playing = playing;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteBoolean(playing);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            playing = reader.ReadBoolean();
        }
        
    }
    
}