//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Context.Mount
{
    using BlueSheep.Protocol;


    public class MountEmoteIconUsedOkMessage : Message
    {
        
        public const int ProtocolId = 5978;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private int m_mountId;
        
        public virtual int MountId
        {
            get
            {
                return m_mountId;
            }
            set
            {
                m_mountId = value;
            }
        }
        
        private byte m_reactionType;
        
        public virtual byte ReactionType
        {
            get
            {
                return m_reactionType;
            }
            set
            {
                m_reactionType = value;
            }
        }
        
        public MountEmoteIconUsedOkMessage(int mountId, byte reactionType)
        {
            m_mountId = mountId;
            m_reactionType = reactionType;
        }
        
        public MountEmoteIconUsedOkMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(m_mountId);
            writer.WriteByte(m_reactionType);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_mountId = reader.ReadVarInt();
            m_reactionType = reader.ReadByte();
        }
    }
}
