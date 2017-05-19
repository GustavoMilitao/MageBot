using MageBot.Protocol.Messages;
using MageBot.Protocol.Messages.Game.Context.Roleplay;
using MageBot.Protocol.Messages.Game.Context.Roleplay.Fight;
using MageBot.Protocol.Types.Game.Context.Roleplay;
using MageBot.Util.IO;
using System;
using System.Collections.Generic;
using Util.Util.Enums.Internal;

namespace MageBot.Core.Heroic
{
    public class Heroic
    {
        public Account.Account account { get; set; }


        public Heroic(Account.Account account)
        {
            this.account = account;

            account.Config.AlliancesNameRun = new List<string>();
            account.Config.AlliancesNameAgro = new List<string>();
        }

        #region Public Methods
        public void AnalysePacket(Message msg, byte[] packetdatas)
        {

            using (BigEndianReader reader = new BigEndianReader(packetdatas))
            {
                msg.Deserialize(reader);
            }
            switch (msg.MessageID)
            {
                case 226:
                    MapComplementaryInformationsDataMessage packet = (MapComplementaryInformationsDataMessage)msg;
                    //if (this.GoAnalyser((int)packet.SubAreaId))
                    //{
                    foreach (GameRolePlayActorInformations informations in packet.Actors)
                    {
                        GameRolePlayCharacterInformations infos;
                        if (!(informations is GameRolePlayCharacterInformations))
                            continue;
                        else
                            infos = (GameRolePlayCharacterInformations)informations;
                        if (GoAgro(infos))
                        {
                            Agression((ulong)informations.ContextualId);
                        }
                        if (IsGoingToRun(infos))
                        {
                            if (account.Config.DisconnectWhenRun)
                            {
                                account.SocketManager.DisconnectFromGUI();
                            }
                            else if (account.Config.UseItemWhenRun && account.Config.ItemToUseWhenRun.HasValue)
                            {
                                Run();
                            }
                        }
                    }

                    break;
                case 5632:
                    GameRolePlayShowActorMessage npacket = (GameRolePlayShowActorMessage)msg;
                    GameRolePlayCharacterInformations infoCharacter = npacket.Informations as GameRolePlayCharacterInformations;
                    if (GoAgro(infoCharacter))
                    {
                        Agression((ulong)infoCharacter.ContextualId);
                    }
                    if (IsGoingToRun(infoCharacter))
                    {
                        if (account.Config.DisconnectWhenRun)
                        {
                            account.SocketManager.DisconnectFromGUI();
                        }
                        else if (account.Config.UseItemWhenRun && account.Config.ItemToUseWhenRun.HasValue)
                        {
                            Run();
                        }
                    }
                    break;

            }
        }
        #endregion

        #region Private Methods
        private void Agression(ulong targetid)
        {
            GameRolePlayPlayerFightRequestMessage packet = new GameRolePlayPlayerFightRequestMessage
            {
                Friendly = false,
                TargetCellId = -1,
                TargetId = targetid
            };

            account.SocketManager.Send(packet);
        }

        private void Run()
        {
            int item = SwitchUid(account.Config.ItemToUseWhenRun.Value);
            if (!account.Inventory.ItemExists(item))
            {
                account.SocketManager.DisconnectFromGUI();
            }
            else
            {
                account.Inventory.UseItem(item);
            }
        }

        private bool GoAgro(GameRolePlayCharacterInformations infoCharacter)
        {
            if (!account.Config.AgroConditionsSet)
                return false;
            long num = Math.Abs((long)(infoCharacter.AlignmentInfos.CharacterPower - infoCharacter.ContextualId));
            bool flag = (account.Config.AgroConditionsSet && (infoCharacter.Name != account.CharacterBaseInformations.Name)) && (num >= account.Config.MinLevelAgro) && (num <= account.Config.MaxLevelAgro);
            if (((account.Config.AlliancesNameAgro.Count > 0) && (infoCharacter.HumanoidInfo.Options[1] != null)) && flag)
            {
                HumanOptionAlliance alliance = infoCharacter.HumanoidInfo.Options[1] as HumanOptionAlliance;
                return (flag && account.Config.AlliancesNameAgro.Contains(alliance.AllianceInformations.AllianceName));
            }
            return flag;
        }

        private bool IsGoingToRun(GameRolePlayCharacterInformations infoCharacter)
        {
            if (!account.Config.RunConditionsSet)
                return false;
            if (infoCharacter.HumanoidInfo.Options[1] == null)
            {
                return false;
            }
            long num = Math.Abs((long)(infoCharacter.AlignmentInfos.CharacterPower - infoCharacter.ContextualId));
            bool flag = (account.Config.RunConditionsSet && (infoCharacter.Name != account.CharacterBaseInformations.Name)) && (num >= account.Config.MinLevelRun) && (num <= account.Config.MaxLevelRun);
            if (((account.Config.AlliancesNameRun.Count > 0) && (infoCharacter.HumanoidInfo.Options[1] != null)) && flag)
            {
                HumanOptionAlliance alliance = infoCharacter.HumanoidInfo.Options[1] as HumanOptionAlliance;
                return (flag && account.Config.AlliancesNameRun.Contains(alliance.AllianceInformations.AllianceName));
            }
            return flag;
        }

        private int SwitchUid(PotionEnum potionEnum)
        {
            switch (potionEnum)
            {
                case PotionEnum.MemoryPotion:
                    return account.Inventory.GetItemFromGID(0x224).UID;

                case PotionEnum.BontaPotion:
                    return account.Inventory.GetItemFromGID(0x1b35).UID;

                case PotionEnum.BrakmarPotion:
                    return account.Inventory.GetItemFromGID(0x1b34).UID;
            }
            return 0;
        }
        #endregion
    }
}
