









// Generated on 12/11/2014 19:01:38
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PartyInvitationDungeonMessage : PartyInvitationMessage
    {
        public new const uint ID =6244;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int dungeonId;
        
        public PartyInvitationDungeonMessage()
        {
        }
        
        public PartyInvitationDungeonMessage(int partyId, sbyte partyType, string partyName, sbyte maxParticipants, int fromId, string fromName, int toId, int dungeonId)
         : base(partyId, partyType, partyName, maxParticipants, fromId, fromName, toId)
        {
            this.dungeonId = dungeonId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarShort((short)dungeonId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            dungeonId = reader.ReadVarUhShort();
            if (dungeonId < 0)
                throw new Exception("Forbidden value on dungeonId = " + dungeonId + ", it doesn't respect the following condition : dungeonId < 0");
        }
        
    }
    
}