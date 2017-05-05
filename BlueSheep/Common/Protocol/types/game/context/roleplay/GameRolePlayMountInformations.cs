


















// Generated on 12/11/2014 19:02:06
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GameRolePlayMountInformations : GameRolePlayNamedActorInformations
    {

        public new const int ID = 180;
        public override int TypeId
        {
            get { return ID; }
        }

        public string ownerName;
        public byte level;


        public GameRolePlayMountInformations()
        {
        }

        public GameRolePlayMountInformations(ulong contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, string name, string ownerName, byte level)
                 : base(contextualId, look, disposition, name)
        {
            this.ownerName = ownerName;
            this.level = level;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteUTF(ownerName);
            writer.WriteByte(level);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            ownerName = reader.ReadUTF();
            level = reader.ReadByte();
            if (level < 0 || level > 255)
                throw new Exception("Forbidden value on level = " + level + ", it doesn't respect the following condition : level < 0 || level > 255");


        }


    }


}