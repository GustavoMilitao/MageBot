


















// Generated on 12/11/2014 19:02:06
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GameRolePlayNpcInformations : GameRolePlayActorInformations
    {

        public new const int ID = 156;
        public override int TypeId
        {
            get { return ID; }
        }

        public int npcId;
        public bool sex;
        public int specialArtworkId;


        public GameRolePlayNpcInformations()
        {
        }

        public GameRolePlayNpcInformations(ulong contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, int npcId, bool sex, int specialArtworkId)
                 : base(contextualId, look, disposition)
        {
            this.npcId = npcId;
            this.sex = sex;
            this.specialArtworkId = specialArtworkId;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteVarShort((short)npcId);
            writer.WriteBoolean(sex);
            writer.WriteVarShort((short)specialArtworkId);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            npcId = reader.ReadVarUhShort();
            if (npcId < 0)
                throw new Exception("Forbidden value on npcId = " + npcId + ", it doesn't respect the following condition : npcId < 0");
            sex = reader.ReadBoolean();
            specialArtworkId = reader.ReadVarUhShort();
            if (specialArtworkId < 0)
                throw new Exception("Forbidden value on specialArtworkId = " + specialArtworkId + ", it doesn't respect the following condition : specialArtworkId < 0");


        }


    }


}