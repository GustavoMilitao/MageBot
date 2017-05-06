using BlueSheep.Common.Protocol.Types.Game.Social;

namespace BlueSheep.Common.Protocol.Types.Game.Context.Roleplay
{
    public class BasicAllianceInformations : AbstractSocialGroupInfos
    {
        public new const int ID = 419;
        public virtual int TypeID { get { return ID; } }

        public uint AllianceId;
        public string AllianceTag;

        public BasicAllianceInformations() { }

        public BasicAllianceInformations(uint allianceId, string allianceTag)
        {
            AllianceId = allianceId;
            AllianceTag = allianceTag;
        }

        public void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarInt(AllianceId);
            writer.WriteUTF(AllianceTag);
        }

        public void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            AllianceId = reader.ReadVarUhInt();
            AllianceTag = reader.ReadUTF();
        }
    }
}
