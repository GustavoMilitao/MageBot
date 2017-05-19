using MageBot.Core.Inventory;
using MageBot.Protocol.Messages.Game.Context.Roleplay.Emote;
using MageBot.Util.Enums.Internal;
using Util.Util.Text.Log;
using System;
using System.Collections.Generic;

namespace MageBot.Core.Regen
{
    public class Regen
    {
        private Account.Account Account { get; set; }

        public Regen(Account.Account account)
        {
            Account = account;
            Account.Config.RegenItems = new List<Item>();
        }

        public void PulseRegen()
        {
            if (((Account.CharacterStats.LifePoints / Account.CharacterStats.MaxLifePoints) * 100) < Account.Config.RegenChoice)
            {
                Account.SetStatus(Status.Regenerating);
                if (Account.Config.RegenItems.Count > 0)
                {
                    if (UseItems(Account.Config.RegenItems))
                    {
                        Account.Wait(1000);
                        PulseRegen();
                        return;
                    }
                }
                int maxLife = Convert.ToInt32(Account.CharacterStats.MaxLifePoints);
                int life = Convert.ToInt32(Account.CharacterStats.LifePoints);
                int time = Convert.ToInt32(Math.Round(Convert.ToDecimal(maxLife - life) / 2));
                EmotePlayRequestMessage msg = new EmotePlayRequestMessage(1);
                Account.SocketManager.Send(msg);
                Account.Log(new GeneralTextInformation(String.Format("Regenerating for {0} seconds.", time)), 2);
                Account.Wait((time + 1) * 1000);
                if (Account.Path != null && Account.Path.Launched)
                    Account.Path.ParsePath();
            }
        }

        public void GetRegenItemsByNames(List<string> names)
        {
            names.ForEach(n => Account.Config.RegenItems.Add(Account.Inventory.GetItemFromName(n)));
        }

        private bool UseItems(List<Item> items)
        {
            foreach (Item i in items)
            {
                if (i.Quantity <= 0)
                    continue;
                else
                {
                    Account.Inventory.UseItem(i.UID);
                    return true;
                }
            }
            return false;
        }
    }
}
