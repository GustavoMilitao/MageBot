


















// Generated on 12/11/2014 19:02:10
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class HouseInformationsExtended : HouseInformations
    {

        public new const short ID = 112;
        public override short TypeId
        {
            get { return ID; }
        }

        public Types.GuildInformations guildInfo;


        public HouseInformationsExtended()
        {
        }

        public HouseInformationsExtended(uint houseId, ushort modelId, Types.GuildInformations guildInfo)
                 : base(houseId, modelId)
        {
            this.guildInfo = guildInfo;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            guildInfo.Serialize(writer);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            guildInfo = new Types.GuildInformations();
            guildInfo.Deserialize(reader);


        }


    }


}