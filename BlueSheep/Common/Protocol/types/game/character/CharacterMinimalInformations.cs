using System;

namespace BlueSheep.Common.Protocol.Types.Game.Character
{
    public class CharacterMinimalInformations : CharacterBasicMinimalInformations
    {
        public new const int ID = 110;
        public virtual int TypeID { get { return ID; } }

        public byte Level { get; set; }

        public CharacterMinimalInformations() { }

        public CharacterMinimalInformations(byte level)
        {
            Level = level;
        }

        public void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte(Level);
        }

        public void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Level = reader.ReadByte();
        }
    }
}
