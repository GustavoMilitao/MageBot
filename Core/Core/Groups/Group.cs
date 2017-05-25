using MageBot.Protocol.Messages.Game.Context.Roleplay.Party;
using System.Collections.Generic;
using System.Linq;
using System;
using MageBot.Core.Path;
using Util.Util.Text.Log;

namespace MageBot.Core.Groups
{
    public class Group
    {
        #region Fields
        public List<Account.Account> Accounts { get; set; }
        public Account.Account LastAccountLaunchedFight { get; set; }
        public uint PartyId { get; set; }
        public PathManager Path { get; set; }
        public string name;
        #endregion

        #region Constructeurs
        public Group(List<Account.Account> list, string namee)
        {
            Accounts = list;
            name = namee;
        }
        #endregion

        #region Publics methods
        public Account.Account GetMaster()
        {
            return Accounts.Find(acc => acc.IsMaster);
        }

        public void MoveGroup(string move)
        {
            Accounts.ForEach(acc =>
            {
                acc.Map.ChangeMap(move);
            });
        }

        public void UseItemGroup(int uid)
        {
            Accounts.ForEach(acc =>
            {
                acc.Inventory.UseItem(uid);
                //acc.Wait(500);
            });
        }

        public void UseZaapGroup()
        {
            var master = GetMaster();
            var slaves = GetSlaves();
            slaves.ForEach(acc =>
            {
                acc.Map.UseZaapTo((int)master.Path.Current_Action.m_delta);
                //acc.Wait(500);
            });
            master.Map.UseZaapTo((int)master.Path.Current_Action.m_delta);
            //master.Wait(500);
        }

        public void UseZaapiGroup()
        {
            var master = GetMaster();
            var slaves = GetSlaves();
            slaves.ForEach(acc =>
            {
                acc.Map.useZaapiTo((int)master.Path.Current_Action.m_delta);
                //acc.Wait(500);
            });
            master.Map.useZaapiTo((int)master.Path.Current_Action.m_delta);
            //master.Wait(500);
        }

        public List<Account.Account> GetSlaves()
        {
            return Accounts.Where(acc => !acc.IsMaster).ToList();
        }

        public void MoveToCellGroup(int delta)
        {
            Accounts.ForEach(acc =>
            {
                acc.Map.MoveToCell(delta);
                //acc.Wait(500);
            });
        }

        public void MoveToElementGroup(int delta)
        {
            Accounts.ForEach(acc =>
            {
                acc.Map.MoveToSecureElement(delta);
                //acc.Wait(500);
            });
        }

        public void TalkToNpcGroup(int delta)
        {
            Accounts.ForEach(acc =>
            {
                acc.Npc.TalkToNpc(delta);
                //acc.Wait(500);
            });
        }

        public void RequestExchangeGroup(string delta)
        {
            Accounts.ForEach(acc =>
            {
                acc.Inventory.RequestExchange(delta);
                acc.Wait(20000); // I have 20 sec to accept the trade request TODO: Change it!
                List<int> items = acc.Inventory.GetItemsToTransfer();
                acc.Inventory.TransferItems(items);
                acc.Wait(3000);
                acc.Inventory.TransferKamas();
                acc.Wait(3000);
                acc.Inventory.ExchangeReady();
            });
        }

        public void QuitAllAccToGroupButNotRemoveObjects()
        {
            Accounts.ForEach(acc =>
            {
                PartyLeaveRequestMessage msg = new PartyLeaveRequestMessage(PartyId);
                acc.SocketManager.Send(msg);
                acc.Wait(500);
                acc.IsMaster = false;
            });
        }

        public void InviteGroupByMaster()
        {
            var master = GetMaster();
            var slaves = GetSlaves();
            slaves.ForEach(accSlave =>
            {
                PartyInvitationRequestMessage msg = new PartyInvitationRequestMessage(accSlave.CharacterBaseInformations.Name);
                master.SocketManager.Send(msg);
            });
        }

        public void DefineNewFightLauncher()
        {
            var r = new Random();
            var i = r.Next(0, Accounts.Count - 2);
            var newMaster = Accounts.Where(
                                     acc => LastAccountLaunchedFight != null && acc.AccountName != LastAccountLaunchedFight.AccountName
                                           ).ToList()[i];
            if (newMaster != null)
            {
                newMaster.Log(new BotTextInformation("This is the Master account now !"), 1);
                Accounts.ForEach(acc =>
                {
                    acc.IsMaster = false;
                });
                newMaster.IsMaster = true;
                newMaster.MyGroup.Path.Account = newMaster;
            }
        }

        #endregion

    }
}