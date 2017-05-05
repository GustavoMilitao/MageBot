


















// Generated on 12/11/2014 19:02:02
using System;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class CharacterMinimalInformations : AbstractCharacterInformation
    {

        public new const short ID = 110;
        public override short TypeId
        {
            get { return ID; }
        }

        public byte level;
        public string name;


        public CharacterMinimalInformations()
        {
        }

        public CharacterMinimalInformations(uint id, byte level, string name)
                 : base(id)
        {
            this.level = level;
            this.name = name;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteUTF(name);
            writer.WriteByte(level);
        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            name = reader.ReadUTF();
            level = reader.ReadByte();
            if (level < 1 || level > 200)
                throw new Exception("Forbidden value on level = " + level + ", it doesn't respect the following condition : level < 1 || level > 200");
        }


    }


}