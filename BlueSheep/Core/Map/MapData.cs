using BlueSheep.Common.Data.D2o;
using BlueSheep.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Core.Map.Elements;
using BlueSheep.Data.D2p.Elements;
using BlueSheep.Interface.Text;
using BlueSheep.Common.Data;
using BlueSheep.Data.Pathfinding.Positions;
using BlueSheep.Common.Types;
using BlueSheep.Common.Protocol.Types.Game.Context.Roleplay;
using BlueSheep.Common.Protocol.Messages.Game.Context.Roleplay;

namespace BlueSheep.Core.Map
{
    public class MapData
    {
        #region Fields
        private AccountUC m_Account;
        public BlueSheep.Data.D2p.Map Data;

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

        public List<Common.Protocol.Types.Game.Interactive.InteractiveElement> InterElements { get; set; }

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
            get { return Players.FirstOrDefault(p => p.ContextualId == m_Account.CharacterBaseInformations.ObjectID); }
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
                    Elements.InteractiveElement interactiveElement = (Elements.InteractiveElement)element;
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
        public MapData(AccountUC Account)
        {
            m_Account = Account;
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
                    Monsters.Add(new MonsterGroup(m.StaticInfos, m.Disposition.CellId, m.ContextualId));
                }
                else if (i is GameRolePlayCharacterInformations)
                {
                    GameRolePlayCharacterInformations p = (GameRolePlayCharacterInformations)i;
                    Players.Add(p);
                }
                else if (i is GameRolePlayNpcInformations)
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
        public void ParseStatedElements(BlueSheep.Common.Protocol.Types.Game.Interactive.StatedElement[] statedElements)
        {
            StatedElements = statedElements.Select(elem => new StatedElement(elem.ElementCellId, elem.ElementId, elem.ElementState)).ToList();
        }

