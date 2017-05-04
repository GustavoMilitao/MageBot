using DofusBot.Packet.Types.Game.Social;

namespace DofusBot.Packet.Types.Game.Context.Roleplay
{
    public class BasicAllianceInformations : AbstractSocialGroupInfos
    {
        public new TypeEnum Type
        {
            get { return TypeEnum.BasicAllianceInformations; }
        }

        public new const short ProtocolId = 419;
        public override short TypeID { get { return ProtocolId; } }

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
