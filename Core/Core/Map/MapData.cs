using MageBot.DataFiles.Data.D2o;
using System;
using System.Collections.Generic;
using System.Linq;
using MageBot.Core.Map.Elements;
using MageBot.DataFiles.Data.D2p.Elements;
using Util.Util.Text.Log;
using MageBot.DataFiles.Data.Pathfinding.Positions;
using MageBot.Protocol.Types.Game.Context.Roleplay;
using MageBot.Core.Monsters;
using MageBot.Util.Enums.Internal;
using System.Collections.Concurrent;

namespace MageBot.Core.Map
{
    public class MapData
    {
        #region Fields
        private Account.Account Account;
        public MageBot.DataFiles.Data.D2p.Map Data;

        /// <summary>
        /// List containing all the players on the map.
        /// </summary>
        public ConcurrentDictionary<double, GameRolePlayCharacterInformations> Players = new ConcurrentDictionary<double, GameRolePlayCharacterInformations>();

        /// <summary>
        /// List containing all the group of monsters on the map.
        /// </summary>
        public ConcurrentDictionary<double, MonsterGroup> Monsters = new ConcurrentDictionary<double, MonsterGroup>();

        /// <summary>
        /// List containing all the Npc on the map.
        /// </summary>
        public ConcurrentDictionary<double, GameRolePlayNpcInformations> Npcs = new ConcurrentDictionary<double, GameRolePlayNpcInformations>();

        /// <summary>
        /// List containing all the other actors that we can't identify.
        /// </summary>
        public ConcurrentDictionary<double, GameRolePlayActorInformations> Others = new ConcurrentDictionary<double, GameRolePlayActorInformations>();

        /// <summary>
        /// Dict containing the interactive elements and their cellId. If no cellId found, cellId equals -1.
        /// </summary>
        public ConcurrentDictionary<uint, InteractiveElement> InteractiveElements = new ConcurrentDictionary<uint, InteractiveElement>();

        /// <summary>
        /// List containing all the stated elements on the map.
        /// </summary>
        public ConcurrentDictionary<double, StatedElement> StatedElements = new ConcurrentDictionary<double, StatedElement>();

        public int LastMapId;
        #endregion

        #region Properties
        public int WorldId
        {
            get
            {
                DataClass map = GameData.GetDataObject(D2oFileEnum.MapPositions, Id);
                return (int)map.Fields["worldMap"];
            }
        }

        public string Pos
        {
            get { return Convert.ToString(X) + ',' + Convert.ToString(Y); }
        }

        public int X
        {
            get
            {
                DataClass map = GameData.GetDataObject(D2oFileEnum.MapPositions, Id);
                return (int)map.Fields["posX"];
            }
        }

        public int Y
        {
            get
            {
                DataClass mapp = GameData.GetDataObject(D2oFileEnum.MapPositions, Id);
                return (int)mapp.Fields["posY"];
            }
        }

        public GameRolePlayCharacterInformations Character
        {
            get { return Players.ContainsKey(Account.CharacterBaseInformations.ObjectID) ? Players[Account.CharacterBaseInformations.ObjectID] : null; }
        }

        public int Id
        {
            get { return (Data.Id); }
        }

