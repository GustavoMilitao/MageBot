namespace BlueSheep.Data.D2p
{
    public class CellData
    {
        // Methods
        internal void Init(BigEndianReader Reader, int MapVersion)
        {
            Floor = (Reader.ReadSByte() * 10);
            if ((Floor != -1280))
            {
                LosMov = Reader.ReadSByte();
                Speed = Reader.ReadSByte();
                MapChangeData = Reader.ReadByte();
                if ((MapVersion > 5))
                {
                    MoveZone = Reader.ReadByte();
                }
                if ((MapVersion > 7))
                {
                    int tmp = Reader.ReadSByte();
                    Arrow = 15 & tmp;
                }
            }
        }

        public bool Blue()
        {
            return (((LosMov & 16) >> 4) == 1);
        }

        public bool FarmCell()
        {
            return (((LosMov & 32) >> 5) == 1);
        }

        public bool Los()
        {
            return (((LosMov & 2) >> 1) == 1);
        }

        public bool Mov()
        {
            return ((LosMov & 1) == 1);
        }

        public bool NonWalkableDuringFight()
        {
            return (((LosMov & 4) >> 2) == 1);
        }

        public bool Red()
        {
            return (((LosMov & 8) >> 3) == 1);
        }

        public bool Visible()
        {
            return (((LosMov & 64) >> 6) == 1);
        }

        public bool TopArrow()
        {
            return ((Arrow & 1) != 0);
        }

        public bool BottomArrow()
        {
            return ((Arrow & 2) != 0);
        }

        public bool RightArrow()
        {
            return ((Arrow & 4) != 0);
        }

        public bool LeftArrow()
        {
            return ((Arrow & 8) != 0);
        }

        // Fields
        public int Floor;
        public int LosMov;
        public int MapChangeData;
        public int MoveZone;
        public int Speed;
        public int Arrow = 0;
    }
}
