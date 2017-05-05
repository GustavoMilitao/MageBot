


















// Generated on 12/11/2014 19:02:06
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GameRolePlayHumanoidInformations : GameRolePlayNamedActorInformations
    {

        public new const int ID = 159;
        public override int TypeId
        {
            get { return ID; }
        }

        public Types.HumanInformations humanoidInfo;
        public int accountId;


        public GameRolePlayHumanoidInformations()
        {
        }

        public GameRolePlayHumanoidInformations(ulong contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, string name, Types.HumanInformations humanoidInfo, int accountId)
                 : base(contextualId, look, disposition, name)
        {
            this.humanoidInfo = humanoidInfo;
            this.accountId = accountId;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteShort((short)humanoidInfo.TypeId);
            humanoidInfo.Serialize(writer);
            writer.WriteInt(accountId);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            humanoidInfo = Types.ProtocolTypeManager.GetInstance<HumanInformations>(reader.ReadUShort());
            humanoidInfo.Deserialize(reader);
            accountId = reader.ReadInt();
            if (accountId < 0)
                throw new Exception("Forbidden value on accountId = " + accountId + ", it doesn't respect the following condition : accountId < 0");


        }


    }


}