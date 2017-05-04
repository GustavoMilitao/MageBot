using DofusBot.Core.Network;

namespace DofusBot.Packet.Messages.Game.Initialization
{
    public class CharacterLoadingCompleteMessage : NetworkMessage
    {
        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.CharacterLoadingCompleteMessage; }
        }

        public const uint ProtocolId = 6471;
        public override uint MessageID { get { return ProtocolId; } }

        public CharacterLoadingCompleteMessage() { }

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
