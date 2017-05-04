using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Queues
{
    public class LoginQueueStatusMessage : NetworkMessage
    {
        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.LoginQueueStatusMessage; }
        }

        public const uint ProtocolId = 10;
        public override uint MessageID { get { return ProtocolId; } }

        public ushort Position;
        public ushort Total;

        public LoginQueueStatusMessage() { }

        public LoginQueueStatusMessage(ushort position, ushort total)
        {
            Position = position;
            Total = total;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)Position);
            writer.WriteShort((short)Total);
        }

        public override void Deserialize(IDataReader reader)
        {
            Position = reader.ReadUShort();
            Total = reader.ReadUShort();
        }
    }
}
