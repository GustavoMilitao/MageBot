


















// Generated on 12/11/2014 19:02:06
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GameRolePlayCharacterInformations : GameRolePlayHumanoidInformations
    {

        public new const int ID = 36;
        public override int TypeId
        {
            get { return ID; }
        }

        public Types.ActorAlignmentInformations alignmentInfos;


        public GameRolePlayCharacterInformations()
        {
        }

        public GameRolePlayCharacterInformations(ulong contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, string name, Types.HumanInformations humanoidInfo, int accountId, Types.ActorAlignmentInformations alignmentInfos)
                 : base(contextualId, look, disposition, name, humanoidInfo, accountId)
        {
            this.alignmentInfos = alignmentInfos;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            alignmentInfos.Serialize(writer);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            alignmentInfos = new ActorAlignmentInformations();
            alignmentInfos.Deserialize(reader);


        }


    }


}