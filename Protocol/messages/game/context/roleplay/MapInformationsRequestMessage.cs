 

namespace MageBot.Protocol.Messages.Game.Context.Roleplay
{
    public class MapInformationsRequestMessage : Message
    {
        public override int ProtocolId { get; } = 225;
        public override int MessageID { get { return ProtocolId; } }

        public int MapId { get; set; }

        public MapInformationsRequestMessage() { }

        public MapInformationsRequestMessage(int mapId)
        {
            MapId = mapId;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(MapId);
        }

        public override void Deserialize(IDataReader reader)
        {
            _mapIdFunc(reader);
        }

        private void _mapIdFunc(IDataReader reader)
        {
            MapId = reader.ReadInt();
        }
    }
}
