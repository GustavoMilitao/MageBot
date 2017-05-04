using DofusBot.Core.Network;
namespace DofusBot.Packet.Messages.Game.Basic
{
    class BasicAckMessage : NetworkMessage
    {
        public ServerPacketEnum PacketType
        {
            get { return ServerPacketEnum.BasicAckMessage; }
        }

        public uint Seq;
        public ushort LastPacketId;

        public const uint ProtocolId = 6362;
        public override uint MessageID { get { return ProtocolId; } }

        public BasicAckMessage() { }

        public BasicAckMessage(uint seq, ushort lastPacketId)
        {
            Seq = seq;
            LastPacketId = lastPacketId;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(Seq);
            writer.WriteVarShort(LastPacketId);
        }

        public override void Deserialize(IDataReader reader)
        {
            Seq = reader.ReadVarUhInt();
            LastPacketId = reader.ReadVarUhShort();
        }
    }
}
