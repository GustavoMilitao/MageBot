using BlueSheep.DataCenter;
using DataFiles.Data.D2o;
using DataFiles.Data.I18n;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;

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
    }
}
