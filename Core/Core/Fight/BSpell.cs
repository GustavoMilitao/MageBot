using DataFiles.Data.D2o;

namespace BlueSheep.Core.Fight
{
    public class BSpell
    {
        public int SpellId { get; set;}
        public int Level { get; set; }
        public string Name { get { return GetName(); } }

        public BSpell()
        {
        }

        public BSpell(int spellId, int level)
        {
            SpellId = spellId;
            Level = level;
        }

        public BSpell(int spellId)
        {
            SpellId = spellId;
        }

        private string GetName()
        {
            DataClass spell = GameData.GetDataObject(D2oFileEnum.Spells, SpellId);
            return DataFiles.Data.I18n.I18N.GetText((int)spell.Fields["nameId"]);
        }
    }

}
