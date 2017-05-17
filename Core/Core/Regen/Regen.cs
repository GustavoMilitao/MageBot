using BlueSheep.Protocol.Messages.Game.Context.Roleplay.Emote;
using BlueSheep.Util.Enums.Internal;
using BlueSheep.Util.Text.Log;
using System;

namespace BlueSheep.Core.Regen
{
    public class Regen
    {
        public Account.Account Account { get; set; }
        public int RegenChoice { get; set; }

        public Regen(Account.Account account)
        {
            Account = account;
        }

        public async void PulseRegen()
        {
            if (((Account.CharacterStats.LifePoints / Account.CharacterStats.MaxLifePoints) * 100) < RegenChoice)
            {
                Account.SetStatus(Status.Regenerating);
                //List<Item> items = GetRegenItems();
                //TODO Militão: Add to get Regen Items later.
                //if (items.Count > 0)
                //{
                //    if (UseItems(items))
                //    {
                //        await Account.PutTaskDelay(1000);
                //        PulseRegen();
                //        return;
                //    }
                //}
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
    }
}
