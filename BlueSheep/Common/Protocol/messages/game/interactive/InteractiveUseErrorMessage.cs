//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Interactive
{
    using BlueSheep.Common;


    public class InteractiveUseErrorMessage : Message
    {
        
        public const int ProtocolId = 6384;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private uint m_elemId;
        
        public virtual uint ElemId
        {
            get
            {
                return m_elemId;
            }
            set
            {
                m_elemId = value;
            }
        }
        
        private uint m_skillInstanceUid;
        
        public virtual uint SkillInstanceUid
        {
            get
            {
                return m_skillInstanceUid;
            }
            set
            {
                m_skillInstanceUid = value;
            }
        }
        
        public InteractiveUseErrorMessage(uint elemId, uint skillInstanceUid)
        {
            m_elemId = elemId;
            m_skillInstanceUid = skillInstanceUid;
        }
        
        public InteractiveUseErrorMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(m_elemId);
            writer.WriteVarInt(m_skillInstanceUid);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_elemId = reader.ReadVarUhInt();
            m_skillInstanceUid = reader.ReadVarUhInt();
        }
    }
}
