using BlueSheep.Util.IO;

namespace BlueSheep.Data.D2p
{
    public class CellData
    {
        // Methods
        internal void Init(BigEndianReader Reader, int MapVersion, Map map)
        {
            Floor = (Reader.ReadSByte() * 10);
            if (Floor == -1280)
                return;
            if (MapVersion >= 9)
            {
                var tmp_bytes = Reader.ReadShort();
                Mov = (tmp_bytes & 1) == 0;
                NonWalkableDuringFight = (tmp_bytes & 2) != 0;
                NonWalkableDuringRP = (tmp_bytes & 4) != 0;
                Los = (tmp_bytes & 8) == 0;
                Blue = (tmp_bytes & 16) != 0;
                Red = (tmp_bytes & 32) != 0;
                Visible = (tmp_bytes & 64) != 0;
                FarmCell = (tmp_bytes & 128) != 0;
                if (MapVersion >= 10)
                {
                    HavenbagCell = (tmp_bytes & 256) != 0;
                    TopArrow = (tmp_bytes & 512) != 0;
                    BottomArrow = (tmp_bytes & 1024) != 0;
                    RightArrow = (tmp_bytes & 2048) != 0;
                    LeftArrow = (tmp_bytes & 4096) != 0;
                }
                else
                {
                    TopArrow = (tmp_bytes & 256) != 0;
                    BottomArrow = (tmp_bytes & 512) != 0;
                    RightArrow = (tmp_bytes & 1024) != 0;
                    LeftArrow = (tmp_bytes & 2048) != 0;
                }
                if (TopArrow)
                    map.TopArrowCells.Add(CellId);
                if (BottomArrow)
                    map.BottomArrowCells.Add(CellId);
                if (RightArrow)
                    map.RightArrowCells.Add(CellId);
                if (LeftArrow)
                    map.LeftArrowCells.Add(CellId);
            }
            else
            {
                LosMov = Reader.ReadByte();
                Los = (LosMov & 2) >> 1 == 1;
                Mov = (LosMov & 1) == 1;
                Visible = (LosMov & 64) >> 6 == 1;
                FarmCell = (LosMov & 32) >> 5 == 1;
                Blue = (LosMov & 16) >> 4 == 1;
                Red = (LosMov & 8) >> 3 == 1;
                NonWalkableDuringRP = (LosMov & 128) >> 7 == 1;
                NonWalkableDuringFight = (LosMov & 4) >> 2 == 1;
            }
            Speed = Reader.ReadSByte();
            MapChangeData = Reader.ReadSByte();

            if (MapVersion > 5)
                MoveZone = Reader.ReadByte();
            if (MapVersion > 7 && MapVersion < 9)
            {
                var tmpBits = Reader.ReadSByte();
                Arrow = 15 & tmpBits;
            }

            if (useTopArrow())
                map.TopArrowCells.Add(CellId);

            if (useBottomArrow())
                map.BottomArrowCells.Add(CellId);

            if (useLeftArrow())
                map.LeftArrowCells.Add(CellId);

            if (useRightArrow())
                map.RightArrowCells.Add(CellId);
        }

        public CellData(int cellId)
        {
            CellId = cellId;
        }

        public CellData()
        {
        }

        private bool useTopArrow()
        {
            if ((Arrow & 1) == 0)
                return false;
            return true;
        }

        private bool useBottomArrow()
        {
            if ((Arrow & 2) == 0)
                return false;
            return true;
        }

        private bool useLeftArrow()
        {
            if ((Arrow & 4) == 0)
                return false;
            return true;
        }

        private bool useRightArrow()
        {
            if ((Arrow & 8) == 0)
                return false;
            return true;
        }

        public int CellId { get; set; }

        public bool Blue { get; set; }

        public bool FarmCell { get; set; }

        public bool Los { get; set; }

        public bool Mov { get; set; }

        public bool NonWalkableDuringFight { get; set; }

        public bool NonWalkableDuringRP { get; set; }

        public bool HavenbagCell { get; set; }

        public bool Red { get; set; }

        public bool Visible { get; set; }

        public bool TopArrow { get; set; }

        public bool BottomArrow { get; set; }

        public bool RightArrow { get; set; }

        public bool LeftArrow { get; set; }

        // Fields
        public int Floor;
        public byte LosMov;
        public int MapChangeData;
        public int MoveZone;
        public int Speed;
        public int Arrow = 0;
    }
}
