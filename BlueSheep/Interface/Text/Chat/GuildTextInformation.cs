using BlueSheep.Properties.I18n.Strings;
using System;
using System.Drawing;

namespace BlueSheep.Interface.Text.Chat
{
    class GuildTextInformation : TextInformation
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
                get { return Color.Purple; }
            }

            public override string Category
            {
                get { return Strings.Guild; }
                set { throw new NotImplementedException(); }
            }

            public override string Account
            {
                get { return Strings.Guild; }
                set { throw new NotImplementedException(); }
            }
            #endregion

        #region Constructeurs
            public GuildTextInformation(string text)
            {
                m_Text = text;
            }
            #endregion
    }
}
