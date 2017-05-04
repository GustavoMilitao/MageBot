using DofusBot.Packet.Types.Game.Look;

namespace DofusBot.Packet.Types.Game.Character
{
    public class CharacterMinimalPlusLookInformations : CharacterMinimalInformations
    {
        public new TypeEnum Type
        {
            get { return TypeEnum.CharacterMinimalPlusLookInformations; }
        }

        public new const short ProtocolId = 163;
        public override short TypeID { get { return ProtocolId; } }

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
