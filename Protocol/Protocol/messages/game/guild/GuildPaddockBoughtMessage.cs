









// Generated on 12/11/2014 19:01:45
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GuildPaddockBoughtMessage : Message
    {
        public new const uint ID =5952;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.PaddockContentInformations paddockInfo;
        
        public GuildPaddockBoughtMessage()
        {
        }
        
        public GuildPaddockBoughtMessage(Types.PaddockContentInformations paddockInfo)
        {
            this.paddockInfo = paddockInfo;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            paddockInfo.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            paddockInfo = new Types.PaddockContentInformations();
            paddockInfo.Deserialize(reader);
        }
        
    }
    
}