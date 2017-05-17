using MageBot.Protocol.Types.Game.Context.Roleplay;

namespace MageBot.Protocol.Types.Game.Character
{
    public class CharacterMinimalAllianceInformations : CharacterMinimalPlusLookInformations
    {
        public override int ProtocolId { get; } = 444;
        public override int TypeID { get { return ProtocolId; } }

        public BasicAllianceInformations Alliance { get; set; }

        public CharacterMinimalAllianceInformations() { }

        public CharacterMinimalAllianceInformations(BasicAllianceInformations alliance)
        {
            Alliance = alliance;
        }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            Alliance.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Alliance = new BasicAllianceInformations();
            Alliance.Deserialize(reader);
        }
    }
}
