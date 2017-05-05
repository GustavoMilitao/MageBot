









// Generated on 12/11/2014 19:01:36
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class DungeonPartyFinderRegisterRequestMessage : Message
    {
        public new const uint ID =6249;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int[] dungeonIds;
        
        public DungeonPartyFinderRegisterRequestMessage()
        {
        }
        
        public DungeonPartyFinderRegisterRequestMessage(int[] dungeonIds)
        {
            this.dungeonIds = dungeonIds;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUShort((ushort)dungeonIds.Length);
            foreach (var entry in dungeonIds)
            {
                 writer.WriteVarShort((short)entry);
            }
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            var limit = reader.ReadUShort();
            dungeonIds = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 dungeonIds[i] = reader.ReadVarUhShort();
            }
        }
        
    }
    
}