        public Dictionary<int, UsableElement> UsableElements
        {
            get
            {
                Dictionary<int, UsableElement> usableElements = new Dictionary<int, UsableElement>();
                foreach (var element in InteractiveElements)
                {
                    InteractiveElement interactiveElement = element.Value;
                    if (interactiveElement.EnabledSkills.Count >= 1)
                    {
                        foreach (var layer in Data.Layers)
                        {
                            foreach (var cell in layer.Cells)
                            {
                                foreach (var layerElement in cell.Elements)
                                {
                                    if (layerElement is GraphicalElement)
                                    {
                                        GraphicalElement graphicalElement = (GraphicalElement)layerElement;
                                        if (graphicalElement.Identifier == interactiveElement.Id)
                                        {
                                            usableElements.Add((int)interactiveElement.Id, new UsableElement(cell.CellId, interactiveElement, interactiveElement.EnabledSkills));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return usableElements;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="account">Compte associé</param>
        public MapData(Account.Account Account)
        {
            this.Account = Account;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Parse the actors array from MapComplementaryInformationsDataMessage.
        /// </summary>
        public void ParseActors(GameRolePlayActorInformations[] actors)
        {
            foreach (GameRolePlayActorInformations i in actors)
            {
                if (i is GameRolePlayGroupMonsterInformations)
                {
                    GameRolePlayGroupMonsterInformations m = (GameRolePlayGroupMonsterInformations)i;
                    var mg = new MonsterGroup(m.StaticInfos, m.Disposition.CellId, m.ContextualId);
                    Monsters.AddOrUpdate(m.ContextualId,
                                         mg,
                                         (key, oldValue) => mg);
                }
                else if (i is GameRolePlayCharacterInformations)
                {
                    GameRolePlayCharacterInformations p = (GameRolePlayCharacterInformations)i;
                    Players.AddOrUpdate(p.ContextualId, p,
                                        (key, oldValue) => p);
                }
                else if (i is GameRolePlayNpcInformations)
                {
                    GameRolePlayNpcInformations npc = (GameRolePlayNpcInformations)i;
                    Npcs.AddOrUpdate(npc.ContextualId, npc,
                                     (key, oldValue) => npc);
                }
                else
                {
                    if (!Others.ContainsKey(i.ContextualId))
                        Others.AddOrUpdate(i.ContextualId, i,
                                           (key, oldValue) => i);
                }
            }
        }

        /// <summary>
        /// Parse the stated elements array from MapComplementaryInformationsDataMessage.
        /// </summary>
        public void ParseStatedElements(Protocol.Types.Game.Interactive.StatedElement[] statedElements)
        {
            var StatedOnMapInEnumerable = statedElements.Where(element => element.OnCurrentMap)
                .Select(elem => new StatedElement(elem.ElementCellId, elem.ElementId, elem.ElementState))
                .ToDictionary(elem => (double)elem.Id);
            StatedElements = new ConcurrentDictionary<double, StatedElement>(StatedOnMapInEnumerable);
        }

        /// <summary>
        /// Parse the interactive elements array from MapComplementaryInformationsDataMessage.
        /// </summary>
        public void ParseInteractiveElements(Protocol.Types.Game.Interactive.InteractiveElement[] interactiveElements)
        {
            var elementsOnTheMap = interactiveElements.Where(element => element.OnCurrentMap).ToList();
            foreach (Protocol.Types.Game.Interactive.InteractiveElement element in elementsOnTheMap)
            {
                if (element.ElementTypeId == 85)
                    Account.Safe = new InteractiveElement(element);
                InteractiveElement Ielement = new InteractiveElement((uint)element.ElementId, element.ElementTypeId, element.EnabledSkills, element.DisabledSkills);
                InteractiveElements.AddOrUpdate(Ielement.Id, Ielement,
                                                (key, oldValue) => Ielement);
                if (Ielement.EnabledSkills.Count > 0)
                {
                    //Ielement.CellId = Data.Layers.Select(
                    //    elem => elem.Cells.FirstOrDefault(cell =>
                    //        cell.Elements.Where(e => e is GraphicalElement &&
                    //        ((GraphicalElement)e).Identifier == Ielement.Id).Count() > 0)
                    //        ).FirstOrDefault().CellId;
                    foreach (var layer in Data.Layers)
                    {
                        foreach (var cell in layer.Cells)
                        {
                            foreach (var layerElement in cell.Elements)
                            {
                                if (layerElement is GraphicalElement)
                                {
                                    GraphicalElement graphicalElement = (GraphicalElement)layerElement;
                                    if (graphicalElement.Identifier == Ielement.Id)
                                        Ielement.CellId = cell.CellId;
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Clear all the fields before parsing MapComplementaryInformationsDataMessage.
        /// </summary>
        public void Clear()
        {
            if (Data != null)
                LastMapId = Id;
            Players.Clear();
            Monsters.Clear();
            Npcs.Clear();
            Others.Clear();
            InteractiveElements.Clear();
            StatedElements.Clear();
        }

        /// <summary>
        /// Perform the correct action that we must do when we arrive on a new map.
        /// </summary>
        public void DoAction()
        {
            //if (Account.Path != null && Account.Path.Launched)
            //{
            //    Account.Log(new DebugTextInformation("[Path] DoAction"), 0);
            //    Account.Path.ParsePath();
            //}
            //if (Account.Path != null && Account.Config.RelaunchPath)
            //{
            //    Account.Path.Start();
            //}
            if (Account.PetsList.Count != 0 && Account.Config.Begin)
            {
                Account.StartFeeding();
            }
            else if (Account.Config.Begin)
            {
                Account.Log(new ErrorTextInformation("No pet in inventory."), 0);
                Account.Config.Begin = false;
            }
        }

        /// <summary>
        /// Actualize and display the new location.
        /// </summary>
        public void ParseLocation(int mapId, int subAreaId)
        {
            Data = DataFiles.Data.D2p.MapsManager.FromId(mapId);
            Data.SubAreaId = subAreaId;
            DataClass subArea = GameData.GetDataObject(D2oFileEnum.SubAreas, subAreaId);
            string mapName = DataFiles.Data.I18n.I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.Areas, (int)subArea.Fields["areaId"]).Fields["nameId"]);
            string subAreaName = DataFiles.Data.I18n.I18N.GetText((int)subArea.Fields["nameId"]);
            Account.ModifBar(5, 0, 0, "[" + X + ";" + Y + "]" + " " + mapName + " (" + subAreaName + ")");
        }

        /// <summary>
        /// Update the state of an interactive element.
        /// </summary>
        public void UpdateInteractiveElement(Protocol.Types.Game.Interactive.InteractiveElement element)
        {
            if (element.OnCurrentMap)
            {
                var ielement = new InteractiveElement((uint)element.ElementId,
                                                element.ElementTypeId,
                                                element.EnabledSkills,
                                                element.DisabledSkills);
                if (InteractiveElements.ContainsKey((uint)element.ElementId))
                {
                    ielement.CellId = InteractiveElements[(uint)element.ElementId].CellId;
                    InteractiveElements[(uint)element.ElementId] = ielement;
                }
                else
                {
                    InteractiveElements.AddOrUpdate(ielement.Id, ielement,
                                                    (key, oldValue) => ielement);
                }
            }
        }

        /// <summary>
        /// Update the state of an Stated element.
        /// </summary>
        public void UpdateStatedElement(Protocol.Types.Game.Interactive.StatedElement element)
        {
            if (element.OnCurrentMap)
            {
                StatedElement Selement = new StatedElement(element.ElementCellId, element.ElementId, element.ElementState);
                if (StatedElements.FirstOrDefault(s => s.Key == Selement.Id).Value != null)
                {
                    StatedElements.FirstOrDefault(s => s.Key == Selement.Id).Value.State = Selement.State;
                }
                else
                {
                    StatedElements.AddOrUpdate(Selement.Id, Selement,
                                               (key, oldValue) => Selement);
                }
            }
        }

        /// <summary>
        /// Remove an entity with the specified contextual id.
        /// </summary>
        public void Remove(double id)
        {
            GameRolePlayCharacterInformations gri = new GameRolePlayCharacterInformations();
            Players.TryRemove(id, out gri);
            GameRolePlayActorInformations grai = new GameRolePlayActorInformations();
            Others.TryRemove(id, out grai);
            MonsterGroup mg = new MonsterGroup();
            Monsters.TryRemove(id, out mg);
        }

        /// <summary>
        /// Return whether or not we can gather an element at the specified distance.
        /// </summary>
        public bool CanGatherElement(int id)
        {
            StatedElement statedElement = StatedElements[id];
            InteractiveElement interactiveElement =
                InteractiveElements[(uint)id];
            if (!interactiveElement.IsUsable
                || (statedElement.State != 0
                    && statedElement.State != 1))
                return false;
            MapPoint characterPoint = new MapPoint(Character.Disposition.CellId);
            if (statedElement != null)
            {
                MapPoint elementPoint = new MapPoint((int)statedElement.CellId);
                List<MapPoint> goodPointsList = GetListPointAtGoodDistance(characterPoint, elementPoint, Account.Inventory.JobAbstractWeaponRange);
                if (goodPointsList.Count > 0)
                {
                    foreach (MapPoint mp in goodPointsList)
                        Account.Log(new DebugTextInformation("[CanGatherElement] GoodPoints -> " + mp.CellId), 0);
                    Account.Log(new DebugTextInformation("[CanGatherElement] Player CellId ? " + characterPoint.CellId), 0);
                    var selectedPoint = goodPointsList.FirstOrDefault();
                    if (selectedPoint != null)
                    {
                        Account.Map.MoveToCell(selectedPoint.CellId);
                        return true;
                    }
                }
                Account.Gather.BanElementId(id);
                return false;
            }
            return false;
        }

        /// <summary>
        /// Return if an entity is on the cell or not.
        /// </summary>
        public bool NoEntitiesOnCell(int cellId)
        {
            bool players = Players.Values.FirstOrDefault(p => p.Disposition.CellId == cellId) == null;
            bool npcs = Npcs.Values.FirstOrDefault(n => n.Disposition.CellId == cellId) == null;
            bool others = Others.Values.FirstOrDefault(o => o.Disposition.CellId == cellId) == null;
            return (players && npcs && others);
        }

        /// <summary>
        /// Returns if a cell is free or not
        /// </summary>
        public bool NothingOnCell(int cellId)
        {
            if (Data.IsWalkable(cellId))
                return NoEntitiesOnCell(cellId);
            return false;
        }

        /// <summary>
        /// Returns the cellId from the contextualId.
        /// </summary>
        public int GetCellFromContextId(double contextId)
        {
            GameRolePlayCharacterInformations pl = Players.ContainsKey(contextId) ? Players[contextId] : null;
            if (pl != null)
                return pl.Disposition.CellId;
            StatedElement st = StatedElements.ContainsKey(contextId) ? StatedElements[contextId] : null;
            if (st != null)
                return (int)st.CellId;
            GameRolePlayNpcInformations npc = Npcs.ContainsKey(contextId) ? Npcs[contextId] : null;
            if (npc != null)
                return npc.Disposition.CellId;
            GameRolePlayActorInformations other = Others.ContainsKey(contextId) ? Others[contextId] : null;
            if (other != null)
                return other.Disposition.CellId;
            return 0;
        }

        /// <summary>
        /// Update the cellId of an entity.
        /// </summary>
        public void UpdateEntityCell(double id, int cell)
        {
            if (Monsters.ContainsKey(id))
                Monsters[id].m_cellId = cell;
            else if (Players.ContainsKey(id))
                Players[id].Disposition.CellId = (short)cell;
            else if (Others.ContainsKey(id))
                Others[id].Disposition.CellId = (short)cell;
        }

        public List<MapPoint> GetListPointAtGoodDistance(MapPoint characterPoint, MapPoint elementPoint, int maxDistance)
        {
            List<MapPoint> list = new List<MapPoint>();
            for (int i = 0; i < maxDistance; i++)
            {
                for (int direction = 0; direction < 8; direction++)
                {
                    MapPoint nearestCellInDirection = elementPoint.GetNearestCellInDirection(direction, i);
                    if (nearestCellInDirection.IsInMap() && Data.IsWalkable(nearestCellInDirection.CellId))
                    {
                        list.Add(nearestCellInDirection);
                    }
                }
            }
            return list.GroupBy(elem => elem.CellId).Select(g => g.FirstOrDefault()).ToList()
                   .OrderBy(item => item.DistanceTo(elementPoint)).ToList();
        }
        #endregion
    }
}
