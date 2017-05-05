









// Generated on 12/11/2014 19:01:24
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class PlayerStatusUpdateMessage : Message
    {
        public new const uint ID =6386;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int accountId;
        public ulong playerId;
        public Types.PlayerStatus status;
        
        public PlayerStatusUpdateMessage()
        {
        }
        
        public PlayerStatusUpdateMessage(int accountId, ulong playerId, Types.PlayerStatus status)
        {
            this.accountId = accountId;
            this.playerId = playerId;
            this.status = status;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteInt(accountId);
            writer.WriteVarLong(playerId);
            writer.WriteShort((short)status.TypeId);
            status.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            accountId = reader.ReadInt();
            if (accountId < 0)
                throw new Exception("Forbidden value on accountId = " + accountId + ", it doesn't respect the following condition : accountId < 0");
            playerId = reader.ReadVarUhLong();
            if (playerId < 0)
                throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0");
            status = Types.ProtocolTypeManager.GetInstance<Types.PlayerStatus>(reader.ReadUShort());
            status.Deserialize(reader);
        }
        
    }
    
}