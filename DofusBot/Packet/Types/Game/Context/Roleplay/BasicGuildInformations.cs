using DofusBot.Packet.Types.Game.Social;

namespace DofusBot.Packet.Types.Game.Context.Roleplay
{
    public class BasicGuildInformations : AbstractSocialGroupInfos
    {
        public new TypeEnum Type
        {
            get { return TypeEnum.BasicGuildInformations; }
        }

        public new const short ProtocolId = 365;
        public override short TypeID { get { return ProtocolId; } }

        public uint GuildId;
        public string GuildName;
        public sbyte GuildLevel;

        public BasicGuildInformations() { }

        public BasicGuildInformations(uint guildId, string guildName, sbyte guildLevel)
        {
            GuildId = guildId;
            GuildName = guildName;
            GuildLevel = guildLevel;
        }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarInt(GuildId);
            writer.WriteUTF(GuildName);
            writer.WriteSByte(GuildLevel);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            GuildId = reader.ReadVarUhInt();
            GuildName = reader.ReadUTF();
            GuildLevel = reader.ReadSByte();
        }
    }
}
