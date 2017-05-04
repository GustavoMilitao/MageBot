using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Game.Friend
{
    public class FriendsGetListMessage : NetworkMessage
    {
        public ClientPacketEnum PacketType
        {
            get { return ClientPacketEnum.FriendsGetListMessage; }
        }

        public const uint ProtocolId = 4001;
        public override uint MessageID { get { return ProtocolId; } }

        public FriendsGetListMessage() { }

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
