using BlueSheep.Common.IO;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class CharacterBasicMinimalInformations : AbstractCharacterInformation
    {
        public new const short ID = 503;


        public String name = "";

        public CharacterBasicMinimalInformations()
        {
        }

        public CharacterBasicMinimalInformations(double id, string name)
        {
            this.name = name;
            Id = id;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(name);
        }

        public virtual void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            name = reader.ReadUTF();
        }
    }
}