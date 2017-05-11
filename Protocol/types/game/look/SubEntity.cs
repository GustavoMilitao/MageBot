namespace BlueSheep.Protocol.Types.Game.Look
{
    public class SubEntity : NetworkType
    {
        public const int ProtocolId = 54;
        public override int TypeID { get { return ProtocolId; } }

        public byte BindingPointCategory;
        public byte BindingPointIndex;
        public EntityLook SubEntityLook;

        public SubEntity() { }

        public SubEntity(byte bindingPointCategory, byte bindingPointIndex, EntityLook subEntityLook)
        {
            BindingPointCategory = bindingPointCategory;
            BindingPointIndex = bindingPointIndex;
            SubEntityLook = subEntityLook;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(BindingPointCategory);
            writer.WriteByte(BindingPointIndex);
            SubEntityLook.Serialize(writer);
        }

        public override void Deserialize(IDataReader reader)
        {
            BindingPointCategory = reader.ReadByte();
            BindingPointIndex = reader.ReadByte();
            SubEntityLook = new EntityLook();
            SubEntityLook.Deserialize(reader);
        }
    }
}
