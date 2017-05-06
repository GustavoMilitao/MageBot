using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages.Game.Basic
{
    public class BasicAckMessage : Message
    {
        public new const int ID = 6362;
        public override int MessageID { get { return ID; } }

        public uint Seq;
        public ushort LastProtocolId;

        public BasicAckMessage() { }

        public BasicAckMessage(uint seq, ushort lastProtocolId)
        {
            Seq = seq;
            LastProtocolId = lastProtocolId;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(Seq);
            writer.WriteVarShort(LastProtocolId);
        }

        public override void Deserialize(IDataReader reader)
        {
            Seq = reader.ReadVarUhInt();
            LastProtocolId = reader.ReadVarUhShort();
        }
    }
}
