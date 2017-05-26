//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MageBot.Protocol.Messages.Server.Basic
{
    using System.Collections.Generic;


    public class SystemMessageDisplayMessage : Message
    {
        
        public override int ProtocolId { get; } = 189;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private List<System.String> m_parameters;
        
        public virtual List<System.String> Parameters
        {
            get
            {
                return m_parameters;
            }
            set
            {
                m_parameters = value;
            }
        }
        
        private bool m_hangUp;
        
        public virtual bool HangUp
        {
            get
            {
                return m_hangUp;
            }
            set
            {
                m_hangUp = value;
            }
        }
        
        private ushort m_msgId;
        
        public virtual ushort MsgId
        {
            get
            {
                return m_msgId;
            }
            set
            {
                m_msgId = value;
            }
        }
        
        public SystemMessageDisplayMessage(List<System.String> parameters, bool hangUp, ushort msgId)
        {
            m_parameters = parameters;
            m_hangUp = hangUp;
            m_msgId = msgId;
        }
        
        public SystemMessageDisplayMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(((short)(m_parameters.Count)));
            int parametersIndex;
            for (parametersIndex = 0; (parametersIndex < m_parameters.Count); parametersIndex = (parametersIndex + 1))
            {
                writer.WriteUTF(m_parameters[parametersIndex]);
            }
            writer.WriteBoolean(m_hangUp);
            writer.WriteVarShort(m_msgId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_hangUp = reader.ReadBoolean();
            m_msgId = reader.ReadVarUhShort();
            int parametersCount = reader.ReadUShort();
            int parametersIndex;
            m_parameters = new System.Collections.Generic.List<string>();
            for (parametersIndex = 0; (parametersIndex < parametersCount); parametersIndex = (parametersIndex + 1))
            {
                m_parameters.Add(reader.ReadUTF());
            }
        }
    }
}
