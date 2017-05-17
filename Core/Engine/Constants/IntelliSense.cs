using BlueSheep.DataCenter;
using DataFiles.Data.D2o;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Core.Engine.Constants
{
    public class IntelliSense
    {
        public static List<string> MonstersList = new List<string>();
        public static List<string> ItemsList = new List<string>();
        public static List<Server> ServersList = new List<Server>();


        [DllImport("user32")]
        private extern static int GetCaretPos(out Point p);

        #region public methods



        public static void InitMonsters()
        {
            foreach (DataClass d in GameData.GetDataObjects(D2oFileEnum.Monsters))
            {
                if (d.Fields.ContainsKey("nameId"))
                    MonstersList.Add(DataFiles.Data.I18n.I18N.GetText((int)d.Fields["nameId"]));
            }
        }

        public static void InitItems()
        {
            foreach (DataClass d in GameData.GetDataObjects(D2oFileEnum.Items))
            {
                ItemsList.Add(DataFiles.Data.I18n.I18N.GetText((int)d.Fields["nameId"]));
            }
        }

        public static void InitServers()
        {
            foreach (DataClass d in GameData.GetDataObjects(D2oFileEnum.Servers))
            {
                ServersList.Add(CreateNewObjectServerFromGameData(d));
            }
        }

        #endregion

        #region Private methods

        private static Server CreateNewObjectServerFromGameData(DataClass data)
        {
            Server server = new Server()
            {
                Id = (int)data.Fields["id"],
                NameId = Convert.ToUInt32(data["nameId"]),
                CommentId = Convert.ToUInt32(data["commentId"]),
                OpeningDate = Convert.ToDouble(data["openingDate"]),
                Language = Convert.ToString(data["language"]),
                PopulationId = Convert.ToInt32(data["populationId"]),
                GameTypeId = Convert.ToUInt32(data["gameTypeId"]),
                CommunityId = Convert.ToInt32(data["communityId"]),
                RestrictedToLanguages = ArrayListToStringList((ArrayList)data["restrictedToLanguages"])
            };
            return server;
        }

        private static List<string> ArrayListToStringList(ArrayList arrayList)
        {
            List<string> result = new List<string>();
            foreach (object o in arrayList)
            {
                result.Add((string)o);
            }
            return result;
        }

        #endregion

        #region UI Methods
        public static void AutoCompleteTextBox(TextBox txtControl, ListBox lstControl,
            List<string> lstAutoCompleteList, KeyEventArgs txtControlKEA)
        {
            Point cp;
            GetCaretPos(out cp);
            List<string> lstTemp = new List<string>();
            //Positioning the Listbox on TextBox by Type Insertion Cursor position
            lstControl.SetBounds(cp.X + txtControl.Location.X, cp.Y + txtControl.Location.Y + 20, 150, 50);

            var TempFilteredList = lstAutoCompleteList.Where
                (n => n.StartsWith(GetLastString(txtControl.Text))).Select(r => r);

            lstTemp = TempFilteredList.ToList<string>();
            if (lstTemp.Count != 0 && GetLastString(txtControl.Text) != "")
            {
                lstControl.DataSource = lstTemp;
                lstControl.Show();
                lstControl.BringToFront();
            }
            else
            {
                lstControl.Hide();
            }

            //Code for focusing ListBox Items While Pressing Down and UP Key. 
            if (txtControlKEA.KeyCode == Keys.Down)
            {
                lstControl.SelectedIndex = 0;
                lstControl.Focus();
                txtControlKEA.Handled = true;
            }
            else if (txtControlKEA.KeyCode == Keys.Up)
            {
                lstControl.SelectedIndex = lstControl.Items.Count - 1;
                lstControl.Focus();
                txtControlKEA.Handled = true;
            }

            //text box key press event
            txtControl.KeyPress += (s, kpeArgs) =>
            {

                if (kpeArgs.KeyChar == (char)Keys.Enter)
                {
                    if (lstControl.Visible)
                    {
                        lstControl.Focus();
                    }
                    kpeArgs.Handled = true;
                }
                else if (kpeArgs.KeyChar == (char)Keys.Escape)
                {
                    lstControl.Visible = false;
                    kpeArgs.Handled = true;
                }
                else if (kpeArgs.KeyChar == (char)Keys.Tab)
                {
                    txtControl.Text = ((ListBox)s).SelectedItem.ToString();
                    txtControl.Select(txtControl.Text.Length, 0);
                    txtControl.Focus();
                    lstControl.Hide();
                }
            };

            txtControl.LostFocus += (s, eventArgs) =>
            {
                if (!lstControl.Focused)
                    lstControl.Hide();
            };

            //listbox keyup event
            lstControl.KeyUp += (s, kueArgs) =>
            {
                if (kueArgs.KeyCode == Keys.Tab)
                {
                    txtControl.Text = ((ListBox)s).SelectedItem.ToString();
                    txtControl.Select(txtControl.Text.Length, 0);
                    txtControl.Focus();
                    lstControl.Hide();
                }
                else if (kueArgs.KeyCode == Keys.Escape)
                {
                    lstControl.Hide();
                    txtControl.Focus();
                }
            };

        }

        private static string GetLastString(string s)
        {
            if (s.Length != 0)
                s = s[0].ToString().ToUpper() + s.Substring(1).ToLower();
            return s;
        }
        #endregion
    }
}
