using System.Collections.Generic;
using MageBot.Util.IO;
using MageBot.DataFiles.Data.D2p.Elements;

namespace MageBot.DataFiles.Data.D2p
{
    public class Cell
    {
        // Methods
        internal void Init(BigEndianReader Reader, int MapVersion)
        {
            CellId = Reader.ReadShort();
            ElementsCount = Reader.ReadShort();
            int elementsCount = ElementsCount;
            int i = 1;
            while ((i <= elementsCount))
            {
                BasicElement elementFromType = BasicElement.GetElementFromType(Reader.ReadByte());
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
