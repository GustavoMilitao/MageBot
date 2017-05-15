using BotForge.Core.Game.Map.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Engine.Types
{
    public static class MonsterComplements
    {
        public static string monstersName(this GroupMonster group, bool withLevel)
        {
            if (withLevel)
                return String.Join(", ", group.Monsters.Select(m => m.Name + "[" + m.Grade.level + "]"));
            return String.Join(", ", group.Monsters.Select(m => m.Name));
        }
    }
}
