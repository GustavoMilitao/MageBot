









// Generated on 12/11/2014 19:01:49
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeLeaveMessage : LeaveDialogMessage
    {
        public new const uint ID =5628;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public bool success;
        
        public ExchangeLeaveMessage()
        {
        }
        
        public ExchangeLeaveMessage(sbyte dialogType, bool success)
         : base(dialogType)
        {
            this.success = success;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(success);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            success = reader.ReadBoolean();
        }
        
    }
    
}