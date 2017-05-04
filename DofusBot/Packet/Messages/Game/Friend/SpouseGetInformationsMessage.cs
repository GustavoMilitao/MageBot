using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Game.Friend
{
    public class SpouseGetInformationsMessage : NetworkMessage
    {
        public ClientPacketEnum PacketType
        {
            get { return ClientPacketEnum.SpouseGetInformationsMessage; }
        }

        public const uint ProtocolId = 6355;
        public override uint MessageID { get { return ProtocolId; } }

        public SpouseGetInformationsMessage() { }

        public override void Serialize(IDataWriter writer)
        {
            //
        }

        public override void Deserialize(IDataReader reader)
        {
            //
        }
    }
}
