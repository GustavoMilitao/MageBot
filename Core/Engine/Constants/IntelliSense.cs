using BlueSheep.Common.Data.D2o;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
namespace BlueSheep.Engine.Constants
{
    public class IntelliSense
    {
        public static List<string> MonstersList = new List<string>();
        public static List<string> ItemsList = new List<string>();
        //public static List<Server> ServersList = new List<Server>();


        [DllImport("user32")]
        private extern static int GetCaretPos(out Point p);

        #region public methods



        public static void InitMonsters()
        {
            foreach (DataClass d in GameData.GetDataObjects(D2oFileEnum.Monsters))
            {
                if (d.Fields.ContainsKey("nameId"))
                    MonstersList.Add(BlueSheep.Common.Data.I18N.GetText((int)d.Fields["nameId"]));
            }
        }

        public static void InitItems()
        {
            foreach (DataClass d in GameData.GetDataObjects(D2oFileEnum.Items))
            {
                ItemsList.Add(BlueSheep.Common.Data.I18N.GetText((int)d.Fields["nameId"]));
            }
        }

        //public static void InitServers()
        //{
        //    foreach (DataClass d in GameData.GetDataObjects(D2oFileEnum.Servers))
        //    {
        //        ServersList.Add(createNewObjectServerFromGameData(d));
        //    }
        //}

        #endregion

        #region Private methods

        //private static Server createNewObjectServerFromGameData(DataClass data)
        //{
        //    Server server = new Server();
        //    server.Id = (int)data.Fields["id"];
        //    server.NameId = Convert.ToUInt32(data["nameId"]);
        //    server.CommentId = Convert.ToUInt32(data["commentId"]);
        //    server.OpeningDate = Convert.ToDouble(data["openingDate"]);
        //    server.Language = Convert.ToString(data["language"]);
        //    server.PopulationId = Convert.ToInt32(data["populationId"]);
        //    server.GameTypeId = Convert.ToUInt32(data["gameTypeId"]);
        //    server.CommunityId = Convert.ToInt32(data["communityId"]);
        //    server.RestrictedToLanguages = ArrayListToStringList((ArrayList)data["restrictedToLanguages"]);
        //    return server;
        //}

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
