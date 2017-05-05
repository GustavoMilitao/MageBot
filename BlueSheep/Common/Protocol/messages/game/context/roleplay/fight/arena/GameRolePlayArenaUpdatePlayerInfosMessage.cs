









// Generated on 12/11/2014 19:01:33
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameRolePlayArenaUpdatePlayerInfosMessage : Message
    {
        public new const uint ID =6301;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int rank;
        public int bestDailyRank;
        public int bestRank;
        public int victoryCount;
        public int arenaFightcount;
        
        public GameRolePlayArenaUpdatePlayerInfosMessage()
        {
        }
        
        public GameRolePlayArenaUpdatePlayerInfosMessage(int rank, int bestDailyRank, int bestRank, int victoryCount, int arenaFightcount)
        {
            this.rank = rank;
            this.bestDailyRank = bestDailyRank;
            this.bestRank = bestRank;
            this.victoryCount = victoryCount;
            this.arenaFightcount = arenaFightcount;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort((short)rank);
            writer.WriteVarShort((short)bestDailyRank);
            writer.WriteVarShort((short)bestRank);
            writer.WriteVarShort((short)victoryCount);
            writer.WriteVarShort((short)arenaFightcount);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            rank = reader.ReadVarUhShort();
            if (rank < 0 || rank > 2300)
                throw new Exception("Forbidden value on rank = " + rank + ", it doesn't respect the following condition : rank < 0 || rank > 2300");
            bestDailyRank = reader.ReadVarUhShort();
            if (bestDailyRank < 0 || bestDailyRank > 2300)
                throw new Exception("Forbidden value on bestDailyRank = " + bestDailyRank + ", it doesn't respect the following condition : bestDailyRank < 0 || bestDailyRank > 2300");
            bestRank = reader.ReadVarUhShort();
            if (bestRank < 0 || bestRank > 2300)
                throw new Exception("Forbidden value on bestRank = " + bestRank + ", it doesn't respect the following condition : bestRank < 0 || bestRank > 2300");
            victoryCount = reader.ReadVarUhShort();
            if (victoryCount < 0)
                throw new Exception("Forbidden value on victoryCount = " + victoryCount + ", it doesn't respect the following condition : victoryCount < 0");
            arenaFightcount = reader.ReadVarUhShort();
            if (arenaFightcount < 0)
                throw new Exception("Forbidden value on arenaFightcount = " + arenaFightcount + ", it doesn't respect the following condition : arenaFightcount < 0");
        }
        
    }
    
}