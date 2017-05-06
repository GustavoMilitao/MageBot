using BlueSheep.Common.Protocol.Types.Game.Context.Roleplay;

namespace BlueSheep.Common.Protocol.Types.Game.Character
{
    public class CharacterMinimalGuildInformations : CharacterMinimalPlusLookInformations
    {
        public new const int ID = 445;
        public virtual int TypeID { get { return ID; } }

        public BasicGuildInformations Guild { get; set; }

        public CharacterMinimalGuildInformations() { }

        public CharacterMinimalGuildInformations(BasicGuildInformations guild)
        {
            Guild = guild;
        }

        public void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            Guild.Serialize(writer);
        }

        public void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Guild = new BasicGuildInformations();
            Guild.Deserialize(reader);
        }
    }
}
