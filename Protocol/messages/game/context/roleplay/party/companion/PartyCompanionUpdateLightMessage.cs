//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Context.Roleplay.Party.Companion
{
    using BlueSheep.Protocol.Messages.Game.Context.Roleplay.Party;


    public class PartyCompanionUpdateLightMessage : PartyUpdateLightMessage
    {
        
        public const int ProtocolId = 6472;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private byte m_indexId;
        
        public virtual byte IndexId
        {
            get
            {
                return m_indexId;
            }
            set
            {
                m_indexId = value;
            }
        }
        
        public PartyCompanionUpdateLightMessage(byte indexId)
        {
            m_indexId = indexId;
        }
        
        public PartyCompanionUpdateLightMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte(m_indexId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_indexId = reader.ReadByte();
        }
    }
}
