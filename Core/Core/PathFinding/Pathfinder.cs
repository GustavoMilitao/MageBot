using MageBot.DataFiles.Data.Pathfinding.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using MageBot.Core.Map;
using MageBot.Core.Fight;

namespace MageBot.DataFiles.Data.Pathfinding
{
    public class Pathfinder
    {
        // Methods
        public Pathfinder(MapData map)
        {
            MapData = map;
            if (MapData.Id == 2561)
            {
                ListCellIdFighters.Add(53);
            }
        }


        public MovementPath FindPath(int FromCell, int ToCell, bool ate)
        {
            return FindPath(FromCell, ToCell, ate, true);
        }

        public MovementPath FindPath(int FromCell, int ToCell)
        {
            return FindPath(FromCell, ToCell, true, true);
        }

        public MovementPath FindPath(int FromCell, int ToCell, bool ate, bool adc)
        {
            MovPath = new MovementPath();
            MovPath.CellStart = new MapPoint(FromCell);
            MovPath.CellEnd = new MapPoint(ToCell);
            AllowDiag = adc;
            AllowDiagCornering = adc;
            AllowTroughEntity = ate;

            StartPathfinding(new MapPoint(FromCell), new MapPoint(ToCell));
            ProcessPathfinding();

            return MovPath;
        }

        public void StartPathfinding(MapPoint startP, MapPoint endP)
        {
            Start = startP;
            End = endP;
            StartX = startP.X;
            StartY = startP.Y;
            EndX = endP.X;
            EndY = endP.Y;

            StartPoint = new MapPoint(startP.X, startP.Y);
            EndPoint = new MapPoint(endP.X, endP.Y);

            AuxEndPoint = StartPoint;
            AuxEndX = StartPoint.X;
            AuxEndY = StartPoint.Y;

            DistanceToEnd = StartPoint.DistanceToCell(EndPoint);

            for (int y = -19; y <= MaxY; y++)
            {
                for (int x = 0; x <= MaxX; x++)
                {
                    MapStatus.Add(new CellInfo(0, null, false, false, x, y));
                }
            }
            OpenList = new List<OpenSquare>();
            OpenSquare(StartY, StartX, null, 0, 0, false);
        }