        /// <summary>
        /// Parse the interactive elements array from MapComplementaryInformationsDataMessage.
        /// </summary>
        public void ParseInteractiveElements(BlueSheep.Common.Protocol.Types.Game.Interactive.InteractiveElement[] interactiveElements)
        {
            foreach (BlueSheep.Common.Protocol.Types.Game.Interactive.InteractiveElement element in interactiveElements)
            {
                if (element.ElementTypeId == 85)
                    m_Account.Safe = element;
                Elements.InteractiveElement Ielement = new Elements.InteractiveElement((uint)element.ElementId, element.ElementTypeId, element.EnabledSkills, element.DisabledSkills);
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
            m_Account.SetStatus(Engine.Enums.Status.None);
        }

        /// <summary>
        /// Perform the correct action that we must do when we arrive on a new map.
        /// </summary>
        public void DoAction()
        {
            if (m_Account.Path != null && m_Account.Path.Launched)
            {
                m_Account.Log(new DebugTextInformation("[Path] DoAction"), 0);
                m_Account.Path.ParsePath();
            }
            if (m_Account.Path != null && m_Account.Path.Relaunch)
            {
                m_Account.Path.Start();
            }
            if (m_Account.petsList.Count != 0 && m_Account.checkBoxBegin.Checked == true)
            {
                m_Account.StartFeeding();
            }
            else if (m_Account.checkBoxBegin.Checked == true)
            {
                m_Account.Log(new ErrorTextInformation("Aucun familier dans l'inventaire."), 0);
                m_Account.checkBoxBegin.Checked = false;
            }
        }

        /// <summary>
        /// Actualize and display the new location.
        /// </summary>
        public void ParseLocation(int mapId, int subAreaId)
        {
            Data = BlueSheep.Data.D2p.MapsManager.FromId(mapId);
            Data.SubAreaId = subAreaId;
            DataClass subArea = GameData.GetDataObject(D2oFileEnum.SubAreas, subAreaId);
            string mapName = I18N.GetText((int)GameData.GetDataObject(D2oFileEnum.Areas, (int)subArea.Fields["areaId"]).Fields["nameId"]);
            string subAreaName = I18N.GetText((int)subArea.Fields["nameId"]);
            m_Account.ModifBar(5, 0, 0, "[" + X + ";" + Y + "]" + " " + mapName + " (" + subAreaName + ")");
            m_Account.ModifBar(5, 0, 0, mapName + " (" + subAreaName + ")");
        }

        /// <summary>
        /// Update the state of an interactive element.
        /// </summary>
        public void UpdateInteractiveElement(BlueSheep.Common.Protocol.Types.Game.Interactive.InteractiveElement element)
        {
            Elements.InteractiveElement Ielement = new Elements.InteractiveElement((uint)element.ElementId, element.TypeID, element.EnabledSkills, element.DisabledSkills);
            Tuple<Elements.InteractiveElement, int> temp = new Tuple<Elements.InteractiveElement, int>(null, 0);
            foreach (KeyValuePair<Elements.InteractiveElement, int> pair in InteractiveElements)
            {
                if (pair.Key.Id == Ielement.Id)
                    temp = new Tuple<Elements.InteractiveElement, int>(pair.Key, pair.Value);
            }
            if (temp.Item1 != null)
            {
                InteractiveElements.Remove(temp.Item1);
                InteractiveElements.Add(Ielement, temp.Item2);
            }
            else
                InteractiveElements.Add(Ielement, -1);
        }

        /// <summary>
        /// Update the state of an Stated element.
        /// </summary>
        public void UpdateStatedElement(BlueSheep.Common.Protocol.Types.Game.Interactive.StatedElement element)
        {
            Elements.StatedElement Selement = new Elements.StatedElement(element.ElementCellId, element.ElementId, element.ElementState);
            if (StatedElements.Find(s => s.Id == Selement.Id) != null)
                StatedElements.Find(s => s.Id == Selement.Id).State = Selement.State;
            else
                StatedElements.Add(Selement);
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
        public bool CanGatherElement(int id, int distance)
        {
            Elements.StatedElement element = StatedElements.Find(s => s.Id == id);
            if (element.State == 0 || element.State == 2)
                return false;
            if (distance <= 1 && distance >= 0)
                return true;
            MapPoint characterPoint = new MapPoint(Character.Disposition.CellId);
            if (element != null)
            {
                MapPoint elementPoint = new MapPoint((int)element.CellId);
                List<MapPoint> goodPointsList = GetListPointAtGoodDistance(characterPoint, elementPoint, m_Account.Inventory.WeaponRange);
                if (goodPointsList.Count > 0)
                {
                    foreach (MapPoint mp in goodPointsList)
                        m_Account.Log(new DebugTextInformation("[CanGatherElement] GoodPoints -> " + mp.CellId), 0);
                    m_Account.Log(new DebugTextInformation("[CanGatherElement] Player CellId ? " + characterPoint.CellId), 0);
                    var selectedPoint = goodPointsList.FirstOrDefault((point) => point.CellId == characterPoint.CellId);
                    if (selectedPoint != null)
                        return true;
                }
                m_Account.Gather.BanElementId(id);
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
                return (int)pl.Disposition.CellId;
            Elements.StatedElement st = StatedElements.Find(s => s.Id == contextId);
            if (st != null)
                return (int)st.CellId;
            GameRolePlayNpcInformations npc = Npcs.Find(s => s.ContextualId == contextId);
            if (npc != null)
                return (int)npc.Disposition.CellId;
            GameRolePlayActorInformations other = Others.Find(s => s.ContextualId == contextId);
            if (other != null)
                return (int)other.Disposition.CellId;
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

        public List<MapPoint> GetListPointAtGoodDistance(MapPoint characterPoint, MapPoint elementPoint, int weaponRange)
        {
            List<MapPoint> list = new List<MapPoint>();
            int num = -1;
            int direction = 1;
            while (true)
            {
                int i = 0;
                while (i < weaponRange)
                {
                    i += 1;
                    MapPoint nearestCellInDirection = elementPoint.GetNearestCellInDirection(direction, i);
                    if (nearestCellInDirection.IsInMap() && Data.IsWalkable(nearestCellInDirection.CellId))
                    {
                        int dist = characterPoint.DistanceToCell(nearestCellInDirection);
                        if ((num == -1) || (num >= dist))
                        {
                            if (dist < num)
                                list.Clear();
                            num = dist;
                            list.Add(nearestCellInDirection);
                        }
                        break;
                    }
                }
                direction = (direction + 2);
                if (direction > 7)
                    return list;
            }
        }
        #endregion
    }
}
