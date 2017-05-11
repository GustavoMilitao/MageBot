using BlueSheep.Util.I18n.Strings;
using System;
using System.Drawing;

namespace BlueSheep.Util.Text.Log
{
    public class ActionTextInformation : TextInformation
    {
        #region Fields
        private string m_Text;
        #endregion

        #region Properties
        public override string Text
        {
            get { return m_Text; }
            set { m_Text = value; }
        }

        public override Color Color
        {
            get { return Color.Black; }
        }

        public override string Category
        {
            get { return Strings.Action; }
            set { throw new NotImplementedException();}
        }

        public override string Account { get; set; }
        #endregion

        #region Constructeurs
        public ActionTextInformation(string text)
        {
            m_Text = text;
        }
        #endregion
    }
}
