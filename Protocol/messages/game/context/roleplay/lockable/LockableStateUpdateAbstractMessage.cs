//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Context.Roleplay.Lockable
{
    using BlueSheep.Protocol;


    public class LockableStateUpdateAbstractMessage : Message
    {
        
        public const int ProtocolId = 5671;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private bool m_locked;
        
        public virtual bool Locked
        {
            get
            {
                return m_locked;
            }
            set
            {
                m_locked = value;
            }
        }
        
        public LockableStateUpdateAbstractMessage(bool locked)
        {
            m_locked = locked;
        }
        
        public LockableStateUpdateAbstractMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(m_locked);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_locked = reader.ReadBoolean();
        }
    }
}
