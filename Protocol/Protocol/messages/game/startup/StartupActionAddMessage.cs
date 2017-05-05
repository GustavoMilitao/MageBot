









// Generated on 12/11/2014 19:01:59
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class StartupActionAddMessage : Message
    {
        public new const uint ID =6538;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.StartupActionAddObject newAction;
        
        public StartupActionAddMessage()
        {
        }
        
        public StartupActionAddMessage(Types.StartupActionAddObject newAction)
        {
            this.newAction = newAction;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            newAction.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            newAction = new Types.StartupActionAddObject();
            newAction.Deserialize(reader);
        }
        
    }
    
}