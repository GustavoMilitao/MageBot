


















// Generated on 12/11/2014 19:02:05
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class FightTeamMemberTaxCollectorInformations : FightTeamMemberInformations
    {

        public new const int ID = 177;
        public override int TypeId
        {
            get { return ID; }
        }

        public int firstNameId;
        public int lastNameId;
        public byte level;
        public uint guildId;
        public uint uid;


        public FightTeamMemberTaxCollectorInformations()
        {
        }

        public FightTeamMemberTaxCollectorInformations(int id, int firstNameId, int lastNameId, byte level, uint guildId, uint uid)
                 : base(id)
        {
            this.firstNameId = firstNameId;
            this.lastNameId = lastNameId;
            this.level = level;
            this.guildId = guildId;
            this.uid = uid;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteVarShort((short)firstNameId);
            writer.WriteVarShort((short)lastNameId);
            writer.WriteByte(level);
            writer.WriteVarInt(guildId);
            writer.WriteVarInt(uid);


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            firstNameId = reader.ReadVarUhShort();
            if (firstNameId < 0)
                throw new Exception("Forbidden value on firstNameId = " + firstNameId + ", it doesn't respect the following condition : firstNameId < 0");
            lastNameId = reader.ReadVarUhShort();
            if (lastNameId < 0)
                throw new Exception("Forbidden value on lastNameId = " + lastNameId + ", it doesn't respect the following condition : lastNameId < 0");
            level = reader.ReadByte();
            if (level < 1 || level > 200)
                throw new Exception("Forbidden value on level = " + level + ", it doesn't respect the following condition : level < 1 || level > 200");
            guildId = reader.ReadVarUhInt();
            if (guildId < 0)
                throw new Exception("Forbidden value on guildId = " + guildId + ", it doesn't respect the following condition : guildId < 0");
            uid = reader.ReadVarUhInt();
            if (uid < 0)
                throw new Exception("Forbidden value on uid = " + uid + ", it doesn't respect the following condition : uid < 0");


        }


    }


}