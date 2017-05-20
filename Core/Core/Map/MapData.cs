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
        public List<GameRolePlayCharacterInformations> Players = new List<GameRolePlayCharacterInformations>();

        /// <summary>
        /// List containing all the group of monsters on the map.
        /// </summary>
        public List<MonsterGroup> Monsters = new List<MonsterGroup>();

        /// <summary>
        /// List containing all the Npc on the map.
        /// </summary>
        public List<GameRolePlayNpcInformations> Npcs = new List<GameRolePlayNpcInformations>();

        /// <summary>
        /// List containing all the other actors that we can't identify.
        /// </summary>
        public List<GameRolePlayActorInformations> Others = new List<GameRolePlayActorInformations>();

        /// <summary>
        /// Dict containing the interactive elements and their cellId. If no cellId found, cellId equals -1.
        /// </summary>
        public Dictionary<Elements.InteractiveElement, int> InteractiveElements = new Dictionary<Elements.InteractiveElement, int>();

        /// <summary>
        /// List containing all the stated elements on the map.
        /// </summary>
        public List<Elements.StatedElement> StatedElements = new List<Elements.StatedElement>();

        public List<Protocol.Types.Game.Interactive.InteractiveElement> InterElements { get; set; }

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
            get { return Players.FirstOrDefault(p => p.ContextualId == Account.CharacterBaseInformations.ObjectID); }
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
                foreach (var element in InteractiveElements.Keys)
                {
                    Elements.InteractiveElement interactiveElement = element;
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
                                            usableElements.Add((int)interactiveElement.Id, new Elements.UsableElement(cell.CellId, interactiveElement, interactiveElement.EnabledSkills));
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
                if (i is GameRolePlayGroupMonsterInformations
                    && !Monsters.Any(m => m.m_contextualId == i.ContextualId))
                {
                    GameRolePlayGroupMonsterInformations m = (GameRolePlayGroupMonsterInformations)i;
                    Monsters.Add(new MonsterGroup(m.StaticInfos, m.Disposition.CellId, m.ContextualId));
                }
                else if (i is GameRolePlayCharacterInformations
                    && !Players.Any(p => p.ContextualId == i.ContextualId))
                {
                    GameRolePlayCharacterInformations p = (GameRolePlayCharacterInformations)i;
                    Players.Add(p);
                }
                else if (i is GameRolePlayNpcInformations
                    && !Npcs.Any(n => n.ContextualId == i.ContextualId))
                {
                    GameRolePlayNpcInformations npc = (GameRolePlayNpcInformations)i;
                    Npcs.Add(npc);
                }
                else
                    Others.Add(i);
            }
        }

        /// <summary>
        /// Parse the stated elements array from MapComplementaryInformationsDataMessage.
        /// </summary>
        public void ParseStatedElements(MageBot.Protocol.Types.Game.Interactive.StatedElement[] statedElements)
        {
            StatedElements =
                statedElements.Where(element => element.OnCurrentMap)
                .Select(elem => new StatedElement(elem.ElementCellId, elem.ElementId, elem.ElementState)).ToList();
        }

        /// <summary>
        /// Parse the interactive elements array from MapComplementaryInformationsDataMessage.
        /// </summary>
        public void ParseInteractiveElements(MageBot.Protocol.Types.Game.Interactive.InteractiveElement[] interactiveElements)
        {
            var elementsOnTheMap = interactiveElements.Where(element => element.OnCurrentMap).ToList();
            foreach (Protocol.Types.Game.Interactive.InteractiveElement element in elementsOnTheMap)
            {
                if (element.ElementTypeId == 85)
                    Account.Safe = new InteractiveElement(element);
                InteractiveElement Ielement = new InteractiveElement((uint)element.ElementId, element.ElementTypeId, element.EnabledSkills, element.DisabledSkills);
                InteractiveElements.Add(Ielement, -1);
                if (Ielement.EnabledSkills.Count > 0)
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
                                    if (graphicalElement.Identifier == Ielement.Id)
                                        InteractiveElements[Ielement] = cell.CellId;
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
            Account.SetStatus(Status.None);
        }

        /// <summary>
        /// Perform the correct action that we must do when we arrive on a new map.
        /// </summary>
        public void DoAction()
        {
            if (Account.Path != null && Account.Path.Launched)
            {
                Account.Log(new DebugTextInformation("[Path] DoAction"), 0);
                Account.Path.ParsePath();
            }
            if (Account.Path != null && Account.Config.RelaunchPath)
            {
                Account.Path.Start();
            }
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
                InteractiveElement Ielement = new InteractiveElement((uint)element.ElementId, element.ElementTypeId, element.EnabledSkills, element.DisabledSkills);
                Tuple<InteractiveElement, int> temp = new Tuple<InteractiveElement, int>(null, 0);
                foreach (KeyValuePair<Elements.InteractiveElement, int> pair in InteractiveElements)
                {
                    if (pair.Key.Id == Ielement.Id)
                        temp = new Tuple<InteractiveElement, int>(pair.Key, pair.Value);
                }
                if (temp.Item1 != null)
                {
                    InteractiveElements.Remove(temp.Item1);
                    InteractiveElements.Add(Ielement, temp.Item2);
                }
                else
                    InteractiveElements.Add(Ielement, -1);
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
                if (StatedElements.Find(s => s.Id == Selement.Id) != null)
                    StatedElements.Find(s => s.Id == Selement.Id).State = Selement.State;
                else
                    StatedElements.Add(Selement);
            }
        }

        /// <summary>
        /// Remove an entity with the specified contextual id.
        /// </summary>
        public void Remove(double id)
        {
            Players.RemoveAll(p => p.ContextualId == id);
            Others.RemoveAll(o => o.ContextualId == id);
            Monsters.RemoveAll(m => m.m_contextualId == id);
        }

        /// <summary>
        /// Return whether or not we can gather an element at the specified distance.
        /// </summary>
        public bool CanGatherElement(int id)
        {
            StatedElement statedElement = StatedElements.Find(s => s.Id == id);
            InteractiveElement interactiveElement =
                InteractiveElements.Keys.ToList().Find(el => el.Id == id);
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
            bool players = Players.Find(p => p.Disposition.CellId == cellId) == null;
            bool npcs = Npcs.Find(n => n.Disposition.CellId == cellId) == null;
            bool others = Others.Find(o => o.Disposition.CellId == cellId) == null;
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
            GameRolePlayCharacterInformations pl = Players.Find(p => p.ContextualId == contextId);
            if (pl != null)
                return pl.Disposition.CellId;
            Elements.StatedElement st = StatedElements.Find(s => s.Id == contextId);
            if (st != null)
                return (int)st.CellId;
            GameRolePlayNpcInformations npc = Npcs.Find(s => s.ContextualId == contextId);
            if (npc != null)
                return npc.Disposition.CellId;
            GameRolePlayActorInformations other = Others.Find(s => s.ContextualId == contextId);
            if (other != null)
                return other.Disposition.CellId;
            return 0;
        }

        /// <summary>
        /// Update the cellId of an entity.
        /// </summary>
        public void UpdateEntityCell(double id, int cell)
        {
            if (Monsters.Find(m => m.m_contextualId == id) != null)
                Monsters.Find(m => m.m_contextualId == id).m_cellId = cell;
            else if (Players.Find(p => p.ContextualId == id) != null)
                Players.Find(p => p.ContextualId == id).Disposition.CellId = (short)cell;
            else if (Others.Find(p => p.ContextualId == id) != null)
                Others.Find(p => p.ContextualId == id).Disposition.CellId = (short)cell;
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
