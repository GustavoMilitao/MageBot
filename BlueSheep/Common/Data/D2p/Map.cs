using System;
using System.Collections.Generic;
using BlueSheep.Common.IO;

namespace BlueSheep.Data.D2p
{
    public class Map
    {
        // Methods
        public bool IsLineOfSight(int cellId)
        {
            return (((cellId >= 0) && (cellId < CellsCount)) && Cells[cellId].Los());
        }

        public bool IsWalkable(int cellId)
        {
            return (((cellId >= 0) && (cellId < CellsCount)) && Cells[cellId].Mov());
        }

        internal void Init(BigEndianReader reader)
        {
            byte b = reader.ReadByte();
            MapVersion = reader.ReadByte();
            Id = (int)reader.ReadUInt();
            if (MapVersion >= 7)
            {
                Encrypted = reader.ReadBoolean();
                EncryptedVersion = reader.ReadByte();
                int count = reader.ReadInt();
                if (Encrypted)
                {
                    byte[] buffer = CustomHex.ToArray(CustomHex.FromString("649ae451ca33ec53bbcbcc33becf15f4", false));
                    byte[] buffer2 = reader.ReadBytes(count);
                    int num7 = (buffer2.Length - 1);
                    int n = 0;
                    while (n <= num7)
                    {
                        buffer2[n] = Convert.ToByte((buffer2[n] ^ buffer[(n % buffer.Length)]));
                        n += 1;
                    }
                    reader = new BigEndianReader(buffer2);
                }
            }
            RelativeId = (int)reader.ReadUInt();
            MapType = reader.ReadByte();
            SubAreaId = reader.ReadInt();
            TopNeighbourId = reader.ReadInt();
            BottomNeighbourId = reader.ReadInt();
            LeftNeighbourId = reader.ReadInt();
            RightNeighbourId = reader.ReadInt();
            ShadowBonusOnEntities = reader.ReadInt();
            if (MapVersion >= 3)
            {
                BackgroundRed = reader.ReadByte();
                BackgroundGreen = reader.ReadByte();
                BackgroundBlue = reader.ReadByte();
            }
            if (MapVersion >= 4)
            {
                ZoomScale = (Convert.ToDouble(reader.ReadUShort()) / 100);
                ZoomOffsetX = reader.ReadShort();
                ZoomOffsetY = reader.ReadShort();
            }
            UseLowPassFilter = reader.ReadBoolean();
            UseReverb = reader.ReadBoolean();
            if (UseReverb)
            {
                PresetId = reader.ReadInt();
            }
            BackgroundsCount = reader.ReadByte();
            int backgroundsCount = BackgroundsCount;
            int i = 1;
            while (i <= backgroundsCount)
            {
                Fixture item = new Fixture();
                item.Init(reader);
                BackgroundFixtures.Add(item);
                i += 1;
            }
            ForegroundsCount = reader.ReadByte();
            int foregroundsCount = ForegroundsCount;
            int j = 1;
            while (j <= foregroundsCount)
            {
                Fixture fixture2 = new Fixture();
                fixture2.Init(reader);
                ForegroundFixtures.Add(fixture2);
                j += 1;
            }
            CellsCount = 560;
            reader.ReadInt();
            GroundCRC = reader.ReadInt().ToString();
            LayersCount = reader.ReadByte();
            int layersCount = LayersCount;
            int k = 1;
            //while (k <= layersCount)
            //{
            //    Layer layer = new Layer();
            //    layer.Init(reader, MapVersion);
            //    Layers.Add(layer);
            //    k += 1;
            //}
            //int cellsCount = CellsCount;
            //int m = 1;
            //uint oldMvtSys = 0;
            //while (m <= cellsCount)
            //{
            //    CellData data = new CellData();
            //    data.Init(reader, MapVersion);
            //    if (oldMvtSys == 0)
            //    {
            //        oldMvtSys = (uint)data.MoveZone;
            //    }
            //    if (data.MoveZone != oldMvtSys)
            //    {
            //        IsUsingNewMovementSystem = true;
            //    }
            //    Cells.Add(data);
            //    m += 1;
            //}
            // TODO: Refactor .D2P Layer and Cell Reading
        }

        // Fields
        public int BackgroundBlue;
        public List<Fixture> BackgroundFixtures = new List<Fixture>();
        public int BackgroundGreen;
        public int BackgroundRed;
        public int BackgroundsCount;
        public int BottomNeighbourId;
        public List<CellData> Cells = new List<CellData>();
        public int CellsCount;
        public bool Encrypted;
        public int EncryptedVersion;
        public List<Fixture> ForegroundFixtures = new List<Fixture>();
        public int ForegroundsCount;
        public string GroundCRC;
        public int Id;
        public bool IsUsingNewMovementSystem;
        public List<Layer> Layers = new List<Layer>();
        public int LayersCount;
        public int LeftNeighbourId;
        public int MapType;
        public int MapVersion;
        public int PresetId;
        public int RelativeId;
        public int RightNeighbourId;
        public int ShadowBonusOnEntities;
        public int SubAreaId;
        public int TopNeighbourId;
        public bool UseLowPassFilter;
        public bool UseReverb;
        public int ZoomOffsetX;
        public int ZoomOffsetY;
        public double ZoomScale;
    }
}
