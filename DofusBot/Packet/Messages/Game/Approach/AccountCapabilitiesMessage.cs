using DofusBot.Core.Network;
using DofusBot.Core.Network.Utils;

namespace DofusBot.Packet.Messages.Game.Approach
{
    class AccountCapabilitiesMessage : NetworkMessage
    {
        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.AccountCapabilitiesMessage; }
        }

        public int AccountId;
        public bool TutorialAvailable;
        public uint BreedsVisible;
        public uint BreedsAvailable;
        public byte Status;
        public bool CanCreateNewCharacter;

        public const uint ProtocolId = 6216;
        public override uint MessageID { get { return ProtocolId; } }

        public AccountCapabilitiesMessage() { }

        public AccountCapabilitiesMessage(int accountId, bool tutorialAvailable, uint breedsVisible, uint breedsAvailable, byte status, bool canCreateNewCharacter)
        {
            AccountId = accountId;
            TutorialAvailable = tutorialAvailable;
            BreedsVisible = breedsVisible;
            BreedsAvailable = breedsAvailable;
            Status = status;
            CanCreateNewCharacter = canCreateNewCharacter;
        }

        public override void Serialize(IDataWriter writer)
        {
            byte flag = new byte();
            BooleanByteWrapper.SetFlag(0, flag, TutorialAvailable);
            BooleanByteWrapper.SetFlag(1, flag, CanCreateNewCharacter);
            writer.WriteByte(flag);
            writer.WriteInt(AccountId);
            writer.WriteVarInt(BreedsVisible);
            writer.WriteVarInt(BreedsAvailable);
            writer.WriteByte(Status);
        }

        public override void Deserialize(IDataReader reader)
        {
            byte flag = reader.ReadByte();
            TutorialAvailable = BooleanByteWrapper.GetFlag(flag, 0);
            CanCreateNewCharacter = BooleanByteWrapper.GetFlag(flag, 1);
            AccountId = reader.ReadInt();
            BreedsVisible = reader.ReadVarUhInt();
            BreedsAvailable = reader.ReadVarUhInt();
            Status = reader.ReadByte();
        }
    }
}
