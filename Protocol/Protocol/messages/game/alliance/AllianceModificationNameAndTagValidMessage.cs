









// Generated on 12/11/2014 19:01:20
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class AllianceModificationNameAndTagValidMessage : Message
    {
        public new const uint ID =6449;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public string allianceName;
        public string allianceTag;
        
        public AllianceModificationNameAndTagValidMessage()
        {
        }
        
        public AllianceModificationNameAndTagValidMessage(string allianceName, string allianceTag)
        {
            this.allianceName = allianceName;
            this.allianceTag = allianceTag;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUTF(allianceName);
            writer.WriteUTF(allianceTag);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            allianceName = reader.ReadUTF();
            allianceTag = reader.ReadUTF();
        }
        
    }
    
}