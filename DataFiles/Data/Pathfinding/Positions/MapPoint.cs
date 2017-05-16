using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DataFiles.Data.Pathfinding.Positions
{
    public class MapPoint
    {
        // Methods
        /// <summary>Créait un nouveau MapPoint grâce à l'identifiant de la cellule sur la map</summary>
        /// <param name="CellId">Identifiant de la cellule</param>
        public MapPoint(int CellId)
        {
            this.CellId = 0;
            X = 0;
            Y = 0;
            if (!MapPoint.IsInit)
            {
                MapPoint.Init();
            }
            this.CellId = CellId;
            if (MapPoint.dictionary_0.ContainsKey(CellId))
            {
                MapPoint point = MapPoint.dictionary_0[CellId];
                Y = point.Y;
                X = point.X;
            }
        }

        /// <summary>Créait un nouveau MapPoint grâce aux coordonnées d'une cellule de la map</summary>
        /// <param name="X">Abscisse</param>
        /// <param name="Y">Ordonnée</param>
        public MapPoint(int X, int Y)
        {
            CellId = 0;
            this.X = 0;
            this.Y = 0;
            this.X = X;
            this.Y = Y;
            CellId = Convert.ToInt32(Math.Round(Convert.ToDouble(((((X - Y) * 14) + Y) + Math.Floor(Convert.ToDouble((Convert.ToDouble((X - Y)) / 2)))))));
        }

        public int AdvancedOrientationTo(MapPoint point, bool b)
        {
            int num = (point.X - X);
            int num2 = (Y - point.Y);
            int num3 = Convert.ToInt32(Math.Round(Convert.ToDouble((Math.Floor(Convert.ToDouble(((Math.Acos((Convert.ToDouble(num) / Math.Sqrt((Math.Pow(Convert.ToDouble(num), 2) + Math.Pow(Convert.ToDouble(num2), 2))))) * 180) / 3.14159265358979))) * ((point.Y > Y) ? Convert.ToDouble(-1) : Convert.ToDouble(1))))));
            if (b)
            {
                num3 = Convert.ToInt32(Math.Round(Convert.ToDouble(((Math.Round(Math.Floor(Convert.ToDouble((Convert.ToDouble(num3) / 90)))) * 2) + 1))));
            }
            else
            {
                num3 = Convert.ToInt32(Math.Round(Convert.ToDouble((Math.Round(Math.Floor(Convert.ToDouble((Convert.ToDouble(num3) / 45)))) + 1))));
            }
            if ((num3 < 0))
            {
                num3 = (num3 + 8);
            }
            return num3;
        }

        /// <summary>Obtient la distance entre ce point et le point donné</summary>
        /// <param name="Point">Point donc on veut connaître la distance par rapport à ce MapPoint</param>
        /// <returns>La distance entre les deux points</returns>
        public int DistanceTo(MapPoint Point)
        {
            return Convert.ToInt32(Math.Round(Math.Sqrt((Math.Pow(Convert.ToDouble((Point.X - X)), 2) + Math.Pow(Convert.ToDouble((Point.Y - Y)), 2)))));
        }

        /// <summary>Obtient la distance entre ce point et le point donné</summary>
        /// <param name="Point">Point donc on veut connaître la distance par rapport à ce MapPoint</param>
        /// <returns>La distance entre les deux points</returns>
        public int DistanceToCell(MapPoint Point)
        {
            return (Math.Abs(Convert.ToInt32((X - Point.X))) + Math.Abs(Convert.ToInt32((Y - Point.Y))));
        }

        /// <summary>Obtient le prochain point dans une dirrection donnée et à une distance donnée</summary>
        /// <param name="Direction">Dirrection point à récupérer par rapport à ce MapPoint</param>
        /// <param name="i">Distance point à récupérer par rapport à ce MapPoint</param>
        /// <returns>Le MapPoint correspondant aux critères donnés</returns>
        public MapPoint GetNearestCellInDirection(int Direction, int i = 1)
        {
            switch (Direction)
            {
                case 0:
                    return new MapPoint((X + i), (Y + i));
                case 1:
                    return new MapPoint((X + i), Y);
                case 2:
                    return new MapPoint((X + i), (Y - i));
                case 3:
                    return new MapPoint(X, (Y - i));
                case 4:
                    return new MapPoint((X - i), (Y - i));
                case 5:
                    return new MapPoint((X - i), Y);
                case 6:
                    return new MapPoint((X - i), (Y + i));
                case 7:
                    return new MapPoint(X, (Y + i));
            }
            return null;
        }

        /// <summary>Indique si le point se trouve sur la map</summary>
        /// <returns>True si le point est sur la map, sinon False</returns>
        public bool IsInMap()
        {
            return ((((X + Y) >= 0) && ((X - Y) >= 0)) && (((X - Y) < 40) && ((X + Y) < 0x1c)));
        }

        /// <summary>Donne l'orientation du point vers le point donné</summary>
        /// <param name="Point">Point dont on veut connaitre l'orientation par rapport à ce MapPoint</param>
        /// <returns>La valeur de l'orientation vers le point donné</returns>
        public int OrientationTo(MapPoint Point)
        {
            int num = (Point.X > X) ? 1 : (Point.X < X) ? -1 : 0;
            int num2 = (Point.Y > Y) ? 1 : (Point.Y < Y) ? -1 : 0;
            if (((num == 1) && (num2 == 1)))
            {
                return 0;
            }
            if (((num == 1) && (num2 == 0)))
            {
                return 1;
            }
            if (((num == 1) && (num2 == -1)))
            {
                return 2;
            }
            if (((num == 0) && (num2 == -1)))
            {
                return 3;
            }
            if (((num == -1) && (num2 == -1)))
            {
                return 4;
            }
            if (((num == -1) && (num2 == 0)))
            {
                return 5;
            }
            if (((num == -1) && (num2 == 1)))
            {
                return 6;
            }
            if (((num == 0) && (num2 == 1)))
            {
                return 7;
            }
            return -1;
        }

        /// <summary>Initialise les MapPoint</summary>
        /// <remarks>Se fait automatiquement</remarks>
        private static void Init()
        {
            MapPoint.IsInit = true;
            lock (MapPoint.ChecklockObject)
            {
                int num = 0;
                int num2 = 0;
                int key = 0;
                int num4 = 0;
                int i = 0;
                for (i = 0; i <= 20 - 1; i++)
                {
                    num4 = 0;
                    while ((num4 < 14))
                    {
                        if (!MapPoint.dictionary_0.ContainsKey(key))
                        {
                            MapPoint.dictionary_0.Add(key, new MapPoint((num + num4), (num2 + num4)));
                        }
                        else
                        {
                            MapPoint.dictionary_0[key] = new MapPoint((num + num4), (num2 + num4));
                        }
                        key += 1;
                        num4 += 1;
                    }
                    num += 1;
                    for (num4 = 0; num4 <= 14 - 1; num4++)
                    {
                        if (!MapPoint.dictionary_0.ContainsKey(key))
                        {
                            MapPoint.dictionary_0.Add(key, new MapPoint((num + num4), (num2 + num4)));
                        }
                        else
                        {
                            MapPoint.dictionary_0[key] = new MapPoint((num + num4), (num2 + num4));
                        }
                        key += 1;
                    }
                    num2 -= 1;
                }
            }
        }


        // Fields
        private static bool IsInit = false;
        public int CellId;
        private static Dictionary<int, MapPoint> dictionary_0 = new Dictionary<int, MapPoint>();
        private static object ChecklockObject = RuntimeHelpers.GetObjectValue(new object());
        public int X;
        public int Y;
    }
}