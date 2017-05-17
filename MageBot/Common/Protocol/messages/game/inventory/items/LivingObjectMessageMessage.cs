//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Inventory.Items
{
    using BlueSheep.Common;


    public class LivingObjectMessageMessage : Message
    {
        
        public const int ProtocolId = 6065;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
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
        
        private int m_timeStamp;
        
        public virtual int TimeStamp
        {
            get
            {
                return m_timeStamp;
            }
            set
            {
                m_timeStamp = value;
            }
        }
        
        private string m_owner;
        
        public virtual string Owner
        {
            get
            {
                return m_owner;
            }
            set
            {
                m_owner = value;
            }
        }
        
        private ushort m_objectGenericId;
        
        public virtual ushort ObjectGenericId
        {
            get
            {
                return m_objectGenericId;
            }
            set
            {
                m_objectGenericId = value;
            }
        }
        
        public LivingObjectMessageMessage(ushort msgId, int timeStamp, string owner, ushort objectGenericId)
        {
            m_msgId = msgId;
            m_timeStamp = timeStamp;
            m_owner = owner;
            m_objectGenericId = objectGenericId;
        }
        
        public LivingObjectMessageMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(m_msgId);
            writer.WriteInt(m_timeStamp);
            writer.WriteUTF(m_owner);
            writer.WriteVarShort(m_objectGenericId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_msgId = reader.ReadVarUhShort();
            m_timeStamp = reader.ReadInt();
            m_owner = reader.ReadUTF();
            m_objectGenericId = reader.ReadVarUhShort();
        }
    }
}