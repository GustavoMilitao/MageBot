using System.ComponentModel;

namespace MageBot.Util.Enums.Internal
{
    public enum Operator
    {
        [Description("")]
        None,
        [Description(">")]
        More,
        [Description(">=")]
        MoreEqual,
        [Description("<")]
        Less,
        [Description("<=")]
        LessEqual,
        [Description("=")]
        Equal,
        [Description("<>")]
        Different
    }
}
