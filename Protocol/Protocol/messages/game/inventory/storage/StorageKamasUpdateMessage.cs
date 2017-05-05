









// Generated on 12/11/2014 19:01:57
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class StorageKamasUpdateMessage : Message
    {
        public new const uint ID =5645;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int kamasTotal;
        
        public StorageKamasUpdateMessage()
        {
        }
        
        public StorageKamasUpdateMessage(int kamasTotal)
        {
            this.kamasTotal = kamasTotal;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt(kamasTotal);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            kamasTotal = reader.ReadInt();
        }
        
    }
    
}