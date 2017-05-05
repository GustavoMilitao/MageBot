









// Generated on 12/11/2014 19:01:46
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class TaxCollectorMovementAddMessage : Message
    {
        public new const uint ID =5917;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.TaxCollectorInformations informations;
        
        public TaxCollectorMovementAddMessage()
        {
        }
        
        public TaxCollectorMovementAddMessage(Types.TaxCollectorInformations informations)
        {
            this.informations = informations;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteShort(informations.TypeId);
            informations.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            informations = Types.ProtocolTypeManager.GetInstance<Types.TaxCollectorInformations>(reader.ReadShort());
            informations.Deserialize(reader);
        }
        
    }
    
}