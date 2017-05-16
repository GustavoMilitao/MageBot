using DataFiles.Data.D2o;
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
            ArrayList effects = (ArrayList)data.Fields["possibleEffects"];
            List<int> effectIds = new List<int>();
            foreach(DataClass d in effects)
            {
                effectIds.Add((int)d["effectId"]);
            }
            return effectIds;
        }
        #endregion
    }
}
