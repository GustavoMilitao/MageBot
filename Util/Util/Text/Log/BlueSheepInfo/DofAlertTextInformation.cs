using BlueSheep.Util.I18n.Strings;
using System;
using System.Drawing;

namespace BlueSheep.Util.Text.Log
{
    public class DofAlertTextInformation : TextInformation
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
            get { return Color.Red; }
        }

        public override string Category
        {
            get { return Strings.Dofus; }
            set { throw new NotImplementedException(); }
        }

        public override string Account
        {
            get { return Strings.General; }
            set { throw new NotImplementedException(); }
        }
        #endregion

        #region Constructeurs
        public DofAlertTextInformation(string text)
        {
            m_Text = text;
        }
        #endregion
    }
}
