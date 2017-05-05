


















// Generated on 12/11/2014 19:02:04
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class TaxCollectorStaticInformations
    {

        public new const int ID = 147;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public int firstNameId;
        public int lastNameId;
        public Types.GuildInformations guildIdentity;


        public TaxCollectorStaticInformations()
        {
        }

        public TaxCollectorStaticInformations(int firstNameId, int lastNameId, Types.GuildInformations guildIdentity)
        {
            this.firstNameId = firstNameId;
            this.lastNameId = lastNameId;
            this.guildIdentity = guildIdentity;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteVarShort((short)firstNameId);
            writer.WriteVarShort((short)lastNameId);
            guildIdentity.Serialize(writer);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            firstNameId = reader.ReadVarUhShort();
            if (firstNameId < 0)
                throw new Exception("Forbidden value on firstNameId = " + firstNameId + ", it doesn't respect the following condition : firstNameId < 0");
            lastNameId = reader.ReadVarUhShort();
            if (lastNameId < 0)
                throw new Exception("Forbidden value on lastNameId = " + lastNameId + ", it doesn't respect the following condition : lastNameId < 0");
            guildIdentity = new Types.GuildInformations();
            guildIdentity.Deserialize(reader);


        }


    }


}