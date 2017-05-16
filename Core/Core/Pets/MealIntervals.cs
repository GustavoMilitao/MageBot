using DataFiles.Data.D2o;

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
