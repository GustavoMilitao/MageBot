using MageBot.Util.Enums.Internal;

namespace MageBot.Core.Fight
{
    public class MonsterRestrictions
    {
        public string MonsterName { get; set; }
        public Operator Operator { get; set; }
        public int Quantity { get; set; }
        public RestrictionLevel RestrictionLevel { get; set; }
    }
}
