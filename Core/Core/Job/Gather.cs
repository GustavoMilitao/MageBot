using MageBot.Core.Map.Elements;
using MageBot.DataFiles.Data.Pathfinding.Positions;
using MageBot.Util.Enums.Internal;
using Util.Util.Text.Log;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MageBot.Core.Job
{
    public class Gather
    {
        #region Fields
        private Account.Account account = null;
        private int _Id = -1;
        private List<int> m_BannedId = new List<int>();

        /// <summary>
        /// Return the name of Current_El.
        /// </summary>
        public string resourceName
        {
            get
            {
                return Current_El.Name;
            }
        }

        /// <summary>
        /// Store the gathering stats.
        /// </summary>
        public Dictionary<string, int> Stats = new Dictionary<string, int>();

        public InteractiveElement Current_El;
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }
        private int _SkillInstanceUid = -1;
        public int SkillInstanceUid
        {
            get
            {
                return _SkillInstanceUid;
            }
            set
            {
                _SkillInstanceUid = value;
            }
        }
        private int _Error;
        #endregion


        public Gather(Account.Account Account)
        {
            account = Account;
        }

        /// <summary>
        /// Perform the gathering of the specified ressources.
        /// </summary>
        /// <param name="ressources">
        /// List of the ressources'id.
        /// </param>
        /// /// <param name="job">
        /// The job used.
        /// </param>
        public bool GoGather(List<int> ressources)
        {
            try
            {
                #region Getting elements and join to some informations
                List<int> resIDList = ressources.Distinct().ToList();
                var usableElementsOnTheMap = account.MapData.UsableElements.Values.Join(account.MapData.InteractiveElements.Keys,
                    usableElem => usableElem.Element.Id,
                    interactiveElem => interactiveElem.Id,
                    (usableElem, interactiveElem) => new
                    {
                        Id = interactiveElem.Id,
                        Name = interactiveElem.Name,
                        CellId = usableElem.CellId,
                        IsUsable = interactiveElem.IsUsable,
                        TypeID = interactiveElem.TypeId,
                        Type = interactiveElem.Type,
                        EnabledSkills = interactiveElem.EnabledSkills,
                        DisabledSkills = interactiveElem.DisabledSkills,
                        Element = usableElem.Element
                    }).ToList();
                if (resIDList.Count > 0)
                    usableElementsOnTheMap = usableElementsOnTheMap.Where(elem => resIDList.Contains(elem.TypeID)).ToList();
                usableElementsOnTheMap = usableElementsOnTheMap.Where(elem => elem.IsUsable
                    && account.MapData.NoEntitiesOnCell(elem.CellId)).ToList();
                var usableElementsWithDistance = usableElementsOnTheMap.Select(elem => new
                {
                    Id = elem.Id,
                    Name = elem.Name,
                    CellId = elem.CellId,
                    IsUsable = elem.IsUsable,
                    TypeID = elem.TypeID,
                    Type = elem.Type,
                    EnabledSkills = elem.EnabledSkills,
                    DisabledSkills = elem.DisabledSkills,
                    Element = elem.Element,
                    ResourceDistance = GetRessourceDistance((int)elem.Id)
                }).OrderBy(elem => elem.ResourceDistance);
                #endregion

                if (usableElementsWithDistance.Count() > 0)
                {
                    foreach (var element in usableElementsWithDistance)
                    {
                        if (!element.IsUsable || m_BannedId.Contains((int)element.Id))
                            continue;
                        Id = (int)element.Id;
                        SkillInstanceUid = element.EnabledSkills.FirstOrDefault().SkillInstanceUid;
                        Current_El = element.Element;
                        int distance = element.ResourceDistance;
                        account.Log(new DebugTextInformation("[Gather] Distance from element " + element.Id + " = " + distance), 0);
                        if (distance == -1)
                        {
                            continue;
                        }
                        if (account.MapData.CanGatherElement(Id))
                        {
                            account.SetStatus(Status.Gathering);
                            account.Map.UseElement(Id, SkillInstanceUid);
                            return true;
                        }
                        else
                            continue;
                    }
                }
            }
            catch (Exception Ex)
            {
                account.Log(new ErrorTextInformation(Ex.Message), 0);
            }
            Id = -1;
            SkillInstanceUid = -1;
            Current_El = null;
            return false;
        }

        /// <summary>
        /// Increase the error count and continue the path if it overtakes a step.
        /// </summary>
        public bool Error()
        {
            _Error++;
            if (_Error > 1 && account.Path != null)
            {
                account.Path.PerformActionsStack();
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Forbid an element id that is unreachable actually.
        /// </summary>
        public void BanElementId(int id)
        {
            m_BannedId.Add(id);
            account.Log(new DebugTextInformation("[Gather] Element id " + id + " banned."), 0);
        }

        /// <summary>
        /// Set the error amount to 0.
        /// </summary>
        public void ClearError()
        {
            _Error = 0;
            m_BannedId.Clear();
        }

        /// <summary>
        /// Sort the distance from a list.
        /// </summary>
        public List<UsableElement> TrierDistanceElement(List<int> ListDistance, List<UsableElement> ListUsableElement)
        {
            int ListLength = 0;
            bool InOrder = false;
            object TimeToAccess = null;

            ListLength = ListDistance.Count;
            while (!InOrder)
            {
                InOrder = true;
                for (var i = 0; i <= ListLength - 2; i++)
                {
                    if (ListDistance[i] > ListDistance[i + 1])
                    {
                        TimeToAccess = ListDistance[i];
                        ListDistance[i] = ListDistance[i + 1];
                        ListDistance[i + 1] = Convert.ToInt32(TimeToAccess);
                        TimeToAccess = ListUsableElement[i];
                        ListUsableElement[i] = ListUsableElement[i + 1];
                        ListUsableElement[i + 1] = (UsableElement)TimeToAccess;
                        InOrder = false;
                    }
                }
                ListLength = ListLength - 1;
            }

            return ListUsableElement;
        }

        /// <summary>
        /// Get the distance between the character and the ressource.
        /// </summary>
        /// <param name="Id">
        /// The id of the ressource.
        /// </param>
        public int GetRessourceDistance(int Id)
        {
            MapPoint CharacterMapPoint = new MapPoint(account.MapData.Character.Disposition.CellId);
            StatedElement StatedRessource = account.MapData.StatedElements.FirstOrDefault((se) => se.Id == Id);
            if (StatedRessource != null)
            {
                MapPoint RessourceMapPoint = new MapPoint((int)StatedRessource.CellId);
                return CharacterMapPoint.DistanceTo(RessourceMapPoint);
            }
            return -1;
        }
    }
}
