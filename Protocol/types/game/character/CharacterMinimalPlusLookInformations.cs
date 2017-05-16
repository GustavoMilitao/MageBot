using BlueSheep.Protocol.Types.Game.Look;

namespace BlueSheep.Protocol.Types.Game.Character
{
    public class CharacterMinimalPlusLookInformations : CharacterMinimalInformations
    {
        protected override int ProtocolId { get; set; } = 163;
        public override int TypeID { get { return ProtocolId; } }

        public EntityLook EntityLook { get; set; }

        public CharacterMinimalPlusLookInformations() { }

        public CharacterMinimalPlusLookInformations(EntityLook entityLook)
        {
            EntityLook = entityLook;
        }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            EntityLook.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            EntityLook = new EntityLook();
            EntityLook.Deserialize(reader);
        }
    }
}
