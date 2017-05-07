using System.Collections.Generic;
using BlueSheep.Common.IO;
using BlueSheep.Data.D2p.Elements;

namespace BlueSheep.Data.D2p
{
    public class Cell
    {
        // Methods
        internal void Init(BigEndianReader Reader, int MapVersion)
        {
            CellId = Reader.ReadVarUhShort();
            ElementsCount = Reader.ReadVarUhShort();
            int elementsCount = ElementsCount;
            int i = 1;
            while ((i <= elementsCount))
            {
                BasicElement elementFromType = BasicElement.GetElementFromType(Reader.ReadVarUhShort());
                elementFromType.Init(Reader, MapVersion);
                Elements.Add(elementFromType);
                i += 1;
            }
        }

        // Fields
        public int CellId;
        public List<BasicElement> Elements = new List<BasicElement>();
        public int ElementsCount;
    }
}
