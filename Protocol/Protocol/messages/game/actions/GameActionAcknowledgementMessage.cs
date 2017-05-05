









// Generated on 12/11/2014 19:01:15
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameActionAcknowledgementMessage : Message
    {
        public new const uint ID =957;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public bool valid;
        public sbyte actionId;
        
        public GameActionAcknowledgementMessage()
        {
        }
        
        public GameActionAcknowledgementMessage(bool valid, sbyte actionId)
        {
            this.valid = valid;
            this.actionId = actionId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteBoolean(valid);
            writer.WriteSByte(actionId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            valid = reader.ReadBoolean();
            actionId = reader.ReadSByte();
        }
        
    }
    
}