//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Types.Game.Context.Roleplay
{


    public class HumanOptionObjectUse : HumanOption
    {
        
        public const int ProtocolId = 449;
        
        public override int TypeID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private byte m_delayTypeId;
        
        public virtual byte DelayTypeId
        {
            get
            {
                return m_delayTypeId;
            }
            set
            {
                m_delayTypeId = value;
            }
        }
        
        private double m_delayEndTime;
        
        public virtual double DelayEndTime
        {
            get
            {
                return m_delayEndTime;
            }
            set
            {
                m_delayEndTime = value;
            }
        }
        
        private ushort m_objectGID;
        
        public virtual ushort ObjectGID
        {
            get
            {
                return m_objectGID;
            }
            set
            {
                m_objectGID = value;
            }
        }
        
        public HumanOptionObjectUse(byte delayTypeId, double delayEndTime, ushort objectGID)
        {
            m_delayTypeId = delayTypeId;
            m_delayEndTime = delayEndTime;
            m_objectGID = objectGID;
        }
        
        public HumanOptionObjectUse()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte(m_delayTypeId);
            writer.WriteDouble(m_delayEndTime);
            writer.WriteVarShort(m_objectGID);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_delayTypeId = reader.ReadByte();
            m_delayEndTime = reader.ReadDouble();
            m_objectGID = reader.ReadVarUhShort();
        }
    }
}
