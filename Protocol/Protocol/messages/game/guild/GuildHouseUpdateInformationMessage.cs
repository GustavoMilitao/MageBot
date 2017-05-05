









// Generated on 12/11/2014 19:01:44
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GuildHouseUpdateInformationMessage : Message
    {
        public new const uint ID =6181;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.HouseInformationsForGuild housesInformations;
        
        public GuildHouseUpdateInformationMessage()
        {
        }
        
        public GuildHouseUpdateInformationMessage(Types.HouseInformationsForGuild housesInformations)
        {
            this.housesInformations = housesInformations;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            housesInformations.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            housesInformations = new Types.HouseInformationsForGuild();
            housesInformations.Deserialize(reader);
        }
        
    }
    
}