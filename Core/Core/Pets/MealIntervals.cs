using BlueSheep.Common.Data.D2o;
using System.Collections;
using System.Collections.Generic;

namespace BlueSheep.Core.Pets
{
    class MealIntervals
    {
        #region Public methods
        public static List<int> GetMealIntervals(int id)
        {
            DataClass data = GameData.GetDataObject(D2oFileEnum.Pets, id);
            ArrayList mealIntervalsAL = (ArrayList)data.Fields["mealIntervals"];
            //TODO Militão: Get metadata name for meal intervals of pet.
            List<int> mealIntervals = new List<int>();
            for (int i = 0; i < mealIntervalsAL.Count; i++)
            {
                if ((int)mealIntervalsAL[i] != 2239)
                    mealIntervals.Add((int)mealIntervalsAL[i]);
            }
            return mealIntervals;
        }
        #endregion
    }
}
