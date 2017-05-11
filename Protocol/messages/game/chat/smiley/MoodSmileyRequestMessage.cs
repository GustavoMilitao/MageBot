//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Chat.Smiley
{
    using BlueSheep.Protocol;


    public class MoodSmileyRequestMessage : Message
    {
        
        public const int ProtocolId = 6192;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ushort m_smileyId;
        
        public virtual ushort SmileyId
        {
            get
            {
                return m_smileyId;
            }
            set
            {
                m_smileyId = value;
            }
        }
        
        public MoodSmileyRequestMessage(ushort smileyId)
        {
            m_smileyId = smileyId;
        }
        
        public MoodSmileyRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(m_smileyId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_smileyId = reader.ReadVarUhShort();
        }
    }
}
