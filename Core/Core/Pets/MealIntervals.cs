using BlueSheep.Common.Data.D2o;
using System.Collections;
using System.Collections.Generic;

namespace BlueSheep.Core.Pets
{
    class MealIntervals
    {
        #region Public methods
        public static int GetMealIntervals(int id)
        {
            DataClass data = GameData.GetDataObject(D2oFileEnum.Pets, id);
            return (int)data.Fields["minDurationBeforeMeal"];
        }
        #endregion
    }
}
