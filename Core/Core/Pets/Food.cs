using DataFiles.Data.D2o;
using System.Collections;
using System.Collections.Generic;

namespace BlueSheep.Core.Pets
{
    public class Food
    {
        #region Properties
        public Core.Inventory.Item Informations { get; set; }

        public DataClass Datas { get; set; }
        #endregion

        #region Constructeurs
        public Food(Core.Inventory.Item informations, DataClass datas)
        {
            Informations = informations;
            Datas = datas;
        }
        #endregion

        #region Public methods
        public static List<int> GetFoods(int id)
        {
            DataClass data = GameData.GetDataObject(D2oFileEnum.Pets, id);
            ArrayList Afoods = (ArrayList)data.Fields["foodItems"];
            List<int> foods = new List<int>();
            for (int i = 0; i < Afoods.Count; i++)
            {
                if ((int)Afoods[i] != 2239)
                    foods.Add((int)Afoods[i]);
            }
            return foods;
        }
        #endregion
    }
}
