









// Generated on 12/11/2014 19:01:53
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeMultiCraftCrafterCanUseHisRessourcesMessage : Message
    {
        public new const uint ID =6020;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public bool allowed;
        
        public ExchangeMultiCraftCrafterCanUseHisRessourcesMessage()
        {
        }
        
        public ExchangeMultiCraftCrafterCanUseHisRessourcesMessage(bool allowed)
        {
            this.allowed = allowed;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteBoolean(allowed);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            allowed = reader.ReadBoolean();
        }
        
    }
    
}