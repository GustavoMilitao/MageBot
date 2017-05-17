using DataFiles.Data.D2o;
using BlueSheep.Protocol.Enums;
using BlueSheep.Protocol.Messages.Game.Inventory.Preset;
using BlueSheep.Protocol.Types.Game.Actions.Fight;
using BlueSheep.Protocol.Types.Game.Character.Characteristic;
using BlueSheep.Protocol.Types.Game.Context.Fight;
using DataFiles.Data.Pathfinding;
using DataFiles.Data.Pathfinding.Positions;
using BlueSheep.Util.Enums.Internal;
using Util.Util.Text.Log;
using BlueSheep.Util.Enums.Fight;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BlueSheep.Core.Monsters;

namespace BlueSheep.Core.Fight
{
    public class FightData
    {
        #region Fields
        #region Dictionary
        public Dictionary<DateTime, int> XpWon { get; set; }
        public Dictionary<string, int> WinLoseDic { get; set; }
        public Dictionary<int, int> DurationByEffect { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, int> LastTurnLaunchBySpell { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, int> TotalLaunchBySpell { get; set; } = new Dictionary<int, int>();
        public Dictionary<int, Dictionary<int, int>> TotalLaunchByCellBySpell { get; set; } = new Dictionary<int, Dictionary<int, int>>();
        private Dictionary<long, List<BFighter>> M_Summons { get; set; } = new Dictionary<long, List<BFighter>>();
        #endregion

        #region Public Fields
        public Account.Account Account { get; set; }
        public List<BFighter> Fighters { get; set; } = new List<BFighter>();
        public int TimeoutFight { get; set; }
        public List<BFighter> DeadEnnemies { get; set; } = new List<BFighter>();
        public int TurnId { get; set; }

        public bool StartFightWithItemSet { get; set; }
        public byte PresetStartUpId { get; set; }
        public bool EndFightWithItemSet { get; set; }
        public sbyte PresetEndUpId { get; set; }
        public bool IsFighterTurn { get; set; } = false;
        public bool IsFightStarted { get; set; } = false;
        public bool WaitForReady { get; set; } = false;
        public bool IsDead { get; set; } = false;

        public List<FightOptionsEnum> Options { get; set; } = new List<FightOptionsEnum>();
        public Stopwatch Watch { get; set; } = new Stopwatch();
        public MonsterGroup FollowingGroup { get; set; }
        private Dictionary<string, int> Boss { get; set; }
        #endregion
        #endregion

        #region Properties
        public BFighter Fighter
        {
            get { return GetFighter((long)Account.CharacterBaseInformations.ObjectID); }
        }

        public int MonsterNumber
        {
            get { return Fighters.FindAll(f => f.TeamId != Fighter.TeamId).ToList().Count; }
        }

        public bool IsFollowingGroup
        {
            get
            {
                if (FollowingGroup != null)
                    return true;
                else
                    return false;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructeur (override)
        /// </summary>
        /// <param name="account">Compte associé</param>
        public FightData(Account.Account Account)
        {
            this.Account = Account;
            XpWon = new Dictionary<DateTime, int>();
            WinLoseDic = new Dictionary<string, int>
            {
                { "Win", 0 },
                { "Lose", 0 }
            };
            XpWon.Add(DateTime.Today, 0);
            DataClass[] data = GameData.GetDataObjects(D2oFileEnum.Monsters);
            List<DataClass> b = data.ToList().FindAll(e => ((bool)e.Fields["isBoss"]) == true).ToList();
            Boss = new Dictionary<string, int>();
            foreach (DataClass d in b)
            {
                Boss.Add(DataFiles.Data.I18n.I18N.GetText((int)d.Fields["nameId"]), (int)d.Fields["id"]);
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns if we are currently hand to hand with the nearest monster or not.
        /// </summary>
        public bool IsHandToHand(int cell = 0)
        {
            if (cell == 0)
                cell = Fighter.CellId;
            MapPoint characterPoint = new MapPoint(cell);
            MapPoint targetPoint = new MapPoint(NearestMonster().CellId);

            if (characterPoint.DistanceToCell(targetPoint) <= 1)
                return true;

            return false;
        }

        /// <summary>
        /// Check if the target is targetable by the specified spell (with and without moving).
        /// </summary>
        /// <param name="spell">Spell to launch</param>
        /// <param name="target">Target</param>
        /// <returns>The cellId we need to move to. -1 if we can't use. 0 if we don't need to move.</returns>
        public int CanUseSpell(BSpell spell, BFighter target)
        {
            if (CanLaunchSpell(spell.SpellId) != SpellInabilityReason.None)
            {
                return -1;
            }

            // Use without move
            if (CanLaunchSpellOn(spell.SpellId, Fighter.CellId, target.CellId) == SpellInabilityReason.None)
            {
                return 0;
            }

            // Try with move
            int moveCell = -1;
            int distance = -1;
            foreach (int cell in GetReachableCells())
            {
                if (CanLaunchSpellOn(spell.SpellId, cell, target.CellId, true) == SpellInabilityReason.None)
                {
                    MapPoint characterPoint = new MapPoint(cell);
                    int tempDistance = characterPoint.DistanceToCell(new MapPoint(target.CellId));

                    if (tempDistance > distance || distance == -1)
                    {
                        distance = tempDistance;
                        moveCell = cell;
                    }
                }
            }
            return moveCell;
        }

        /// <summary>
        /// Add the summoner and its summon.
        /// </summary>
        public void AddSummon(long sourceId, BFighter summon)
        {
            Fighters.Add(summon);
            List<BFighter> summoned = new List<BFighter>();
            if (M_Summons.ContainsKey(sourceId))
            {
                M_Summons.TryGetValue(sourceId, out summoned);
                summoned.Add(summon);
                M_Summons.Remove(sourceId);
                M_Summons.Add(sourceId, summoned);
            }
            else
            {
                summoned.Add(summon);
                M_Summons.Add(sourceId, summoned);
            }
        }

        /// <summary>
        /// Get the summoner with the biggest number of summons.
        /// </summary>
        public BFighter GetSummoner()
        {
            Tuple<long, int> temp = new Tuple<long,int>(0, -1);
            foreach (KeyValuePair<long, List<BFighter>> pair in M_Summons)
            {
                if (pair.Value.Count > temp.Item2)
                {
                    temp = new Tuple<long, int>(pair.Key, pair.Value.Count);
                }
            }
            if (temp.Item1 != 0)
                return GetFighter(temp.Item1);
            else
                return null;
        }

        /// <summary>
        /// Removes the fighter from the fighters and add it to the dead ennemies' list.
        /// </summary>
        public void SetFighterDeath(long id)
        {
            BFighter fighter = GetFighter(id);
            DeadEnnemies.Add(fighter);
            if (!IsDead && fighter.Id == Fighter.Id)
            {
                Account.Log(new ErrorTextInformation("Personnage mort :'("), 0);
                IsDead = true;
            }
            else if (fighter.CreatureGenericId != 0)
            {
                Account.Log(new ActionTextInformation(fighter.Name + " est mort !"), 5);
            }
            Fighters.Remove(fighter);          
        }

        /// <summary>
        /// Set the specified effect.
        /// </summary>
        public void SetEffect(AbstractFightDispellableEffect effect, int actionId = -1)
        {
            if (effect is FightTemporaryBoostStateEffect m_effect1)
            {
                if (!IsDead && m_effect1.TargetId == Fighter.Id)
                {
                    if (DurationByEffect.ContainsKey(m_effect1.StateId))
                        DurationByEffect.Remove(m_effect1.StateId);
                    DurationByEffect.Add(m_effect1.StateId, effect.TurnDuration);
                }
            }
            else if (effect is FightTemporaryBoostEffect m_effect2)
            {
                if (actionId == 168)
                    Fighter.ActionPoints -= m_effect2.Delta;
                else if (actionId == 169)
                    Fighter.MovementPoints -= m_effect2.Delta;
                else if (!IsDead && actionId == 116 && m_effect2.TargetId == Fighter.Id)
                    Account.CharacterStats.Range.ContextModif -= m_effect2.Delta;

            }
        }

        /// <summary>
        /// Affect the specified variation on the specified target.
        /// </summary>
        public void SetPointsVariation(long targetId, int actionId, int delta)
        {
            BFighter fighter = GetFighter(targetId);
            if (fighter != null)
            {
                switch (actionId)
                {
                    case 101:
                    case 102:
                    case 120:
                        fighter.ActionPoints += delta;
                        break;
                    case 78:
                    case 127:
                    case 129:
                        fighter.MovementPoints += delta;
                        break;
                }
            }
        }

        /// <summary>
        /// Update the life points of the specified fighter.
        /// </summary>
        public void UpdateFighterLifePoints(long id, int delta)
        {
            BFighter fighter = GetFighter(id);
            if (fighter != null)
            {
                fighter.LifePoints += delta;
                if (fighter.Id == Fighter.Id)
                {
                    Account.ModifBar(2, Account.FightData.Fighter.MaxLifePoints, Account.FightData.Fighter.LifePoints, "Life");
                }
                Account.Log(new ActionTextInformation(fighter.Name + ": " + delta + "PV."), 5);
            }
            
        }

        /// <summary>
        /// Set a spell casted by the player.
        /// </summary>
        public void SetSpellCasted(long id, int spellId, int destinationCellId)
        {
            BFighter fighter = GetFighter(id);
            if (fighter != null && fighter.Id == Fighter.Id)
            {
                int spellLevel = -1;
                BSpell spell = Account.Spells.FirstOrDefault(s => s.SpellId == spellId);
                if (spell != null)
                    spellLevel = spell.Level;
                if (spellLevel != -1)
                {
                    DataClass spellData = GameData.GetDataObject(D2oFileEnum.Spells, spellId);
                    if (spellData != null)
                    {
                        uint spellLevelId = (uint)((ArrayList)spellData.Fields["spellLevels"])[spellLevel - 1];
                        DataClass spellLevelData = GameData.GetDataObject(D2oFileEnum.SpellLevels, (int)spellLevelId);
                        if (spellLevelData != null)
                        {
                            if ((int)spellLevelData.Fields["minCastInterval"] > 0 && !(LastTurnLaunchBySpell.ContainsKey(spellId)))
                                LastTurnLaunchBySpell.Add(spellId, (int)spellLevelData.Fields["minCastInterval"]);
                            if (TotalLaunchBySpell.ContainsKey(spellId)) //Si on a déjà utilisé ce sort ce tour
                                TotalLaunchBySpell[spellId] += 1;
                            else
                                TotalLaunchBySpell.Add(spellId, 1);
                            if (TotalLaunchByCellBySpell.ContainsKey(spellId)) //Si on a déjà utilisé ce sort ce tour
                            {
                                if (TotalLaunchByCellBySpell[spellId].ContainsKey(destinationCellId)) //Si on a déjà utilisé ce sort sur cette case
                                    TotalLaunchByCellBySpell[spellId][destinationCellId] += 1;
                                else
                                    TotalLaunchByCellBySpell[spellId].Add(destinationCellId, 1);
                            }
                            else
                            {
                                Dictionary<int, int> tempdico = new Dictionary<int, int>
                                {
                                    { destinationCellId, 1 }
                                };
                                TotalLaunchByCellBySpell.Add(spellId, tempdico);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Update the cellId of the specified fighter.
        /// </summary>
        public void UpdateFighterCell(long id, int cell)
        {
            BFighter fighter = GetFighter(id);
            if (fighter != null)
            {
                fighter.CellId = cell;
            }
        }

        /// <summary>
        /// Update state of a specified option.
        /// </summary>
        public void SetOption(bool state, int id)
        {
            if (!state && Options.Contains((FightOptionsEnum)id))
                Options.Remove((FightOptionsEnum)id);
            if (state && !Options.Contains((FightOptionsEnum)id))
                Options.Add((FightOptionsEnum)id);
        }

        /// <summary>
        /// Add a fighter to the fighter's list.
        /// </summary>
        public void AddFighter(GameFightFighterInformations informations)
        {
            if (informations is GameFightMonsterInformations infos)
            {
                Fighters.Add(new BFighter(informations.ContextualId, informations.Disposition.CellId, informations.Stats.ActionPoints, informations.Stats, informations.Alive, (int)informations.Stats.LifePoints, (int)informations.Stats.MaxLifePoints, informations.Stats.MovementPoints, informations.TeamId, infos.CreatureGenericId));
            }
            else
            {
                Fighters.Add(new BFighter(informations.ContextualId, informations.Disposition.CellId, informations.Stats.ActionPoints, informations.Stats, informations.Alive, (int)informations.Stats.LifePoints, (int)informations.Stats.MaxLifePoints, informations.Stats.MovementPoints, informations.TeamId, 0));
            }
            if (Fighter != null)
                Fighter.Name = Account.CharacterBaseInformations.Name;
        }

        /// <summary>
        /// Set the fight as started.
        /// </summary>
        public void FightStart()
        {
            WaitForReady = false;
            IsFightStarted = true;
            Account.Log(new ActionTextInformation("Beginning of the fight"), 2);
            Account.SetStatus(Status.Fighting);
            Watch.Restart();
        }

        /// <summary>
        /// Set the fight as stopped.
        /// </summary>
        public void FightStop()
        {
            Watch.Stop();
            WaitForReady = false;
            IsFighterTurn = false;
            IsFightStarted = false;
            IsDead = false;
            Account.Log(new ActionTextInformation("End fight ! (" + Watch.Elapsed.Minutes + " min, " + Watch.Elapsed.Seconds + " sec)"), 0);
            Watch.Reset();
            Account.SetStatus(Status.Busy);
            Reset();
            PerformAutoTimeoutFight(2000);
            if (EndFightWithItemSet)
            {
                sbyte id = PresetEndUpId;
                InventoryPresetUseMessage msg2 = new InventoryPresetUseMessage((byte)(id - 1));
                Account.SocketManager.Send(msg2);
                Account.Log(new ActionTextInformation("Fast equipment number " + Convert.ToString(id)), 5);
            }
            PulseRegen();
        }

        /// <summary>
        /// Set turn as started.
        /// </summary>
        public void TurnStarted(ulong id = 0)
        {
            if (!IsFightStarted)
                IsFightStarted = true;
            //if (id == m_Account.CharacterBaseInformations.id)
            //{
            //    IsFighterTurn = true;
            //}
            //else
            //    IsFighterTurn = false;
            IsFighterTurn = true;
        }

        /// <summary>
        /// Set turn as ended.
        /// </summary>
        public void TurnEnded(ulong id)
        {
            if (id == Account.CharacterBaseInformations.ObjectID)
            {
                int num4 = 0;
                List<int> list = new List<int>();
                IsFighterTurn = false;
                TotalLaunchBySpell.Clear(); //Nettoyage des variables de vérification de lancement d'un sort
                TotalLaunchByCellBySpell.Clear(); //Nettoyage des variables de vérification de lancement d'un sort
                for (int i = 0; i < DurationByEffect.Keys.Count; i++)
                {
                    Dictionary<int, int> durationPerEffect = DurationByEffect;
                    num4 = Enumerable.ElementAtOrDefault<int>(DurationByEffect.Keys, i);
                    durationPerEffect[num4] = (durationPerEffect[num4] - 1);
                    if (DurationByEffect[Enumerable.ElementAtOrDefault<int>(DurationByEffect.Keys, i)] <= 0)
                        list.Add(Enumerable.ElementAtOrDefault<int>(DurationByEffect.Keys, i));
                }
                while (list.Count > 0)
                {
                    DurationByEffect.Remove(list[0]);
                    list.RemoveAt(0);
                }
                for (int i = 0; i < LastTurnLaunchBySpell.Keys.Count; i++)
                {
                    Dictionary<int, int> dictionary = LastTurnLaunchBySpell;
                    num4 = Enumerable.ElementAtOrDefault<int>(LastTurnLaunchBySpell.Keys, i);
                    dictionary[num4] = (dictionary[num4] - 1);
                    if (LastTurnLaunchBySpell[Enumerable.ElementAtOrDefault<int>(LastTurnLaunchBySpell.Keys, i)] <= 0)
                        list.Add(Enumerable.ElementAtOrDefault<int>(LastTurnLaunchBySpell.Keys, i));
                }
                while (list.Count > 0)
                {
                    LastTurnLaunchBySpell.Remove(list[0]);
                    list.RemoveAt(0);
                }
                Account.Log(new ActionTextInformation("Fin du tour"), 5);
            }

        }

        /// <summary>
        /// Update the turnId.
        /// </summary>
        public void UpdateTurn(int roundNumber)
        {
            TurnId = roundNumber;
        }

        /// <summary>
        /// Clear all the fields.
        /// </summary>
        public void Reset(bool isFightStarted = false, bool canSayReady = false)
        {
            M_Summons.Clear();
            Fighters.Clear();
            DeadEnnemies.Clear();
            Options.Clear();
            TotalLaunchBySpell.Clear();
            LastTurnLaunchBySpell.Clear();
            TotalLaunchByCellBySpell.Clear();
            DurationByEffect.Clear();
            IsFightStarted = isFightStarted;
            WaitForReady = (!isFightStarted && canSayReady);
            FollowingGroup = null;
        }

        /// <summary>
        /// Perform the auto-timeout.
        /// </summary>
        public async void PerformAutoTimeoutFight(int originalTime)
        {
            await Account.PutTaskDelay(Convert.ToInt32(originalTime * TimeoutFight));
        }

        /// <summary>
        /// Returns the nearest monster from our player.
        /// </summary>
        public BFighter NearestMonster()
        {
            BFighter Fighterr = null;
            int SavDistance = -1;
            foreach (BFighter TestFighter in Fighters)
            {
                if (TestFighter.TeamId == Fighter.TeamId || TestFighter.IsAlive == false)
                    continue;
                int dist = DistanceFrom(TestFighter);
                if (((dist < SavDistance) || (SavDistance == -1)) && TestFighter != Fighter)
                {
                    SavDistance = dist;
                    Fighterr = TestFighter;
                }
            }
            if (Fighterr == null)
            {
                return null;
            }
            return Fighterr;
        }

        /// <summary>
        /// Returns the weakest monster in the fight.
        /// </summary>
        public BFighter WeakestMonster()
        {
            Tuple<int, BFighter> temp = new Tuple<int, BFighter>(0, null);
            foreach (BFighter f in Fighters)
            {
                if (temp.Item1 > f.LifePoints && f.TeamId != Fighter.TeamId)
                    temp = new Tuple<int, BFighter>(f.LifePoints, f);
            }
            return temp.Item2;
        }

        /// <summary>
        /// Returns the nearest monster from our player, in the specified list.
        /// </summary>
        public BFighter NearestMonster(List<BFighter> LFighters)
        {
            MapPoint CharacterPoint = new MapPoint(Fighter.CellId);
            BFighter Fighterr = null;
            int SavDistance = -1;
            foreach (BFighter TestFighter in LFighters)
            {
                if (TestFighter.TeamId == Fighter.TeamId || TestFighter.IsAlive == false)
                    continue;
                MapPoint TestFighterPoint = new MapPoint(TestFighter.CellId);
                int dist = new SimplePathfinder(Account.MapData).FindPath(CharacterPoint.CellId, TestFighterPoint.CellId).Cells.Count();
                dist += CharacterPoint.DistanceToCell(TestFighterPoint);
                if (((dist < SavDistance) || (SavDistance == -1)) && TestFighter != Fighter)
                {
                    SavDistance = dist;
                    Fighterr = TestFighter;
                }
            }
            if (Fighterr == null)
            {
                return null;
            }
            return Fighterr;
        }

        /// <summary>
        /// Returns the nearest ally from our player.
        /// </summary>
        public BFighter NearestAlly()
        {
            BFighter Fighterr = null;
            int SavDistance = -1;
            foreach (BFighter TestFighter in Fighters)
            {
                if (TestFighter.TeamId != Fighter.TeamId || TestFighter.IsAlive == false)
                    continue;
                int dist = DistanceFrom(TestFighter);
                if (((dist < SavDistance) || (SavDistance == -1)) && TestFighter != Fighter)
                {
                    SavDistance = dist;
                    Fighterr = TestFighter;
                }
            }
            if (Fighterr == null)
            {
                return null;
            }
            return Fighterr;
        }

        /// <summary>
        /// Returns the weakest ally in the fight.
        /// </summary>
        public BFighter WeakestAlly()
        {
            Tuple<int, BFighter> temp = new Tuple<int, BFighter>(0, null);
            foreach (BFighter f in Fighters)
            {
                if (temp.Item1 > f.LifePoints && f.TeamId == Fighter.TeamId)
                    temp = new Tuple<int, BFighter>(f.LifePoints, f);
            }
            return temp.Item2;
        }


        /// <summary>
        /// Returns the distance between our player and the specified fighter. Default is the nearest monster.
        /// </summary>
        public int DistanceFrom(BFighter fighter = null)
        {
            if (fighter == null)
                fighter = NearestMonster();
            MapPoint CharacterPoint = new MapPoint(Fighter.CellId);
            MapPoint TestFighterPoint = new MapPoint(fighter.CellId);
            int dist = new SimplePathfinder(Account.MapData).FindPath(fighter.CellId, TestFighterPoint.CellId).Cells.Count();
            dist += CharacterPoint.DistanceToCell(TestFighterPoint);
            return dist;
        }

        /// <summary>
        /// Returns the furthest cell from the specified fighter. Default is the nearest monster.
        /// </summary>
        public int FurthestCellFrom(BFighter fighter = null)
        {
            if (fighter == null)
                fighter = NearestMonster();
            List<int> ReachableCells = GetReachableCells();
            int CellId = -1;
            int SavDistance = -1;
            foreach (int ReachableCell in ReachableCells)
            {
                MapPoint ReachableCellPoint = new MapPoint(ReachableCell);
                int Distance = 0;
                Distance = (Distance + ReachableCellPoint.DistanceToCell(new MapPoint(fighter.CellId)));
                if (((SavDistance == -1) || (Distance > SavDistance)))
                {
                    CellId = ReachableCell;
                    SavDistance = Distance;
                }
            }
            return CellId;
        }

        /// <summary>
        /// Returns the nearest cell from the specified fighter. Default is the nearest monster.
        /// </summary>
        public int NearestCellFrom(BFighter fighter = null)
        {
            if (fighter == null)
                fighter = NearestMonster();
            List<int> ReachableCells = GetReachableCells();
            int CellId = -1;
            int SavDistance = -1;
            foreach (int ReachableCell in ReachableCells)
            {
                MapPoint ReachableCellPoint = new MapPoint(ReachableCell);
                int Distance = 0;
                Distance = (Distance + ReachableCellPoint.DistanceToCell(new MapPoint(fighter.CellId)));
                if (((SavDistance == -1) || (Distance < SavDistance)))
                {
                    CellId = ReachableCell;
                    SavDistance = Distance;
                }
            }
            return CellId;
        }

        /// <summary>
        /// Returns if a boss is in the fight or not.
        /// </summary>
        public bool BossInFight()
        {
            foreach (BFighter f in Fighters)
            {
                if (Boss.ContainsKey(f.Name) || Boss.ContainsValue(f.CreatureGenericId))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Returns if the specified cellId is walkable or not.
        /// </summary>
        public bool IsCellWalkable(int cellId)
        {
            if (Account.MapData.Data.IsWalkable(cellId))
            {
                var selectedFighter = Fighters.FirstOrDefault((f) => f.CellId == cellId || Account.MapData.Data.Cells[cellId].NonWalkableDuringFight);
                if (selectedFighter != null)
                    return false;
                else
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Clear the fighters list before synchronization with server.
        /// </summary>
        public void ClearFighters()
        {
            Fighters.Clear();
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Get the fighter by it's id.
        /// </summary>
        /// <param name="id">ID du fighter</param>
        /// <returns>BFighter fighter.</returns>
        private BFighter GetFighter(long id)
        {
            return Fighters.FirstOrDefault(f => f.Id == id);
        }

        /// <summary>
        /// Returns the number of summoned monsters by our character.
        /// </summary>
        private int GetInvokationNumber()
        {
            int num = 0;
            foreach (BFighter fighter in Fighters)
            {
                if (fighter.GameFightMinimalStats.Summoner == Fighter.Id)
                    num++;
            }
            return num;
        }

        /// <summary>
        /// Returns if any of the fighters are currently on the cellId or not.
        /// </summary>
        private bool IsFreeCell(int cellId)
        {
            return !Fighters.Any(f => f != null && f.CellId == cellId);
        }

        /// <summary>
        /// Returns the cells reachable by the player.
        /// </summary>
        private List<int> GetReachableCells()
        {
            List<int> listWalkableCells = new List<int>();
            MapPoint point = new MapPoint(Fighter.CellId);
            int movementPoints = Fighter.MovementPoints;

            for (int i = 0; i < 600; i++)
            {
                if (IsCellWalkable(i))
                {
                    MapPoint cellPoint = new MapPoint(i);
                    if (cellPoint.DistanceToCell(point) <= movementPoints)
                        listWalkableCells.Add(i);
                }
            }
            if (listWalkableCells.Contains(point.CellId))
                listWalkableCells.Add(point.CellId);

            return listWalkableCells;
        }

        /// <summary>
        /// Returns if the spectator mode is locked or not.
        /// </summary>
        private bool IsSpectatorAvailable()
        {
            foreach (FightOptionsEnum e in Options)
                if (e == FightOptionsEnum.FIGHT_OPTION_SET_SECRET)
                    return true;
            return false;
        }

        /// <summary>
        /// Returns if a spell is launchable on a specified spellId or not.
        /// </summary>
        /// <param name="spellId">ID du sort</param>
        /// <param name="characterCellId">CellId du personnage</param>
        /// <param name="cellId">CellId cible</param>
        /// <returns>SpellInabilityReasons: Unknown, ActionPoints, TooManyLaunch, Cooldown, TooManyInvocations, None </returns>
        private SpellInabilityReason CanLaunchSpellOn(int spellId, int characterCellId, int cellId, bool withMove = false)
        {
            if (!withMove)
            {
                SpellInabilityReason canLaunchSpell = CanLaunchSpell(spellId);
                if (canLaunchSpell != SpellInabilityReason.None)
                    return canLaunchSpell;
            }
            Inventory.Item weapon = Account.Inventory.Weapon;
            DataClass weaponData = null;

            DataClass spellData = GameData.GetDataObject(D2oFileEnum.Spells, spellId);
            ArrayList ids = (ArrayList)spellData.Fields["spellLevels"];
            int level = Account.Spells.FirstOrDefault(Spell => Spell.SpellId == spellId).Level;
            int id = Convert.ToInt32(ids[level - 1]);
            DataClass spellLevelsData = GameData.GetDataObject(D2oFileEnum.SpellLevels, id);

            if (spellLevelsData == null && spellId != -1) // spellId = -1 -> Use weapon.
                return SpellInabilityReason.Unknown;
            if (spellId == 0 && weapon != null)
                weaponData = GameData.GetDataObject(D2oFileEnum.Items, weapon.GID);


            MapPoint characterPoint = new MapPoint(characterCellId);
            MapPoint targetPoint = new MapPoint(cellId);
            int distanceToTarget = characterPoint.DistanceToCell(targetPoint);
            int minRange = (spellId != -1) ? (int)spellLevelsData.Fields["minRange"] : (int)weaponData.Fields["minRange"];
            if ((spellId != 0 && (bool)spellLevelsData.Fields["castInDiagonal"]) || (weaponData != null && !(bool)weaponData.Fields["castInLine"]))
                minRange = (minRange * 2);
            if (minRange < 0)
                minRange = 0;
            int maxRange = (spellId != 0) ? (int)spellLevelsData.Fields["range"] + ((bool)spellLevelsData.Fields["rangeCanBeBoosted"] ? (Account.CharacterStats.Range.ObjectsAndMountBonus + Account.CharacterStats.Range.ContextModif) : 0) : (int)spellLevelsData.Fields["range"];
            if ((spellId != 0 && (bool)spellLevelsData.Fields["castInDiagonal"]) || (weaponData != null && !(bool)weaponData.Fields["castInLine"]))
                maxRange = (maxRange * 2);
            if (maxRange < 0)
                maxRange = 0;
            if (distanceToTarget < minRange && distanceToTarget > 0)
                return SpellInabilityReason.MinRange;
            if (distanceToTarget > maxRange)
                return SpellInabilityReason.MaxRange;
            if (((spellId != 0 && (bool)spellLevelsData.Fields["castInLine"]) || (weaponData != null && (bool)weaponData.Fields["castInLine"]))
               && characterPoint.X != targetPoint.X &&
                characterPoint.Y != targetPoint.Y)
                return SpellInabilityReason.NotInLine;
            if ((spellId != 0 && (bool)spellLevelsData.Fields["castInDiagonal"]) || (weaponData != null && !(bool)weaponData.Fields["castInLine"]))
            {
                ArrayList list = Dofus1Line.GetLine(characterPoint.X, characterPoint.Y, targetPoint.X, targetPoint.Y);

                int i = 0;
                while (i < list.Count - 1)
                {
                    Dofus1Line.Point actualPoint = (Dofus1Line.Point)list[i];
                    Dofus1Line.Point nextPoint = (Dofus1Line.Point)list[i + 1];
                    i += 1;
                    if (actualPoint.X == nextPoint.X + 1 && actualPoint.Y == nextPoint.Y + 1)
                        continue;
                    else if (actualPoint.X == nextPoint.X - 1 && actualPoint.Y == nextPoint.Y - 1)
                        continue;
                    else if (actualPoint.X == nextPoint.X + 1 && actualPoint.Y == nextPoint.Y - 1)
                        continue;
                    else if (actualPoint.X == nextPoint.X - 1 && actualPoint.Y == nextPoint.Y + 1)
                        continue;
                    return SpellInabilityReason.NotInDiagonal;
                }
            }
            if (((spellId != 0 && (bool)spellLevelsData.Fields["castTestLos"] && distanceToTarget > 1)) || (weaponData != null && (bool)weaponData.Fields["castTestLos"]) && distanceToTarget > 1)
            {
                ArrayList list = Dofus1Line.GetLine(characterPoint.X, characterPoint.Y, targetPoint.X, targetPoint.Y);
                int i = 0;
                while (i < list.Count - 1)
                {
                    Dofus1Line.Point point3 = (Dofus1Line.Point)list[i];
                    MapPoint point4 = new MapPoint((int)Math.Round(Math.Floor(point3.X)), (int)Math.Round(Math.Floor(point3.Y)));
                    if (!(IsFreeCell(point4.CellId)) || !(Account.MapData.Data.IsLineOfSight(point4.CellId)))
                        return SpellInabilityReason.LineOfSight;
                    i += 1;
                }
            }
            if ((TotalLaunchByCellBySpell.ContainsKey(spellId) && TotalLaunchByCellBySpell[spellId].ContainsKey(targetPoint.CellId)) && TotalLaunchByCellBySpell[spellId][targetPoint.CellId] >= (int)spellLevelsData.Fields["maxCastPerTarget"] && (int)spellLevelsData.Fields["maxCastPerTarget"] > 0)
                return SpellInabilityReason.TooManyLaunchOnCell;
            if (IsFreeCell(cellId))
            {
                if ((bool)spellLevelsData.Fields["needTakenCell"])
                    return SpellInabilityReason.NeedTakenCell;
            }
            else if ((bool)spellLevelsData.Fields["needFreeCell"])
                return SpellInabilityReason.NeedFreeCell;
            return SpellInabilityReason.None;
        }

        /// <summary>
        /// Returns if a spell is launchable or not. Don't take the target in count.
        /// </summary>
        /// <param name="spellId">ID du sort</param>
        /// <returns>SpellInabilityReasons: Unknown, ActionPoints, TooManyLaunch, Cooldown, TooManyInvocations, None  </returns>
        private SpellInabilityReason CanLaunchSpell(int spellId)
        {
            Inventory.Item weapon = Account.Inventory.Weapon;
            DataClass weaponData = null;

            DataClass spellData = GameData.GetDataObject(D2oFileEnum.Spells, spellId);
            ArrayList ids = ((ArrayList)spellData.Fields["spellLevels"]);
            int level = 0;
            try
            {
                level = Account.Spells.FirstOrDefault(Spell => Spell.SpellId == spellId).Level;
            }
            catch (NullReferenceException)
            {
                Account.Log(new ErrorTextInformation("Le sort spécifié n'existe pas dans votre liste de sorts."), 0);
                return SpellInabilityReason.UnknownSpell;
            }
            int id = Convert.ToInt32(ids[level - 1]);
            DataClass spellLevelsData = GameData.GetDataObject(D2oFileEnum.SpellLevels, id);

            if (spellLevelsData == null && spellId != -1) // spellId = -1 -> Use weapon.
                return SpellInabilityReason.Unknown;
            if (spellId == 0 && weapon != null)
                weaponData = GameData.GetDataObject(D2oFileEnum.Items, weapon.GID);

            if ((spellId != 0 && Fighter.ActionPoints < (int)spellLevelsData.Fields["apCost"]) || (weaponData != null && Fighter.ActionPoints < (int)weaponData.Fields["apCost"]))
                return SpellInabilityReason.ActionPoints;

            if (TotalLaunchBySpell.ContainsKey(spellId) && TotalLaunchBySpell[spellId] >= (int)spellLevelsData.Fields["maxCastPerTurn"] && (int)spellLevelsData.Fields["maxCastPerTurn"] > 0)
                return SpellInabilityReason.TooManyLaunch;
            if (LastTurnLaunchBySpell.ContainsKey(spellId))
                return SpellInabilityReason.Cooldown;

            ArrayList listEffects = (ArrayList)spellLevelsData.Fields["effects"];
            //EffectInstanceDice
            if (((listEffects != null) && (listEffects.Count > 0)) && ((int)((DataClass)listEffects[0]).Fields["effectId"]) == 181)
            {
                CharacterCharacteristicsInformations stats = Account.CharacterStats;
                int total = stats.SummonableCreaturesBoost.Base + stats.SummonableCreaturesBoost.ObjectsAndMountBonus + stats.SummonableCreaturesBoost.AlignGiftBonus + stats.SummonableCreaturesBoost.ContextModif;
                if (GetInvokationNumber() >= total)
                    return SpellInabilityReason.TooManyInvocations;
            }
            ArrayList listOfStates = (ArrayList)spellLevelsData.Fields["statesRequired"];
            foreach (var state in listOfStates)
            {
                if (!(DurationByEffect.ContainsKey((int)state)))
                    return SpellInabilityReason.RequiredState;
            }
            listOfStates = (ArrayList)spellLevelsData.Fields["statesForbidden"];
            foreach (var state in listOfStates)
            {
                if (DurationByEffect.ContainsKey((int)state))
                    return SpellInabilityReason.ForbiddenState;
            }
            return SpellInabilityReason.None;
        }

        /// <summary>
        /// Perform the regeneration if necessary.
        /// </summary>
        private void PulseRegen()
        {
            Account.Config.RegenConfig.PulseRegen();
        }
        #endregion
    }
}
