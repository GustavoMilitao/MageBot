using MageBot.DataFiles.Data.D2o;
using MageBot.Protocol.Types.Game.Interactive;
using System.Collections.Generic;
using System.Linq;

namespace MageBot.Core.Map.Elements
{
    public class InteractiveElement
    {
        public InteractiveElement(uint id, int typeId, List<InteractiveElementSkill> enabledSkills, List<InteractiveElementSkill> disabledSkills)
        {
            Id = id;
            TypeId = typeId;
            EnabledSkills = enabledSkills;
            DisabledSkills = disabledSkills;
        }

        public InteractiveElement()
        {

        }

        public InteractiveElement(Protocol.Types.Game.Interactive.InteractiveElement element)
        {
            Id = (uint)element.ElementId;
            TypeId = element.TypeID;
            EnabledSkills = element.EnabledSkills;
            DisabledSkills = element.DisabledSkills;
        }

        public List<InteractiveElementSkill> DisabledSkills { get; private set; }
        public List<InteractiveElementSkill> EnabledSkills { get; private set; }
        public uint Id { get; private set; }
        public int CellId { get; set; } = -1;

        public bool IsUsable
        {
            get { return EnabledSkills.Count > 0; }
        }

        public int TypeId { get; private set; }

        public string Type
        {
            get
            {
                List<DataClass> ld = GameData.GetDataObjects(D2oFileEnum.Skills).ToList();

                foreach (DataClass d in ld)
                {
                    if ((int)d.Fields["interactiveId"] == TypeId)
                    {
                        var itemClass = GameData.GetDataObject(D2oFileEnum.Items, (int)d.Fields["gatheredRessourceItem"]);
                        if (itemClass != null)
                        {
                            var itemTypeClass = GameData.GetDataObject(D2oFileEnum.ItemTypes, (int)itemClass.Fields["typeId"]);
                            if (itemTypeClass != null)
                            {
                                return DataFiles.Data.I18n.I18N.GetText((int)itemTypeClass.Fields["nameId"]);
                            }
                            return "Unknown";
                        }
                        return "Unknown";
                    }
                }
                return "Unknown";
            }
        }

        public string Name
        {
            get
            {
                List<DataClass> ld = GameData.GetDataObjects(D2oFileEnum.Skills).ToList();

                foreach (DataClass d in ld)
                {
                    if ((int)d.Fields["interactiveId"] == TypeId)
                    {
                        var dataClass = GameData.GetDataObject(D2oFileEnum.Items, (int)d.Fields["gatheredRessourceItem"]);
                        if (dataClass != null)
                        {
                            return DataFiles.Data.I18n.I18N.GetText((int)dataClass.Fields["nameId"]);
                        }
                        return "Unknown";
                    }
                }
                return "Unknown";
            }
        }
    }
}
