using BlueSheep.Common.Protocol.Types.Game.Look;

namespace BlueSheep.Common.Protocol.Types.Game.Character
{
    public class CharacterMinimalPlusLookInformations : CharacterMinimalInformations
    {
        public new const int ID = 163;
        public virtual int TypeID { get { return ID; } }

        public EntityLook EntityLook { get; set; }

        public CharacterMinimalPlusLookInformations() { }

        public CharacterMinimalPlusLookInformations(EntityLook entityLook)
        {
            EntityLook = entityLook;
        }

        public void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            EntityLook.Serialize(writer);
        }

        public void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            EntityLook = new EntityLook();
            EntityLook.Deserialize(reader);
        }
    }
}
