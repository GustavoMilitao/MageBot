


















// Generated on 12/11/2014 19:02:06
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GameRolePlayGroupMonsterInformations : GameRolePlayActorInformations
    {

        public new const int ID = 160;
        public override int TypeId
        {
            get { return ID; }
        }

        public Types.GroupMonsterStaticInformations staticInfos;
        public double creationTime { get; set; }
        public int ageBonus;
        public byte lootShare;
        public byte alignmentSide;
        public bool keyRingBonus;
        public bool hasHardcoreDrop;
        public bool hasAVARewardToken;


        public GameRolePlayGroupMonsterInformations()
        {
        }

        public GameRolePlayGroupMonsterInformations(ulong contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, Types.GroupMonsterStaticInformations staticInfos, int ageBonus, byte lootShare, byte alignmentSide, double creationTime)
                 : base(contextualId, look, disposition)
        {
            this.creationTime = creationTime;
            this.staticInfos = staticInfos;
            this.ageBonus = ageBonus;
            this.lootShare = lootShare;
            this.alignmentSide = alignmentSide;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteShort((short)staticInfos.TypeId);
            staticInfos.Serialize(writer);
            writer.WriteInt(ageBonus);
            writer.WriteByte(lootShare);
            writer.WriteByte(alignmentSide);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            byte b = reader.ReadByte();
            keyRingBonus = BooleanByteWrapper.GetFlag(b, 0);
            hasHardcoreDrop = BooleanByteWrapper.GetFlag(b, 1);
            hasAVARewardToken = BooleanByteWrapper.GetFlag(b, 2);
            int s = reader.ReadUShort();
            staticInfos = Types.ProtocolTypeManager.GetInstance<GroupMonsterStaticInformations>(s);
            staticInfos.Deserialize(reader);
            creationTime = reader.ReadDouble();
            ageBonus = reader.ReadInt();
            if (ageBonus < 0 )
                throw new Exception("Forbidden value on ageBonus = " + ageBonus + ", it doesn't respect the following condition : ageBonus < 0");
            lootShare = reader.ReadByte();
            alignmentSide = reader.ReadByte();


        }


    }


}