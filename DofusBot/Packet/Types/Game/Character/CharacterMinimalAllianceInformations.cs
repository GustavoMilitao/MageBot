using DofusBot.Packet.Types.Game.Context.Roleplay;

namespace DofusBot.Packet.Types.Game.Character
{
    public class CharacterMinimalAllianceInformations : CharacterMinimalPlusLookInformations
    {
        public new TypeEnum Type
        {
            get { return TypeEnum.CharacterMinimalAllianceInformations; }
        }

        public new const short ProtocolId = 444;
        public override short TypeID { get { return ProtocolId; } }

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
