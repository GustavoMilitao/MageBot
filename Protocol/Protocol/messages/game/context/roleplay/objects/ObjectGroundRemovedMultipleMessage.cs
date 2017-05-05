









// Generated on 12/11/2014 19:01:36
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ObjectGroundRemovedMultipleMessage : Message
    {
        public new const uint ID =5944;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public short[] cells;
        
        public ObjectGroundRemovedMultipleMessage()
        {
        }
        
        public ObjectGroundRemovedMultipleMessage(short[] cells)
        {
            this.cells = cells;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUShort((ushort)cells.Length);
            foreach (var entry in cells)
            {
                 writer.WriteVarShort(entry);
            }
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            var limit = reader.ReadUShort();
            cells = new short[limit];
            for (int i = 0; i < limit; i++)
            {
                 cells[i] = reader.ReadVarShort();
            }
        }
        
    }
    
}