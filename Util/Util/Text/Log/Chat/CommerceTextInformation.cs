using BlueSheep.Util.I18n.Strings;
using System;
using System.Drawing;

namespace BlueSheep.Util.Text.Log
{
    public class CommerceTextInformation : TextInformation
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
                get 
                {
                    Color col = (Color)ColorTranslator.FromHtml("#CA8D23");
                    return col;
                }
            }

            public override string Category
            {
                get { return Strings.Commerce; }
                set { throw new NotImplementedException(); }
            }

            public override string Account
            {
                get { return Strings.Commerce; }
                set { throw new NotImplementedException(); }
            }
            #endregion

        #region Constructeurs
            public CommerceTextInformation(string text)
            {
                m_Text = text;
            }
            #endregion
    }
}
