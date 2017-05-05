


















// Generated on 12/11/2014 19:02:06
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GameRolePlayMutantInformations : GameRolePlayHumanoidInformations
    {

        public new const int ID = 3;
        public override int TypeId
        {
            get { return ID; }
        }

        public int monsterId;
        public byte powerLevel;


        public GameRolePlayMutantInformations()
        {
        }

        public GameRolePlayMutantInformations(ulong contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, string name, Types.HumanInformations humanoidInfo, int accountId, int monsterId, byte powerLevel)
                 : base(contextualId, look, disposition, name, humanoidInfo, accountId)
        {
            this.monsterId = monsterId;
            this.powerLevel = powerLevel;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteVarShort((short)monsterId);
            writer.WriteByte(powerLevel);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            monsterId = reader.ReadVarUhShort();
            if (monsterId < 0)
                throw new Exception("Forbidden value on monsterId = " + monsterId + ", it doesn't respect the following condition : monsterId < 0");
            powerLevel = reader.ReadByte();


        }


    }


}