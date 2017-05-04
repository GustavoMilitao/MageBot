using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Game.Friend
{
    public class IgnoredGetListMessage : NetworkMessage
    {
        public ClientPacketEnum PacketType
        {
            get { return ClientPacketEnum.IgnoredGetListMessage; }
        }

        public const uint ProtocolId = 5676;
        public override uint MessageID { get { return ProtocolId; } }

        public IgnoredGetListMessage() { }

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
