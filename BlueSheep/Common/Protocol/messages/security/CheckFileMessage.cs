//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Security
{
    using BlueSheep.Common;


    public class CheckFileMessage : Message
    {
        
        public const int ProtocolId = 6156;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private string m_filenameHash;
        
        public virtual string FilenameHash
        {
            get
            {
                return m_filenameHash;
            }
            set
            {
                m_filenameHash = value;
            }
        }
        
        private byte m_type;
        
        public virtual byte Type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }
        
        private string m_value;
        
        public virtual string Value
        {
            get
            {
                return m_value;
            }
            set
            {
                m_value = value;
            }
        }
        
        public CheckFileMessage(string filenameHash, byte type, string value)
        {
            m_filenameHash = filenameHash;
            m_type = type;
            m_value = value;
        }
        
        public CheckFileMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteUTF(m_filenameHash);
            writer.WriteByte(m_type);
            writer.WriteUTF(m_value);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_filenameHash = reader.ReadUTF();
            m_type = reader.ReadByte();
            m_value = reader.ReadUTF();
        }
    }
}
