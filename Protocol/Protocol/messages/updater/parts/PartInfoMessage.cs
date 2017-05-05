









// Generated on 12/11/2014 19:02:01
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PartInfoMessage : Message
    {
        public new const uint ID =1508;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.ContentPart part;
        public float installationPercent;
        
        public PartInfoMessage()
        {
        }
        
        public PartInfoMessage(Types.ContentPart part, float installationPercent)
        {
            this.part = part;
            this.installationPercent = installationPercent;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            part.Serialize(writer);
            writer.WriteFloat(installationPercent);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            part = new Types.ContentPart();
            part.Deserialize(reader);
            installationPercent = reader.ReadFloat();
        }
        
    }
    
}