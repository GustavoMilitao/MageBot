using BlueSheep.Protocol.Messages;
using BlueSheep.Protocol.Messages.Game.Context.Roleplay;
using BlueSheep.Protocol.Messages.Game.Context.Roleplay.Fight;
using BlueSheep.Protocol.Types.Game.Context.Roleplay;
using BlueSheep.Util.IO;
using System;
using System.Collections.Generic;
using Util.Util.Enums.Internal;

namespace BlueSheep.Core.Heroic
{
    public class Heroic
    {
        public Account.Account account { get; set; }
        public bool HeroicModeOn { get; set; }
        public bool AgroConditionsSet { get; set; }
        public bool RunConditionsSet { get; set; }
        public long MinLevelRun { get; set; }
        public long MaxLevelRun { get; set; }
        public List<string> AlliancesNameRun { get; set; }
        public List<string> AlliancesNameAgro { get; set; }
        public bool DisconnectWhenRun { get; set; }
        public bool UseItemWhenRun { get; set; }
        public PotionEnum? ItemToUseWhenRun { get; set; }
        public long MinLevelAgro { get; set; }
        public long MaxLevelAgro { get; set; }


        public Heroic(Account.Account account)
        {
            this.account = account;

            AlliancesNameRun = new List<string>();
            AlliancesNameAgro = new List<string>();
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
                            if (DisconnectWhenRun)
                            {
                                account.SocketManager.DisconnectFromGUI();
                            }
                            else if (UseItemWhenRun && ItemToUseWhenRun.HasValue)
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
                        if (DisconnectWhenRun)
                        {
                            account.SocketManager.DisconnectFromGUI();
                        }
                        else if (UseItemWhenRun && ItemToUseWhenRun.HasValue)
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
            int item = SwitchUid(ItemToUseWhenRun.Value);
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
            if (!AgroConditionsSet)
                return false;
            long num = Math.Abs((long)(infoCharacter.AlignmentInfos.CharacterPower - infoCharacter.ContextualId));
            bool flag = (AgroConditionsSet && (infoCharacter.Name != account.CharacterBaseInformations.Name)) && (num >= MinLevelAgro) && (num <= MaxLevelAgro);
            if (((AlliancesNameAgro.Count > 0) && (infoCharacter.HumanoidInfo.Options[1] != null)) && flag)
            {
                HumanOptionAlliance alliance = infoCharacter.HumanoidInfo.Options[1] as HumanOptionAlliance;
                return (flag && AlliancesNameAgro.Contains(alliance.AllianceInformations.AllianceName));
            }
            return flag;
        }

        private bool IsGoingToRun(GameRolePlayCharacterInformations infoCharacter)
        {
            if (!RunConditionsSet)
                return false;
            if (infoCharacter.HumanoidInfo.Options[1] == null)
            {
                return false;
            }
            long num = Math.Abs((long)(infoCharacter.AlignmentInfos.CharacterPower - infoCharacter.ContextualId));
            bool flag = (RunConditionsSet && (infoCharacter.Name != account.CharacterBaseInformations.Name)) && (num >= MinLevelRun) && (num <= MaxLevelRun);
            if (((AlliancesNameRun.Count > 0) && (infoCharacter.HumanoidInfo.Options[1] != null)) && flag)
            {
                HumanOptionAlliance alliance = infoCharacter.HumanoidInfo.Options[1] as HumanOptionAlliance;
                return (flag && AlliancesNameRun.Contains(alliance.AllianceInformations.AllianceName));
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
