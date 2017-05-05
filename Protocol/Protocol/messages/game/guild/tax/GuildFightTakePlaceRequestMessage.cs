









// Generated on 12/11/2014 19:01:46
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GuildFightTakePlaceRequestMessage : GuildFightJoinRequestMessage
    {
        public new const uint ID =6235;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int replacedCharacterId;
        
        public GuildFightTakePlaceRequestMessage()
        {
        }
        
        public GuildFightTakePlaceRequestMessage(int taxCollectorId, int replacedCharacterId)
         : base(taxCollectorId)
        {
            this.replacedCharacterId = replacedCharacterId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(replacedCharacterId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            replacedCharacterId = reader.ReadInt();
        }
        
    }
    
}