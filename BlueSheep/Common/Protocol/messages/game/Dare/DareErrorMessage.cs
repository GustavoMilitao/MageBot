//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Dare
{
    using BlueSheep.Common;


    public class DareErrorMessage : Message
    {
        
        public const int ProtocolId = 6667;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private byte m_error;
        
        public virtual byte Error
        {
            get
            {
                return m_error;
            }
            set
            {
                m_error = value;
            }
        }
        
        public DareErrorMessage(byte error)
        {
            m_error = error;
        }
        
        public DareErrorMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(m_error);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_error = reader.ReadByte();
        }
    }
}
