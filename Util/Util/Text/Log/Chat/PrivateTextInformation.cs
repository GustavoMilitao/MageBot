using Util.Util.I18n.Strings;
using System;
using System.Drawing;

namespace Util.Util.Text.Log
{
    public class PrivateTextInformation : TextInformation
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
                get { return ColorTranslator.FromHtml("#0096AE"); }
            }

            public override string Category
            {
                get { return Strings.Private; }
                set { throw new NotImplementedException(); }
            }

            public override string Account
            {
                get { return Strings.Private; }
                set { throw new NotImplementedException(); }
            }
            #endregion

        #region Constructeurs
            public PrivateTextInformation(string text)
            {
                m_Text = text;
            }
            #endregion
    }
}
