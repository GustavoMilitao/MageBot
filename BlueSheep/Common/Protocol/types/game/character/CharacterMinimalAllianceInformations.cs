using BlueSheep.Common.Protocol.Types.Game.Context.Roleplay;

namespace BlueSheep.Common.Protocol.Types.Game.Character
{
    public class CharacterMinimalAllianceInformations : CharacterMinimalPlusLookInformations
    {
        public new const int ID = 444;
        public virtual int TypeID { get { return ID; } }

        public BasicAllianceInformations Alliance { get; set; }

        public CharacterMinimalAllianceInformations() { }

        public CharacterMinimalAllianceInformations(BasicAllianceInformations alliance)
        {
            Alliance = alliance;
        }

        public void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            Alliance.Serialize(writer);
        }

        public void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Alliance = new BasicAllianceInformations();
            Alliance.Deserialize(reader);
        }
    }
}
