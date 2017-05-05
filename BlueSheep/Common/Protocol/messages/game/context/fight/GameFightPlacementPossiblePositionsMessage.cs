









// Generated on 12/11/2014 19:01:28
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameFightPlacementPossiblePositionsMessage : Message
    {
        public new const uint ID =703;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int[] positionsForChallengers;
        public int[] positionsForDefenders;
        public sbyte teamNumber;
        
        public GameFightPlacementPossiblePositionsMessage()
        {
        }
        
        public GameFightPlacementPossiblePositionsMessage(int[] positionsForChallengers, int[] positionsForDefenders, sbyte teamNumber)
        {
            this.positionsForChallengers = positionsForChallengers;
            this.positionsForDefenders = positionsForDefenders;
            this.teamNumber = teamNumber;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUShort((ushort)positionsForChallengers.Length);
            foreach (var entry in positionsForChallengers)
            {
                 writer.WriteVarShort((short)entry);
            }
            writer.WriteUShort((ushort)positionsForDefenders.Length);
            foreach (var entry in positionsForDefenders)
            {
                 writer.WriteVarShort((short)entry);
            }
            writer.WriteSByte(teamNumber);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            var limit = reader.ReadUShort();
            positionsForChallengers = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 positionsForChallengers[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            positionsForDefenders = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 positionsForDefenders[i] = reader.ReadVarUhShort();
            }
            teamNumber = reader.ReadSByte();
            if (teamNumber < 0)
                throw new Exception("Forbidden value on teamNumber = " + teamNumber + ", it doesn't respect the following condition : teamNumber < 0");
        }
        
    }
    
}