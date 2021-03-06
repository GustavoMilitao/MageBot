//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Friend
{
    using BlueSheep.Common;


    public class FriendDeleteResultMessage : Message
    {
        
        public const int ProtocolId = 5601;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private bool m_success;
        
        public virtual bool Success
        {
            get
            {
                return m_success;
            }
            set
            {
                m_success = value;
            }
        }
        
        private string m_name;
        
        public virtual string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }
        
        public FriendDeleteResultMessage(bool success, string name)
        {
            m_success = success;
            m_name = name;
        }
        
        public FriendDeleteResultMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteBoolean(m_success);
            writer.WriteUTF(m_name);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_success = reader.ReadBoolean();
            m_name = reader.ReadUTF();
        }
    }
}
