









// Generated on 12/11/2014 19:01:35
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class AlliancePrismDialogQuestionMessage : Message
    {
        public new const uint ID =6448;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public AlliancePrismDialogQuestionMessage()
        {
        }
        
        
        public override void Serialize(BigEndianWriter writer)
        {
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
        }
        
    }
    
}