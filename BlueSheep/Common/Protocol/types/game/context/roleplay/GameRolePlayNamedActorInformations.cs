


















// Generated on 12/11/2014 19:02:06
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GameRolePlayNamedActorInformations : GameRolePlayActorInformations
    {

        public new const int ID = 154;
        public override int TypeId
        {
            get { return ID; }
        }

        public string name;


        public GameRolePlayNamedActorInformations()
        {
        }

        public GameRolePlayNamedActorInformations(ulong contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, string name)
                 : base(contextualId, look, disposition)
        {
            this.name = name;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteUTF(name);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            name = reader.ReadUTF();


        }


    }


}