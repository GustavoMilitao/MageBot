 

namespace MageBot.Protocol.Messages.Game.Approach
{
    public class ServerSettingsMessage : Message
    {
        public override int ProtocolId { get; } = 6340;
        public override int MessageID { get { return ProtocolId; } }

        public string Lang;
        public byte Community;
        public byte GameType;
        public ushort ArenaLeaveBanTime;

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
