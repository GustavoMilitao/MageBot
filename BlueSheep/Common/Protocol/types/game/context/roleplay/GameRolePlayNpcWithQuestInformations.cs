


















// Generated on 12/11/2014 19:02:06
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GameRolePlayNpcWithQuestInformations : GameRolePlayNpcInformations
    {

        public new const int ID = 383;
        public override int TypeId
        {
            get { return ID; }
        }

        public Types.GameRolePlayNpcQuestFlag questFlag;


        public GameRolePlayNpcWithQuestInformations()
        {
        }

        public GameRolePlayNpcWithQuestInformations(ulong contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, int npcId, bool sex, int specialArtworkId, Types.GameRolePlayNpcQuestFlag questFlag)
                 : base(contextualId, look, disposition, npcId, sex, specialArtworkId)
        {
            this.questFlag = questFlag;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            questFlag.Serialize(writer);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            questFlag = new Types.GameRolePlayNpcQuestFlag();
            questFlag.Deserialize(reader);


        }


    }


}