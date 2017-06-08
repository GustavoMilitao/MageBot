using MageBot.DataFiles.Data.D2o;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Util.Util.StringsUtils;

namespace MageBot.Core.Engine.Constants
{
    public class IntelliSense
    {
        public static List<string> MonstersList = new List<string>();
        public static List<string> ItemsList = new List<string>();

        [DllImport("user32")]
        private extern static int GetCaretPos(out Point p);

        #region public methods

        public static void SaveGameData(string directory)
        {
            foreach (D2oFileEnum enumeration in Enum.GetValues(typeof(D2oFileEnum)))
            {
                //    for (var j = (int)D2oFileEnum.ItemTypes; j <= (int)D2oFileEnum.WorldMaps; j++)
                //{
                //    var enumeration = (D2oFileEnum)j;
                string path = System.IO.Path.Combine(directory, Enum.GetName(enumeration.GetType(), enumeration));
                System.IO.Directory.CreateDirectory(path);
                var objects = GameData.GetDataObjects(enumeration);
                if (objects != null)
                {
                    int i = 0;
                    foreach (var obj in new ConcurrentBag<DataClass>(objects))
                    {
                        string fileName = Enum.GetName(enumeration.GetType(), enumeration) + i.ToString();
                        if (obj.Fields.ContainsKey("nameId"))
                        {
                            foreach (var field in new ConcurrentDictionary<string, object>(obj.Fields))
                            {
                                if (field.Key == "nameId")
                                {
                                    fileName = DataFiles.Data.I18n.I18N.GetText((int)field.Value);
                                    fileName = StringUtils.removeSpecialChars(fileName);
                                }
                            }
                        }
                        dynamic oldJObj = JObject.FromObject(obj);
                        dynamic name = oldJObj.Name;
                        oldJObj = oldJObj.Fields;
                        dynamic JObj = new JObject();
                        foreach (var variable in oldJObj.Properties())
                        {
                            var aux = variable.Name.ToString().Replace("_", "");
                            aux = aux[0].ToString().ToUpper() + aux.Substring(1, aux.Length - 1);
                            JObj.Add(aux, variable.Value);
                        }
                        JObj.Name = name;
                        var newJson = JsonConvert.SerializeObject(JObj, Formatting.Indented);
                        System.IO.File.WriteAllText(System.IO.Path.Combine(path, fileName + ".json"), newJson);
                        i++;
                    }
                }
            }
        }

        public static void InitMonsters()
        {
            foreach (DataClass d in GameData.GetDataObjects(D2oFileEnum.Monsters))
            {
                if (d.Fields.ContainsKey("nameId"))
                    MonstersList.Add(MageBot.DataFiles.Data.I18n.I18N.GetText((int)d.Fields["nameId"]));
            }
        }

        public static void InitItems()
        {
            foreach (DataClass d in GameData.GetDataObjects(D2oFileEnum.Items))
            {
                ItemsList.Add(MageBot.DataFiles.Data.I18n.I18N.GetText((int)d.Fields["nameId"]));
            }
        }

        #endregion

        #region Private methods

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

            lstTemp = TempFilteredList.ToList();
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
                lstControl.SelectedIndex = lstControl.SelectedIndex < lstControl.Items.Count - 1 ? lstControl.SelectedIndex + 1 : lstControl.SelectedIndex;
                txtControl.Text = lstControl.SelectedItem.ToString();
                lstControl.Focus();
                txtControlKEA.Handled = true;
            }
            else if (txtControlKEA.KeyCode == Keys.Up)
            {
                lstControl.SelectedIndex = lstControl.SelectedIndex > 0 ? lstControl.SelectedIndex - 1 : lstControl.SelectedIndex;
                txtControl.Text = lstControl.SelectedItem.ToString();

                //lstControl.SelectedIndex = lstControl.Items.Count - 1;
                lstControl.Focus();
                txtControlKEA.Handled = true;
            }
            else if (txtControlKEA.KeyCode == Keys.Escape)
            {
                lstControl.Visible = false;
                txtControlKEA.Handled = true;
            }
            else if (txtControlKEA.KeyCode == Keys.Tab ||
                     txtControlKEA.KeyCode == Keys.Enter)
            {
                txtControl.Text = (lstControl).SelectedItem.ToString();
                txtControl.Select(txtControl.Text.Length, 0);
                txtControl.Focus();
                lstControl.Hide();
            }

            txtControl.LostFocus += (s, eventArgs) =>
            {
                if (!lstControl.Focused)
                    lstControl.Hide();
            };

            //listbox keyup event
            lstControl.KeyPress += (s, kueArgs) =>
            {
                if (kueArgs.KeyChar == (char)Keys.Tab ||
                txtControlKEA.KeyCode == Keys.Enter)
                {
                    txtControl.Text = ((ListBox)s).SelectedItem.ToString();
                    txtControl.Select(txtControl.Text.Length, 0);
                    txtControl.Focus();
                    lstControl.Hide();
                }
                else if (kueArgs.KeyChar == (char)Keys.Escape)
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
