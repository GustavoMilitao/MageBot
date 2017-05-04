using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Types
{
    public class PaddockGuildedInformations : PaddockBuyableInformations
    {
        public PaddockGuildedInformations()
        {

        }
        public new const short ID = 508;
        public bool deserted = false;
        public GuildInformations guildInfo;

        public virtual void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteBoolean(deserted);
            guildInfo.Serialize(writer);
        }
        public virtual void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            _desertedFunc(reader);
            guildInfo = new GuildInformations();
            guildInfo.Deserialize(reader);
        }
        
        private void _desertedFunc(BigEndianReader reader)
        {
            deserted = reader.ReadBoolean();
        }

    }
}