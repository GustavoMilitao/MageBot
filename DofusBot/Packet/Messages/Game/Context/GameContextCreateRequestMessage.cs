using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Game.Context
{
    public class GameContextCreateRequestMessage : NetworkMessage
    {
        public ClientPacketEnum PacketType
        {
            get { return ClientPacketEnum.GameContextCreateRequestMessage; }
        }

        public const uint ProtocolId = 250;
        public override uint MessageID { get { return ProtocolId; } }

        public GameContextCreateRequestMessage() { }

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
