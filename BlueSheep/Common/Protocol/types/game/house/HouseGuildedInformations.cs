using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Types
{
    public class HouseGuildedInformations : HouseInstanceInformations
    {
        public new const int ID = 512;
        public GuildInformations guildInfo;

        public HouseGuildedInformations()
        {

        }

        public virtual void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            guildInfo.Serialize(writer);
        }

        public virtual void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            guildInfo = new GuildInformations();
            guildInfo.Deserialize(reader);
        }
    }
}