using MageBot.Protocol.Types.Game.Context.Roleplay;

namespace MageBot.Protocol.Types.Game.Character
{
    public class CharacterMinimalGuildInformations : CharacterMinimalPlusLookInformations
    {
        public override int ProtocolId { get; } = 445;
        public override int TypeID { get { return ProtocolId; } }

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
