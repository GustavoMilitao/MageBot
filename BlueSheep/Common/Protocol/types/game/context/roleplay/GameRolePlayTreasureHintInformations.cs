


















// Generated on 12/11/2014 19:02:06
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GameRolePlayTreasureHintInformations : GameRolePlayActorInformations
    {

        public new const int ID = 471;
        public override int TypeId
        {
            get { return ID; }
        }

        public int npcId;


        public GameRolePlayTreasureHintInformations()
        {
        }

        public GameRolePlayTreasureHintInformations(ulong contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, int npcId)
                 : base(contextualId, look, disposition)
        {
            this.npcId = npcId;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteVarShort((short)npcId);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            npcId = reader.ReadVarUhShort();
            if (npcId < 0)
                throw new Exception("Forbidden value on npcId = " + npcId + ", it doesn't respect the following condition : npcId < 0");


        }


    }


}