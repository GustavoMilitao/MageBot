









// Generated on 12/11/2014 19:01:35
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class TaxCollectorDialogQuestionBasicMessage : Message
    {
        public new const uint ID =5619;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.BasicGuildInformations guildInfo;
        
        public TaxCollectorDialogQuestionBasicMessage()
        {
        }
        
        public TaxCollectorDialogQuestionBasicMessage(Types.BasicGuildInformations guildInfo)
        {
            this.guildInfo = guildInfo;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            guildInfo.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            guildInfo = new Types.BasicGuildInformations();
            guildInfo.Deserialize(reader);
        }
        
    }
    
}