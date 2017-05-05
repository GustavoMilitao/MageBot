









// Generated on 12/11/2014 19:01:27
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameMapMovementMessage : Message
    {
        public new const uint ID =951;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int[] keyMovements;
        public ulong actorId;
        
        public GameMapMovementMessage()
        {
        }
        
        public GameMapMovementMessage(int[] keyMovements, ulong actorId)
        {
            this.keyMovements = keyMovements;
            this.actorId = actorId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUShort((ushort)keyMovements.Length);
            foreach (var entry in keyMovements)
            {
                 writer.WriteShort((short)entry);
            }
            writer.WriteULong(actorId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            var limit = reader.ReadUShort();
            keyMovements = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 keyMovements[i] = reader.ReadShort();
            }
            actorId = reader.ReadULong();
        }
        
    }
    
}