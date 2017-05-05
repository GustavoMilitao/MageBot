









// Generated on 12/11/2014 19:01:29
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GameDataPaddockObjectRemoveMessage : Message
    {
        public new const uint ID =5993;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public short cellId;
        
        public GameDataPaddockObjectRemoveMessage()
        {
        }
        
        public GameDataPaddockObjectRemoveMessage(short cellId)
        {
            this.cellId = cellId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort(cellId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            cellId = reader.ReadVarShort();
            if (cellId < 0 || cellId > 559)
                throw new Exception("Forbidden value on cellId = " + cellId + ", it doesn't respect the following condition : cellId < 0 || cellId > 559");
        }
        
    }
    
}