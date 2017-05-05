


















// Generated on 12/11/2014 19:02:12
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class GuildInsiderFactSheetInformations : GuildFactSheetInformations
{

public new const int ID = 423;
public override int TypeId
{
    get { return ID; }
}

public string leaderName;
        public int nbConnectedMembers;
        public sbyte nbTaxCollectors;
        public int lastActivity;
        public bool enabled;
        

public GuildInsiderFactSheetInformations()
{
}

public GuildInsiderFactSheetInformations(uint guildId, string guildName, Types.GuildEmblem guildEmblem, int leaderId, byte guildLevel, int nbMembers, string leaderName, int nbConnectedMembers, sbyte nbTaxCollectors, int lastActivity, bool enabled)
         : base(guildId, guildName, guildLevel, guildEmblem, leaderId, nbMembers)
        {
            this.leaderName = leaderName;
            this.nbConnectedMembers = nbConnectedMembers;
            this.nbTaxCollectors = nbTaxCollectors;
            this.lastActivity = lastActivity;
            this.enabled = enabled;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteUTF(leaderName);
            writer.WriteVarShort((short)nbConnectedMembers);
            writer.WriteSByte(nbTaxCollectors);
            writer.WriteInt(lastActivity);
            writer.WriteBoolean(enabled);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            leaderName = reader.ReadUTF();
            nbConnectedMembers = reader.ReadVarUhShort();
            if (nbConnectedMembers < 0)
                throw new Exception("Forbidden value on nbConnectedMembers = " + nbConnectedMembers + ", it doesn't respect the following condition : nbConnectedMembers < 0");
            nbTaxCollectors = reader.ReadSByte();
            if (nbTaxCollectors < 0)
                throw new Exception("Forbidden value on nbTaxCollectors = " + nbTaxCollectors + ", it doesn't respect the following condition : nbTaxCollectors < 0");
            lastActivity = reader.ReadInt();
            if (lastActivity < 0)
                throw new Exception("Forbidden value on lastActivity = " + lastActivity + ", it doesn't respect the following condition : lastActivity < 0");
            enabled = reader.ReadBoolean();
            

}


}


}