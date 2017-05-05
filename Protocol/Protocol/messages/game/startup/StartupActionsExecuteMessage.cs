









// Generated on 12/11/2014 19:01:59
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class StartupActionsExecuteMessage : Message
    {
        public new const uint ID =1302;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        
        public StartupActionsExecuteMessage()
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