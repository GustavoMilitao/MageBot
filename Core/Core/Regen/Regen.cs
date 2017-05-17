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
        public Account.Account Account { get; set; }
        public int RegenChoice { get; set; }
        public List<Item> RegenItems { get; set; }

        public Regen(Account.Account account)
        {
            Account = account;
            RegenItems = new List<Item>();
        }

        public async void PulseRegen()
        {
            if (((Account.CharacterStats.LifePoints / Account.CharacterStats.MaxLifePoints) * 100) < RegenChoice)
            {
                Account.SetStatus(Status.Regenerating);
                //TODO Militão: Add to get Regen Items later.
                if (RegenItems.Count > 0)
                {
                    if (UseItems(RegenItems))
                    {
                        await Account.PutTaskDelay(1000);
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
                await Account.PutTaskDelay((time + 1) * 1000);
                if (Account.Config.Path != null && Account.Config.Path.Launched)
                    Account.Config.Path.ParsePath();
            }
        }

        public void GetRegenItemsByNames(List<string> names)
        {
            names.ForEach(n => RegenItems.Add(Account.Inventory.GetItemFromName(n)));
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
