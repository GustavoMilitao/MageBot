using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Game.Approach
{
    class ServerSettingsMessage : NetworkMessage
    {
        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.ServerSettingsMessage; }
        }

        public string Lang;
        public byte Community;
        public byte GameType;
        public ushort ArenaLeaveBanTime;

        public const uint ProtocolId = 6340;
        public override uint MessageID { get { return ProtocolId; } }

        public ServerSettingsMessage() { }

        public ServerSettingsMessage(string lang, byte community, byte gameType, ushort arenaLeaveBanTime)
        {
            Lang = lang;
            Community = community;
            GameType = gameType;
            ArenaLeaveBanTime = arenaLeaveBanTime;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(Lang);
            writer.WriteByte(Community);
            writer.WriteByte(GameType);
            writer.WriteVarShort(ArenaLeaveBanTime);
        }

        public override void Deserialize(IDataReader reader)
        {
            Lang = reader.ReadUTF();
            Community = reader.ReadByte();
            GameType = reader.ReadByte();
            ArenaLeaveBanTime = reader.ReadVarUhShort();
        }
    }
}
