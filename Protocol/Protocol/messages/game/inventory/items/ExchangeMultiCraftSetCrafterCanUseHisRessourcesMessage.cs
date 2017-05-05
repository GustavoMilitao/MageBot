









// Generated on 12/11/2014 19:01:53
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeMultiCraftSetCrafterCanUseHisRessourcesMessage : Message
    {
        public new const uint ID =6021;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public bool allow;
        
        public ExchangeMultiCraftSetCrafterCanUseHisRessourcesMessage()
        {
        }
        
        public ExchangeMultiCraftSetCrafterCanUseHisRessourcesMessage(bool allow)
        {
            this.allow = allow;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteBoolean(allow);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            allow = reader.ReadBoolean();
        }
        
    }
    
}