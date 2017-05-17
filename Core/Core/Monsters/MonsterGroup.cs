using MageBot.DataFiles.Data.D2o;
using MageBot.Protocol.Types.Game.Context.Roleplay;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MageBot.Core.Monsters
{
    public class MonsterGroup
    {
        public MonsterGroup(GroupMonsterStaticInformations staticInfos, int cellId, double contextualId)
        {
            m_staticInfos = staticInfos;
            m_cellId = cellId;
            m_contextualId = contextualId;
        }

        public GroupMonsterStaticInformations m_staticInfos;
        public int m_cellId;
        public double m_contextualId;

        public int monstersCount
        {
            get { return m_staticInfos.Underlings.Count() + 1; }
        }

        public int monstersLevel
        {
            get
            {
                int monstersLevel = 0;
                foreach (MonsterInGroupInformations monster in m_staticInfos.Underlings)
                {
                    DataClass monsterData = GameData.GetDataObject(D2oFileEnum.Monsters, monster.CreatureGenericId);
                    object monsterGrades = monsterData.Fields["grades"];
                    ArrayList monsterGrades2 = (ArrayList)monsterGrades;
                    DataClass monsterGradeData = (DataClass)monsterGrades2[Convert.ToInt32(monster.Grade) - 1];
                    monstersLevel += Convert.ToInt32(monsterGradeData.Fields["level"]);
                }
                DataClass mainmonsterData = GameData.GetDataObject(D2oFileEnum.Monsters, m_staticInfos.MainCreatureLightInfos.CreatureGenericId);
                object mainmonsterGrades = mainmonsterData.Fields["grades"];
                ArrayList mainmonsterGrades2 = (ArrayList)mainmonsterGrades;
                DataClass mainmonsterGradeData = (DataClass)mainmonsterGrades2[Convert.ToInt32(m_staticInfos.MainCreatureLightInfos.Grade) - 1];
                monstersLevel += Convert.ToInt32(mainmonsterGradeData.Fields["level"]);
                return monstersLevel;
            }
        }

        public string monstersName(bool withLevels)
        {
            List<string> monstersname = new List<string>();

            foreach (MonsterInGroupInformations monster in m_staticInfos.Underlings)
            {
                DataClass monsterData = GameData.GetDataObject(D2oFileEnum.Monsters, monster.CreatureGenericId);
                object monsterGrades = monsterData.Fields["grades"];
                ArrayList monsterGrades2 = (ArrayList)monsterGrades;
                DataClass monsterGradeData = (DataClass)monsterGrades2[Convert.ToInt32(monster.Grade) - 1];
                if (withLevels)
                    monstersname.Add(MageBot.DataFiles.Data.I18n.I18N.GetText((int)monsterData.Fields["nameId"]) + "(" + monsterGradeData.Fields["level"] + ")");
                else
                    monstersname.Add(MageBot.DataFiles.Data.I18n.I18N.GetText((int)monsterData.Fields["nameId"]));
            }
            DataClass mainmonsterData = GameData.GetDataObject(D2oFileEnum.Monsters, m_staticInfos.MainCreatureLightInfos.CreatureGenericId);
            object mainmonsterGrades = mainmonsterData.Fields["grades"];
            ArrayList mainmonsterGrades2 = (ArrayList)mainmonsterGrades;
            DataClass mainmonsterGradeData = (DataClass)mainmonsterGrades2[Convert.ToInt32(m_staticInfos.MainCreatureLightInfos.Grade) - 1];
            if (withLevels)
                monstersname.Add(MageBot.DataFiles.Data.I18n.I18N.GetText((int)mainmonsterData.Fields["nameId"]) + "(" + mainmonsterGradeData.Fields["level"] + ")");
            else
                monstersname.Add(MageBot.DataFiles.Data.I18n.I18N.GetText((int)mainmonsterData.Fields["nameId"]));

            string names = "";
            foreach (string item in monstersname)
            {
                names = names + item;
                if (monstersname.IndexOf(item) != monstersname.Count - 1)
                    names = names + ",";
            }
            return names;
        }

        public List<string> NameList()
        {
            List<string> monstersname = new List<string>();

            foreach (MonsterInGroupInformations monster in m_staticInfos.Underlings)
            {
                DataClass monsterData = GameData.GetDataObject(D2oFileEnum.Monsters, monster.CreatureGenericId);
                monstersname.Add(MageBot.DataFiles.Data.I18n.I18N.GetText((int)monsterData.Fields["nameId"]));
            }
            DataClass mainmonsterData = GameData.GetDataObject(D2oFileEnum.Monsters, m_staticInfos.MainCreatureLightInfos.CreatureGenericId);
            monstersname.Add(MageBot.DataFiles.Data.I18n.I18N.GetText((int)mainmonsterData.Fields["nameId"]));
            return monstersname;
        }
                    
    }
}
