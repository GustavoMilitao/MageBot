using Util.Util.I18n.Strings;
using System;
using System.Drawing;

namespace Util.Util.Text.Log
{
    public class DofInfoCanal : TextInformation
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
            get { return ColorTranslator.FromHtml("#008E1C"); }
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
        public DofInfoCanal(string text)
        {
            m_Text = text;
        }
        #endregion
    }
}
