using DofusBot.Packet.Types.Game.Context.Roleplay;

namespace DofusBot.Packet.Types.Game.Character
{
    public class CharacterMinimalGuildInformations : CharacterMinimalPlusLookInformations
    {
        public new TypeEnum Type
        {
            get { return TypeEnum.CharacterMinimalGuildInformations; }
        }

        public new const short ProtocolId = 445;
        public override short TypeID { get { return ProtocolId; } }

        public BasicGuildInformations Guild { get; set; }

        public CharacterMinimalGuildInformations() { }

        public CharacterMinimalGuildInformations(BasicGuildInformations guild)
        {
            Guild = guild;
        }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            Guild.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Guild = new BasicGuildInformations();
            Guild.Deserialize(reader);
        }
    }
}
