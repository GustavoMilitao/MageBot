









// Generated on 12/11/2014 19:01:27
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class DungeonKeyRingMessage : Message
    {
        public new const uint ID =6299;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int[] availables;
        public int[] unavailables;
        
        public DungeonKeyRingMessage()
        {
        }
        
        public DungeonKeyRingMessage(int[] availables, int[] unavailables)
        {
            this.availables = availables;
            this.unavailables = unavailables;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUShort((ushort)availables.Length);
            foreach (var entry in availables)
            {
                 writer.WriteVarShort((short)entry);
            }
            writer.WriteUShort((ushort)unavailables.Length);
            foreach (var entry in unavailables)
            {
                 writer.WriteVarShort((short)entry);
            }
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            var limit = reader.ReadUShort();
            availables = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 availables[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            unavailables = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 unavailables[i] = reader.ReadVarUhShort();
            }
        }
        
    }
    
}