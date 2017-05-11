//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Idol
{
    using BlueSheep.Protocol;


    public class IdolSelectRequestMessage : Message
    {
        
        public const int ProtocolId = 6587;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private bool m_activate;
        
        public virtual bool Activate
        {
            get
            {
                return m_activate;
            }
            set
            {
                m_activate = value;
            }
        }
        
        private bool m_party;
        
        public virtual bool Party
        {
            get
            {
                return m_party;
            }
            set
            {
                m_party = value;
            }
        }
        
        private ushort m_idolId;
        
        public virtual ushort IdolId
        {
            get
            {
                return m_idolId;
            }
            set
            {
                m_idolId = value;
            }
        }
        
        public IdolSelectRequestMessage(bool activate, bool party, ushort idolId)
        {
            m_activate = activate;
            m_party = party;
            m_idolId = idolId;
        }
        
        public IdolSelectRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            byte flag = new byte();
            BooleanByteWrapper.SetFlag(0, flag, m_activate);
            BooleanByteWrapper.SetFlag(1, flag, m_party);
            writer.WriteByte(flag);
            writer.WriteVarShort(m_idolId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            byte flag = reader.ReadByte();
            m_activate = BooleanByteWrapper.GetFlag(flag, 0);
            m_party = BooleanByteWrapper.GetFlag(flag, 1);
            m_idolId = reader.ReadVarUhShort();
        }
    }
}
