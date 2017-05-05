









// Generated on 12/11/2014 19:01:31
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class MapRunningFightDetailsExtendedMessage : MapRunningFightDetailsMessage
    {
        public new const uint ID =6500;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.NamedPartyTeam[] namedPartyTeams;
        
        public MapRunningFightDetailsExtendedMessage()
        {
        }
        
        public MapRunningFightDetailsExtendedMessage(int fightId, Types.GameFightFighterLightInformations[] attackers, Types.GameFightFighterLightInformations[] defenders, Types.NamedPartyTeam[] namedPartyTeams)
         : base(fightId, attackers, defenders)
        {
            this.namedPartyTeams = namedPartyTeams;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUShort((ushort)namedPartyTeams.Length);
            foreach (var entry in namedPartyTeams)
            {
                 entry.Serialize(writer);
            }
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            var limit = reader.ReadUShort();
            namedPartyTeams = new Types.NamedPartyTeam[limit];
            for (int i = 0; i < limit; i++)
            {
                 namedPartyTeams[i] = new Types.NamedPartyTeam();
                 namedPartyTeams[i].Deserialize(reader);
            }
        }
        
    }
    
}