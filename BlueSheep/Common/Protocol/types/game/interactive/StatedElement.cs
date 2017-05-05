


















// Generated on 12/11/2014 19:02:10
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class StatedElement
    {

        public new const int ID = 108;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public int elementId;
        public int elementCellId;
        public uint elementState;
        public bool onCurrentMap { get; set; } = false;


        public StatedElement()
        {
        }

        public StatedElement(int elementId, int elementCellId, uint elementState, bool onCurrentMap)
        {
            this.elementId = elementId;
            this.elementCellId = elementCellId;
            this.elementState = elementState;
            this.onCurrentMap = onCurrentMap;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteInt(elementId);
            writer.WriteVarShort((short)elementCellId);
            writer.WriteVarInt(elementState);
            writer.WriteBoolean(onCurrentMap);


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            elementId = reader.ReadInt();
            if (elementId < 0)
                throw new Exception("Forbidden value on elementId = " + elementId + ", it doesn't respect the following condition : elementId < 0");
            elementCellId = reader.ReadVarUhShort();
            if (elementCellId < 0 || elementCellId > 559)
                throw new Exception("Forbidden value on elementCellId = " + elementCellId + ", it doesn't respect the following condition : elementCellId < 0 || elementCellId > 559");
            elementState = reader.ReadVarUhInt();
            if (elementState < 0)
                throw new Exception("Forbidden value on elementState = " + elementState + ", it doesn't respect the following condition : elementState < 0");


        }


    }


}