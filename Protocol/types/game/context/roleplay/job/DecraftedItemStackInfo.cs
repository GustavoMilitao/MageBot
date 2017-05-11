//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Types.Game.Context.Roleplay.Job
{
    using System.Collections.Generic;
    using BlueSheep.Protocol.Types;


    public class DecraftedItemStackInfo : NetworkType
    {
        
        public const int ProtocolId = 481;
        
        public override int TypeID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private List<System.UInt16> m_runesId;
        
        public virtual List<System.UInt16> RunesId
        {
            get
            {
                return m_runesId;
            }
            set
            {
                m_runesId = value;
            }
        }
        
        private List<System.UInt32> m_runesQty;
        
        public virtual List<System.UInt32> RunesQty
        {
            get
            {
                return m_runesQty;
            }
            set
            {
                m_runesQty = value;
            }
        }
        
        private uint m_objectUID;
        
        public virtual uint ObjectUID
        {
            get
            {
                return m_objectUID;
            }
            set
            {
                m_objectUID = value;
            }
        }
        
        private float m_bonusMin;
        
        public virtual float BonusMin
        {
            get
            {
                return m_bonusMin;
            }
            set
            {
                m_bonusMin = value;
            }
        }
        
        private float m_bonusMax;
        
        public virtual float BonusMax
        {
            get
            {
                return m_bonusMax;
            }
            set
            {
                m_bonusMax = value;
            }
        }
        
        public DecraftedItemStackInfo(List<System.UInt16> runesId, List<System.UInt32> runesQty, uint objectUID, float bonusMin, float bonusMax)
        {
            m_runesId = runesId;
            m_runesQty = runesQty;
            m_objectUID = objectUID;
            m_bonusMin = bonusMin;
            m_bonusMax = bonusMax;
        }
        
        public DecraftedItemStackInfo()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(((short)(m_runesId.Count)));
            int runesIdIndex;
            for (runesIdIndex = 0; (runesIdIndex < m_runesId.Count); runesIdIndex = (runesIdIndex + 1))
            {
                writer.WriteVarShort(m_runesId[runesIdIndex]);
            }
            writer.WriteShort(((short)(m_runesQty.Count)));
            int runesQtyIndex;
            for (runesQtyIndex = 0; (runesQtyIndex < m_runesQty.Count); runesQtyIndex = (runesQtyIndex + 1))
            {
                writer.WriteVarInt(m_runesQty[runesQtyIndex]);
            }
            writer.WriteVarInt(m_objectUID);
            writer.WriteFloat(m_bonusMin);
            writer.WriteFloat(m_bonusMax);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            int runesIdCount = reader.ReadUShort();
            int runesIdIndex;
            m_runesId = new System.Collections.Generic.List<ushort>();
            for (runesIdIndex = 0; (runesIdIndex < runesIdCount); runesIdIndex = (runesIdIndex + 1))
            {
                m_runesId.Add(reader.ReadVarUhShort());
            }
            int runesQtyCount = reader.ReadUShort();
            int runesQtyIndex;
            m_runesQty = new System.Collections.Generic.List<uint>();
            for (runesQtyIndex = 0; (runesQtyIndex < runesQtyCount); runesQtyIndex = (runesQtyIndex + 1))
            {
                m_runesQty.Add(reader.ReadVarUhInt());
            }
            m_objectUID = reader.ReadVarUhInt();
            m_bonusMin = reader.ReadFloat();
            m_bonusMax = reader.ReadFloat();
        }
    }
}
