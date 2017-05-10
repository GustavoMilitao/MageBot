//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Packs
{
    using BlueSheep.Common;


    public class PackRestrictedSubAreaMessage : Message
    {
        
        public const int ProtocolId = 6186;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private int m_subAreaId;
        
        public virtual int SubAreaId
        {
            get
            {
                return m_subAreaId;
            }
            set
            {
                m_subAreaId = value;
            }
        }
        
        public PackRestrictedSubAreaMessage(int subAreaId)
        {
            m_subAreaId = subAreaId;
        }
        
        public PackRestrictedSubAreaMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(m_subAreaId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_subAreaId = reader.ReadInt();
        }
    }
}
