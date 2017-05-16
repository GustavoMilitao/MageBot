using BlueSheep.Protocol.Types.Game.Social;

namespace BlueSheep.Protocol.Types.Game.Context.Roleplay
{
    public class BasicAllianceInformations : AbstractSocialGroupInfos
    {
        protected override int ProtocolId { get; set; } = 419;
        public override int TypeID { get { return ProtocolId; } }

        public uint AllianceId;
        public string AllianceTag;

        public BasicAllianceInformations() { }

        public BasicAllianceInformations(uint allianceId, string allianceTag)
        {
            AllianceId = allianceId;
            AllianceTag = allianceTag;
        }

        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarInt(AllianceId);
            writer.WriteUTF(AllianceTag);
        }

        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            AllianceId = reader.ReadVarUhInt();
            AllianceTag = reader.ReadUTF();
        }
    }
}
