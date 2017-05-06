using BlueSheep.Common.Data.D2o;

namespace BlueSheep.Core.Fight
{
    public class BSpell
    {
        public int SpellId
        {
            get { return m_SpellId; }
            set { m_SpellId = value; }
        }
        private int m_SpellId;
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
        private string m_Name;
        
        public BSpell()
        {
        }

        public BSpell(int spellId, string name)
        {
            SpellId = spellId;
            Name = name;
        }

        public DataClass data
        {
            get
            {
                return GameData.GetDataObject(D2oFileEnum.Spells, m_SpellId);
            }
        }
    }

}
