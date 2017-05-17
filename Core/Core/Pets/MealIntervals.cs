using MageBot.DataFiles.Data.D2o;

namespace MageBot.Core.Pets
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
