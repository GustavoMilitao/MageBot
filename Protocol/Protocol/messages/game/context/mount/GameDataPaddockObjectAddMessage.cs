









// Generated on 12/11/2014 19:01:29
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameDataPaddockObjectAddMessage : Message
    {
        public new const uint ID =5990;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.PaddockItem paddockItemDescription;
        
        public GameDataPaddockObjectAddMessage()
        {
        }
        
        public GameDataPaddockObjectAddMessage(Types.PaddockItem paddockItemDescription)
        {
            this.paddockItemDescription = paddockItemDescription;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            paddockItemDescription.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            paddockItemDescription = new Types.PaddockItem();
            paddockItemDescription.Deserialize(reader);
        }
        
    }
    
}