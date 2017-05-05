









// Generated on 12/11/2014 19:01:26
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameEntityDispositionMessage : Message
    {
        public new const uint ID =5693;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.IdentifiedEntityDispositionInformations disposition;
        
        public GameEntityDispositionMessage()
        {
        }
        
        public GameEntityDispositionMessage(Types.IdentifiedEntityDispositionInformations disposition)
        {
            this.disposition = disposition;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            disposition.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            disposition = new Types.IdentifiedEntityDispositionInformations();
            disposition.Deserialize(reader);
        }
        
    }
    
}