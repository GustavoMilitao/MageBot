using System;
using System.Collections.Generic;
using MageBot.Core.Storage;
using Util.Util.Text.Log;
using MageBot.DataFiles.Data.D2o;
using MageBot.Core.Pets;

namespace MageBot.Core
{
    public class Running
    {
        #region Fields
        private int m_CurrentPetIndex;
        private Opening m_Openning;
        private Leaving m_Leaving;
        private Getting m_Getting;
        private Feeding m_Feeding;
        private LeavingDialog m_LeavingDialog;
        private bool m_OnSafe;
        private Account.Account Account;
        #endregion

        #region Properties
        public Leaving Leaving
        {
            get { return m_Leaving; }
            set { m_Leaving = value; }
        }

        public int CurrentPetIndex
        {
            get { return m_CurrentPetIndex; }
            set { m_CurrentPetIndex = value; }
        }

        public Feeding Feeding
        {
            get { return m_Feeding; }
            set { m_Feeding = value; }
        }

        public LeavingDialog LeavingDialog
        {
            get { return m_LeavingDialog; }
            set { m_LeavingDialog = value; }
        }

        public bool OnSafe
        {
            get { return m_OnSafe; }
            set { m_OnSafe = value; }
        }

        public bool OnGetting { get; set; }

        public bool OnLeaving { get; set; }
        #endregion

        #region Constructeurs
        public Running()
        {
            m_Openning = null;
            m_Leaving = null;
            m_Getting = null;
            OnSafe = false;
            m_CurrentPetIndex = 0;
        }

        public Running(Account.Account account)
        {
            Account = account;
            m_Openning = null;
            m_Leaving = null;
            m_Getting = null;
            m_CurrentPetIndex = 0;
        }
        #endregion

        #region Public methods
        public void Init()
        {
            if (m_CurrentPetIndex == Account.PetsList.Count)
            {
                Account.SetNextMeal();
                Account.GetNextMeal();
                return;
            }

            if ((CheckTime(Account.PetsList[m_CurrentPetIndex])) ||
                ((m_Feeding != null) && (m_Feeding.SecondFeeding)))
            {
                if (
                    Account.PetsList[m_CurrentPetIndex].Informations.Position == 8)
                {
                    Console.WriteLine();
                }

                if (Account.PetsList[m_CurrentPetIndex].FoodList.Count == 0)
                {
                    if (Account.Safe == null)
                    {
                        NoFood();
                        return;
                    }

                    if (!m_OnSafe)
                    {
                        m_OnSafe = true;
                        m_Openning = new Opening();
                        m_Openning.Init(Account);
                        return;
                    }

                    LeavingFoodToSafe();
                    return;
                }

                m_Feeding = new Feeding(Account);
                m_Feeding.Init(Account.PetsList[m_CurrentPetIndex]);
                Account.Wait(1000);
                m_CurrentPetIndex++;
                return;
            }

            m_CurrentPetIndex++;
            Init();
        }

        public void LeavingFoodToSafe()
        {
            if (m_Leaving == null)
                m_Leaving = new Leaving(Account);

            m_Leaving.Init();
        }

        public void GettingFoodFromSafe()
        {
            if (m_Getting == null)
                m_Getting = new Getting(Account);

            m_Getting.Init();
        }

        public void CheckStatisticsUp()
        {
            foreach (Pet petModified in Account.PetsModifiedList)
            {
                if (m_CurrentPetIndex >= Account.PetsList.Count)
                    continue;
                if (petModified.Informations.UID ==
                    Account.PetsList[m_CurrentPetIndex].Informations.UID)
                {
                    Pet pet = Account.PetsList[m_CurrentPetIndex];

                    if (pet.Effect != petModified.Effect)
                    {
                       Account.Log(new ActionTextInformation("Up de caractéristique, " + petModified.Datas.Name + " " + petModified.Informations.UID + "."),4);

                        m_Feeding.SecondFeeding = true;
                    }
                    else
                        m_Feeding.SecondFeeding = false;

                    break;
                }
            }

            Account.PetsList = new List<Pet>();


            foreach (MageBot.Core.Inventory.Item item in Account.Inventory.Items)
            {
                DataClass itemData = GameData.GetDataObject(D2oFileEnum.Items, item.GID);
                if ((int)itemData.Fields["typeId"] == 18)
                {
                    Pet petToAdd = new Pet(item, itemData, Account);
                    Account.PetsList.Add(petToAdd);
                }
            }

            Account.PetsModifiedList = null;

            Init();
        }

        public void LeavingDialogWithSafe()
        {
            if (m_LeavingDialog == null)
                m_LeavingDialog = new LeavingDialog();

            m_LeavingDialog.Init(Account);
        }

        public void NoFood()
        {
           Account.Log(new ActionTextInformation("Aucune nourriture disponible pour " +
                                                                        MageBot.DataFiles.Data.I18n.I18N.GetText((int)Account.PetsList[m_CurrentPetIndex].Datas
                                                                          .Fields["nameId"]) +
                                                                      "."),0);

           Account.PetsList[m_CurrentPetIndex].NonFeededForMissingFood = true;

           Account.PetsList[m_CurrentPetIndex].NextMeal = new DateTime();
            m_CurrentPetIndex++;
            Init();
        }
        #endregion

        #region Private methods
        private static bool CheckTime(Pet pet)
        {
            DateTime nextMeal = new DateTime(pet.NextMeal.Year, pet.NextMeal.Month, pet.NextMeal.Day, pet.NextMeal.Hour,
                pet.NextMeal.Minute, 0);

            return nextMeal <= DateTime.Now;
        }
        #endregion
    }
}
