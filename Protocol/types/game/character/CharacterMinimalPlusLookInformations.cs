using MageBot.Protocol.Types.Game.Look;

namespace MageBot.Protocol.Types.Game.Character
{
    public class CharacterMinimalPlusLookInformations : CharacterMinimalInformations
    {
        public override int ProtocolId { get; } = 163;
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
