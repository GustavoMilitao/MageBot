using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Game.Context.Roleplay
{
    public class MapInformationsRequestMessage : NetworkMessage
    {
        public ClientPacketEnum PacketType
        {
            get { return ClientPacketEnum.MapInformationsRequestMessage; }
        }

        public const uint ProtocolId = 225;
        public override uint MessageID { get { return ProtocolId; } }

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