        public void ProcessPathfinding()
        {
            int actualX = 0;
            int actualY = 0;
            int speed = 0;
            int moveCost = 0;

            bool isDownRightEnd = false;
            bool isDownRightStart = false;
            bool isTopRightEnd = false;
            bool isTopRightStart = false;

            MapPoint actualPoint = null;
            int actualDistanceToEnd = 0;
            double heuristic = 0;
            uint square = 0;

            if (OpenList.Count > 0 && !(IsClosed(EndY, EndX)))
            {
                square = NearerSquare();
                NowY = OpenList[(int)square].Y;
                NowX = OpenList[(int)square].X;
                PreviousCellId = (new MapPoint(NowX, NowY)).CellId;
                CloseSquare(NowY, NowX);

                for (actualY = NowY - 1; actualY <= NowY + 1; actualY++)
                {
                    for (actualX = NowX - 1; actualX <= NowX + 1; actualX++)
                    {
                        if ((new MapPoint(actualX, actualY)).IsInMap())
                        {
                            if (actualY >= MinY && actualY < MaxY && actualX >= MinX && actualX < MaxX && !(actualY == NowY && actualX == NowX) && ((AllowDiag) || actualY == NowY || actualX == NowX && ((AllowDiagCornering) || actualY == NowY || actualX == NowX || (PointMov(NowX, NowY, PreviousCellId, AllowTroughEntity)) || (PointMov(actualX, NowY, PreviousCellId, AllowTroughEntity)))))
                            {
                                if (!(!(PointMov(NowX, actualY, PreviousCellId, AllowTroughEntity)) && !(PointMov(actualX, NowY, PreviousCellId, AllowTroughEntity)) && !IsFighting && (AllowDiag)))
                                {
                                    if (PointMov(actualX, actualY, PreviousCellId, AllowTroughEntity))
                                    {
                                        if (!(IsClosed(actualY, actualX)))
                                        {
                                            if (actualX == EndX && actualY == EndY)
                                            {
                                                speed = 1;
                                            }
                                            else
                                            {
                                                speed = (int)GetCellSpeed((new MapPoint(actualX, actualY)).CellId, AllowTroughEntity);
                                            }

                                            moveCost = GetCellInfo(NowY, NowX).MovementCost + ((actualY == NowY || actualX == NowX) ? HVCost : DCost) * speed;

                                            if (AllowTroughEntity)
                                            {
                                                isDownRightEnd = actualX + actualY == EndX + EndY;
                                                isDownRightStart = actualX + actualY == StartX + StartY;
                                                isTopRightEnd = actualX - actualY == EndX - EndY;
                                                isTopRightStart = actualX - actualY == StartX - StartY;
                                                actualPoint = new MapPoint(actualX, actualY);

                                                if (!isDownRightEnd && !isTopRightEnd || !isDownRightStart && !isTopRightStart)
                                                {
                                                    moveCost = moveCost + actualPoint.DistanceToCell(EndPoint);
                                                    moveCost = moveCost + actualPoint.DistanceToCell(StartPoint);
                                                }

                                                if (actualX == EndX || actualY == EndY)
                                                {
                                                    moveCost = moveCost - 3;
                                                }
                                                if ((isDownRightEnd) || (isTopRightEnd) || actualX + actualY == NowX + NowY || actualX - actualY == NowX - NowY)
                                                {
                                                    moveCost = moveCost - 2;
                                                }
                                                if (actualX == StartX || actualY == StartY)
                                                {
                                                    moveCost = moveCost - 3;
                                                }
                                                if ((isDownRightStart) || (isTopRightStart))
                                                {
                                                    moveCost = moveCost - 2;
                                                }

                                                actualDistanceToEnd = actualPoint.DistanceToCell(EndPoint);
                                                if (actualDistanceToEnd < DistanceToEnd)
                                                {
                                                    if (actualX == EndX || actualY == EndY || actualX + actualY == EndX + EndY || actualX - actualY == EndX - EndY)
                                                    {
                                                        AuxEndPoint = actualPoint;
                                                        AuxEndX = actualX;
                                                        AuxEndY = actualY;
                                                        DistanceToEnd = actualDistanceToEnd;
                                                    }
                                                }
                                            }

                                            if (IsOpened(actualY, actualX))
                                            {
                                                if (moveCost < GetCellInfo(actualY, actualX).MovementCost)
                                                {
                                                    OpenSquare(actualY, actualX, new[] { NowY, NowX }, moveCost, 0, true);
                                                }
                                            }
                                            else
                                            {
                                                heuristic = Convert.ToDouble(HeuristicCost) * Math.Sqrt((EndY - actualY) * (EndY - actualY) + (EndX - actualX) * (EndX - actualX));
                                                OpenSquare(actualY, actualX, new[] { NowY, NowX }, moveCost, heuristic, false);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                ProcessPathfinding();
            }
            else
            {
                EndPathfinding();
            }
        }

        public void EndPathfinding()
        {
            List<MapPoint> mapsArray = new List<MapPoint>();
            int parentY = 0;
            int parentX = 0;
            MapPoint btwPoint = null;
            List<MapPoint> tempArray = new List<MapPoint>();
            int i = 0;
            int actualX = 0;
            int actualY = 0;
            int thirdX = 0;
            int thirdY = 0;
            int btwX = 0;
            int btwY = 0;
            bool endPointClosed = IsClosed(EndY, EndX);

            if (!endPointClosed)
            {
                EndPoint = AuxEndPoint;
                EndX = AuxEndX;
                EndY = AuxEndY;
                endPointClosed = true;
                MovPath.CellEnd = EndPoint;
            }
            PreviousCellId = -1;
            if (endPointClosed)
            {
                NowX = EndX;
                NowY = EndY;

                while (!(NowX == StartX) || !(NowY == StartY))
                {
                    mapsArray.Add(new MapPoint(NowX, NowY));
                    parentY = GetCellInfo(NowY, NowX).Parent[0];
                    parentX = GetCellInfo(NowY, NowX).Parent[1];
                    NowX = parentX;
                    NowY = parentY;
                }
                mapsArray.Add(StartPoint);
                if (AllowDiag)
                {
                    for (i = 0; i < mapsArray.Count; i++)
                    {
                        tempArray.Add(mapsArray[i]);
                        PreviousCellId = mapsArray[i].CellId;
                        if ((mapsArray.Count > i + 2 && !((mapsArray[i + 2] == null)) && (mapsArray[i].DistanceToCell(mapsArray[i + 2]) == 1)) && (!(IsChangeZone(mapsArray[i].CellId, mapsArray[i + 1].CellId))) && !(IsChangeZone(mapsArray[i + 1].CellId, mapsArray[i + 2].CellId)))
                        {
                            i += 1;
                        }
                        else
                        {
                            if ((mapsArray.Count > i + 3 && !((mapsArray[i + 3] == null))) && mapsArray[i].DistanceToCell(mapsArray[i + 3]) == 2)
                            {
                                actualX = mapsArray[i].X;
                                actualY = mapsArray[i].Y;
                                thirdX = mapsArray[i + 3].X;
                                thirdY = mapsArray[i + 3].Y;
                                btwX = actualX + (int)Math.Round((thirdX - actualX) / 2.0);
                                btwY = actualY + (int)Math.Round((thirdY - actualY) / 2.0);
                                btwPoint = new MapPoint(btwX, btwY);
                                if ((PointMov(btwX, btwY, PreviousCellId, true)) && GetCellSpeed(btwPoint.CellId, AllowTroughEntity) < 2)
                                {
                                    tempArray.Add(btwPoint);
                                    PreviousCellId = btwPoint.CellId;
                                    i += 2;
                                }
                            }
                            else
                            {
                                if (mapsArray.Count > i + 2 && !((mapsArray[i + 2] == null)) && mapsArray[i].DistanceToCell(mapsArray[i + 2]) == 2)
                                {
                                    actualX = mapsArray[i].X;
                                    actualY = mapsArray[i].Y;
                                    thirdX = mapsArray[i + 2].X;
                                    thirdY = mapsArray[i + 2].Y;
                                    btwX = mapsArray[i + 1].X;
                                    btwY = mapsArray[i + 1].Y;

                                    if (actualX + actualY == thirdX + thirdY && !(actualX - actualY == btwX - btwY) && !(IsChangeZone((new MapPoint(actualX, actualY)).CellId, (new MapPoint(btwX, btwY)).CellId)) && !(IsChangeZone((new MapPoint(btwX, btwY)).CellId, (new MapPoint(thirdX, thirdY)).CellId)))
                                    {
                                        i += 1;
                                    }
                                    else
                                    {
                                        if (actualX - actualY == thirdX - thirdY && !(actualX - actualY == btwX - btwY) && !(IsChangeZone((new MapPoint(actualX, actualY)).CellId, (new MapPoint(btwX, btwY)).CellId)) && !(IsChangeZone((new MapPoint(btwX, btwY)).CellId, (new MapPoint(thirdX, thirdY)).CellId)))
                                        {
                                            i += 1;
                                        }
                                        else
                                        {
                                            if (actualX == thirdX && !(actualX == btwX) && GetCellSpeed((new MapPoint(actualX, btwY)).CellId, AllowTroughEntity) < 2 && (PointMov(actualX, btwY, PreviousCellId, AllowTroughEntity)))
                                            {
                                                btwPoint = new MapPoint(actualX, btwY);
                                                tempArray.Add(btwPoint);
                                                PreviousCellId = btwPoint.CellId;
                                                i += 1;
                                            }
                                            else
                                            {
                                                if (actualY == thirdY && !(actualY == btwY) && GetCellSpeed((new MapPoint(btwX, actualY)).CellId, AllowTroughEntity) < 2 && (PointMov(btwX, actualY, PreviousCellId, AllowTroughEntity)))
                                                {
                                                    btwPoint = new MapPoint(btwX, actualY);
                                                    tempArray.Add(btwPoint);
                                                    PreviousCellId = btwPoint.CellId;
                                                    i += 1;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    mapsArray = tempArray;
                }
                if (mapsArray.Count == 1)
                {
                    mapsArray = new List<MapPoint>();
                }
                mapsArray.Reverse();
                MovementPathFromArray(mapsArray.ToArray());
            }
        }


        public bool PointMov(int x, int y, int cellId)
        {
            return PointMov(x, y, cellId, true);
        }

        public bool PointMov(int x, int y)
        {
            return PointMov(x, y, -1, true);
        }

        public bool PointMov(int x, int y, int cellId, bool troughtEntities)
        {
            bool isNewSystem = MapData.Data.IsUsingNewMovementSystem;
            MapPoint actualPoint = new MapPoint(x, y);
            MageBot.DataFiles.Data.D2p.CellData fCellData = null;
            MageBot.DataFiles.Data.D2p.CellData sCellData = null;
            bool mov = false;
            int floor = 0;

            if (actualPoint.IsInMap())
            {
                fCellData = MapData.Data.Cells[actualPoint.CellId];
                mov = ((fCellData.Mov) && (!IsFighting || !fCellData.NonWalkableDuringFight));

                if (!((mov == false)) && isNewSystem && cellId != -1 && cellId != actualPoint.CellId)
                {
                    sCellData = (MapData).Data.Cells[cellId];
                    floor = Math.Abs(Math.Abs(fCellData.Floor) - Math.Abs(sCellData.Floor));
                    if (!(sCellData.MoveZone == fCellData.MoveZone) && floor > 0 && sCellData.MoveZone == fCellData.MoveZone && fCellData.MoveZone == 0 && floor > 11)
                    {
                        mov = false;
                    }
                    if (!troughtEntities)
                    {
                        int count = 0;
                        count += MapData.Monsters.Values.Where((e) => e.m_cellId == actualPoint.CellId).ToList().Count;
                        if (count > 0)
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                mov = false;
            }
            return mov;
        }

        public bool IsChangeZone(int fCell, int sCell)
        {
            MageBot.DataFiles.Data.D2p.Map data = MapData.Data;
            return data.Cells[fCell].MoveZone != data.Cells[sCell].MoveZone && System.Math.Abs(data.Cells[fCell].Floor) == System.Math.Abs(data.Cells[sCell].Floor);
        }

        private double GetCellSpeed(int cellId, bool throughEntities)
        {
            MageBot.DataFiles.Data.D2p.Map data = MapData.Data;
            var speed = data.Cells[cellId].Speed;
            MapPoint cell = new MapPoint(cellId);

            if (throughEntities)
            {
                if (!(MapData.NoEntitiesOnCell(cellId)))
                {
                    return 20;
                }

                if (speed >= 0)
                {
                    return 1 + (5 - speed);
                }

                return 1 + (11 + Math.Abs(speed));
            }

            var cost = 1.0D;
            MapPoint adjCell = null;

            if (!(MapData.NoEntitiesOnCell(cellId)))
            {
                cost += 0.3;
            }

            adjCell = new MapPoint(cell.X + 1, cell.Y);
            if (adjCell != null && !(MapData.NoEntitiesOnCell(adjCell.CellId)))
            {
                cost += 0.3;
            }

            adjCell = new MapPoint(cell.X, cell.Y + 1);
            if (adjCell != null && !(MapData.NoEntitiesOnCell(adjCell.CellId)))
            {
                cost += 0.3;
            }

            adjCell = new MapPoint(cell.X - 1, cell.Y);
            if (adjCell != null && !(MapData.NoEntitiesOnCell(adjCell.CellId)))
            {
                cost += 0.3;
            }

            adjCell = new MapPoint(cell.X, cell.Y - 1);
            if (adjCell != null && !(MapData.NoEntitiesOnCell(adjCell.CellId)))
            {
                cost += 0.3;
            }

            return cost;
        }

        public bool IsOpened(int y, int x)
        {
            return GetCellInfo(y, x).Opened;
        }

        public bool IsClosed(int y, int x)
        {
            CellInfo cellInfo = GetCellInfo(y, x);
            if ((cellInfo == null) || !cellInfo.Closed)
            {
                return false;
            }
            return true;
        }

        public uint NearerSquare()
        {
            uint y = 0;
            double distance = 9999999;
            double tempDistance = 0;

            for (int tempY = 0; tempY < OpenList.Count; tempY++)
            {
                tempDistance = GetCellInfo(OpenList[tempY].Y, OpenList[tempY].X).Heuristic + GetCellInfo(OpenList[tempY].Y, OpenList[tempY].X).MovementCost;
                if (tempDistance <= distance)
                {
                    distance = tempDistance;
                    y = Convert.ToUInt32(tempY);
                }
            }
            return y;
        }

        public void CloseSquare(int y, int x)
        {
            OpenList.RemoveAll((os) => os.X == x && os.Y == y);
            CellInfo cell = GetCellInfo(y, x);
            cell.Opened = false;
            cell.Closed = true;
        }

        public void OpenSquare(int y, int x, int[] parent, int moveCost, double heuristic, bool newSquare)
        {
            if (!newSquare)
            {
                foreach (OpenSquare op in OpenList)
                {
                    if (op.Y == y && op.X == x)
                    {
                        newSquare = true;
                        break;
                    }
                }
            }

            if (!newSquare)
            {
                OpenList.Add(new OpenSquare(y, x));
                MapStatus.RemoveAll((c) => c.X == x && c.Y == y);
                MapStatus.Add(new CellInfo(heuristic, null, true, false, x, y));
            }

            CellInfo cell = GetCellInfo(y, x);
            cell.Parent = parent;
            cell.MovementCost = moveCost;
        }

        public void MovementPathFromArray(MapPoint[] squares)
        {
            PathElement path = null;

            for (var i = 0; i <= squares.Length - 2; i++)
            {
                path = new PathElement();
                path.Cell = squares[i];
                path.Orientation = squares[i].OrientationTo(squares[i + 1]);
                MovPath.Cells.Add(path);
            }
            MovPath.Compress();
        }

        public void SetFight(List<BFighter> Fighters, int MovementPoints)
        {
            foreach (BFighter fighter in Fighters)
            {
                if (fighter.IsAlive)
                    ListCellIdFighters.Add(fighter.CellId);
            }
            MovePoint = MovementPoints;
            IsFighting = true;
        }

        public CellInfo GetCellInfo(int y, int x)
        {
            CellInfo cell = null;
            try
            {
                cell = MapStatus.First((ci) => ci.X == x && ci.Y == y);
            }
            catch
            {
                cell = null;
            }
            return cell;
        }

        // Fields
        private bool IsFighting = false;
        private List<int> ListCellIdFighters = new List<int>();
        private int MovePoint = -1;

        private MapData MapData;
        //private Map Map;

        private List<CellInfo> MapStatus = new List<CellInfo>();
        private List<OpenSquare> OpenList = new List<OpenSquare>();

        private MapPoint Start;
        private MapPoint End;
        private MapPoint StartPoint;
        private MapPoint EndPoint;
        private MapPoint AuxEndPoint;
        private MovementPath MovPath;
        private int HVCost = 10;
        private int DCost = 20;
        private object HeuristicCost = 10;
        private bool AllowDiag;
        private bool AllowTroughEntity = true;
        private bool AllowDiagCornering = false;

        private int StartX;
        private int StartY;
        private int EndX;
        private int EndY;
        private int AuxEndX;
        private int AuxEndY;
        private int NowX;
        private int NowY;

        private int DistanceToEnd;
        private int PreviousCellId;

        private int MinX = 0;
        private int MaxX = 34;
        private int MinY = -19;
        private int MaxY = 14;
    }

}
