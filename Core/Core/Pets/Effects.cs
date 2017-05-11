using BlueSheep.Common.Data.D2o;
using System.Collections;
using System.Collections.Generic;

namespace BlueSheep.Core.Pets
{
    static class Effects
    {

        #region Public methods
        public static List<int> GetEffects(int id)
        {
            DataClass data = GameData.GetDataObject(D2oFileEnum.Pets, id);
            ArrayList Afoods = (ArrayList)data.Fields["effects"];
            //TODO Militão: Get metadata name for Effect of pet.
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
