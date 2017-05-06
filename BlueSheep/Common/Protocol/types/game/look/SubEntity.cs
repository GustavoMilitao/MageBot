namespace BlueSheep.Common.Protocol.Types.Game.Look
{
    public class SubEntity 
    {
        public new const int ID = 54;
        public virtual int TypeID { get { return ID; } }

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

        public void Serialize(IDataWriter writer)
        {
            writer.WriteByte(BindingPointCategory);
            writer.WriteByte(BindingPointIndex);
            SubEntityLook.Serialize(writer);
        }

        public void Deserialize(IDataReader reader)
        {
            BindingPointCategory = reader.ReadByte();
            BindingPointIndex = reader.ReadByte();
            SubEntityLook = new EntityLook();
            SubEntityLook.Deserialize(reader);
        }
    }
}
