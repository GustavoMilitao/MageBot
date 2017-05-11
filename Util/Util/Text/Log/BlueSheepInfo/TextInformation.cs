using System.Drawing;

namespace BlueSheep.Util.Text.Log
{
    public abstract class TextInformation
    {
        #region Properties
        public abstract string Text { get; set; }

        public abstract Color Color { get; }

        public abstract string Category { get; set; }

        public abstract string Account { get; set; }
        #endregion
    }
}
