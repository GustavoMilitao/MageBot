//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Context.Roleplay.Job
{
    using BlueSheep.Common;


    public class JobCrafterDirectoryEntryRequestMessage : Message
    {
        
        public const int ProtocolId = 6043;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ulong m_playerId;
        
        public virtual ulong PlayerId
        {
            get
            {
                return m_playerId;
            }
            set
            {
                m_playerId = value;
            }
        }
        
        public JobCrafterDirectoryEntryRequestMessage(ulong playerId)
        {
            m_playerId = playerId;
        }
        
        public JobCrafterDirectoryEntryRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarLong(m_playerId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_playerId = reader.ReadVarUhLong();
        }
    }
}
