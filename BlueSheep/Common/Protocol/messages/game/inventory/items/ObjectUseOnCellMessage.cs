









// Generated on 12/11/2014 19:01:55
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ObjectUseOnCellMessage : ObjectUseMessage
    {
        public new const uint ID =3013;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int cells;
        
        public ObjectUseOnCellMessage()
        {
        }
        
        public ObjectUseOnCellMessage(int objectUID, int cells)
         : base(objectUID)
        {
            this.cells = cells;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarShort((short)cells);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            cells = reader.ReadVarUhShort();
            if (cells < 0 || cells > 559)
                throw new Exception("Forbidden value on cells = " + cells + ", it doesn't respect the following condition : cells < 0 || cells > 559");
        }
        
    }
    
}