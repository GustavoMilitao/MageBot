using BlueSheep.Common.IO;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class ArenaRankInfos
    {
        public new const short ID = 499;


        public uint rank = 0;

        public uint bestRank = 0;

        public uint victoryCount = 0;

        public uint fightcount = 0;

        public ArenaRankInfos()
        {
        }

        public void Serialize(BigEndianWriter writer)
        {
            if (rank < 0 || rank > 20000)
            {
                throw new Exception("Forbidden value (" + rank + ") on element rank.");
            }
            writer.WriteVarShort((short)rank);
            if (bestRank < 0 || bestRank > 20000)
            {
                throw new Exception("Forbidden value (" + bestRank + ") on element bestRank.");
            }
            writer.WriteVarShort((short)bestRank);
            if (victoryCount < 0)
            {
                throw new Exception("Forbidden value (" + victoryCount + ") on element victoryCount.");
            }
            writer.WriteVarShort((short)victoryCount);
            if (fightcount < 0)
            {
                throw new Exception("Forbidden value (" + fightcount + ") on element fightcount.");
            }
            writer.WriteVarShort((short)fightcount);
        }

        public void Deserialize(BigEndianReader reader)
        {
            _rankFunc(reader);
            _bestRankFunc(reader);
            _victoryCountFunc(reader);
            _fightcountFunc(reader);
        }

        private void  _rankFunc(BigEndianReader reader)
        {
            rank = reader.ReadVarUhShort();
            if (rank < 0 || rank > 20000)
            {
                throw new Exception("Forbidden value (" + rank + ") on element of ArenaRankInfos.rank.");
            }
        }

        private void  _bestRankFunc(BigEndianReader reader)
        {
            bestRank = reader.ReadVarUhShort();
            if (bestRank < 0 || bestRank > 20000)
            {
                throw new Exception("Forbidden value (" + bestRank + ") on element of ArenaRankInfos.bestRank.");
            }
        }

        private void _victoryCountFunc(BigEndianReader reader)
        {
            victoryCount = reader.ReadVarUhShort();
            if (victoryCount < 0)
            {
                throw new Exception("Forbidden value (" + victoryCount + ") on element of ArenaRankInfos.victoryCount.");
            }
        }

        private void  _fightcountFunc(BigEndianReader reader)
        {
            fightcount = reader.ReadVarUhShort();
            if (fightcount < 0)
            {
                throw new Exception("Forbidden value (" + fightcount + ") on element of ArenaRankInfos.fightcount.");
            }
        }

    }
}