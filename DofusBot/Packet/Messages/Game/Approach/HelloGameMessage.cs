using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Game.Approach
{
    class HelloGameMessage : NetworkMessage
    {
        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.HelloGameMessage; }
        }

        public const uint ProtocolId = 101;
        public override uint MessageID { get { return ProtocolId; } }

        public HelloGameMessage() { }

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
