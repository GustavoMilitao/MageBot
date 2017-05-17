using MageBot.Protocol.Types.Game.Interactive;
using System.Collections.Generic;

namespace MageBot.Core.Map.Elements
{
    public class UsableElement
    {
        public UsableElement(int cellId, InteractiveElement element, List<InteractiveElementSkill> skills)
        {
            CellId = cellId;
            Element = element;
            Skills = skills;
        }

        public int CellId { get; private set; }
        public InteractiveElement Element { get; private set; }
        public List<InteractiveElementSkill> Skills { get; private set; }
    }
}
