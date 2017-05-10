//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Inventory.Exchanges
{
    using BlueSheep.Common.Protocol.Types.Game.Data.Items;
    using System.Collections.Generic;
    using BlueSheep.Common;


    public class ExchangeStartOkTaxCollectorMessage : Message
    {
        
        public const int ProtocolId = 5780;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private int m_collectorId;
        
        public virtual int CollectorId
        {
            get
            {
                return m_collectorId;
            }
            set
            {
                m_collectorId = value;
            }
        }
        
        private List<ObjectItem> m_objectsInfos;
        
        public virtual List<ObjectItem> ObjectsInfos
        {
            get
            {
                return m_objectsInfos;
            }
            set
            {
                m_objectsInfos = value;
            }
        }
        
        private uint m_goldInfo;
        
        public virtual uint GoldInfo
        {
            get
            {
                return m_goldInfo;
            }
            set
            {
                m_goldInfo = value;
            }
        }
        
        public ExchangeStartOkTaxCollectorMessage(int collectorId, List<ObjectItem> objectsInfos, uint goldInfo)
        {
            m_collectorId = collectorId;
            m_objectsInfos = objectsInfos;
            m_goldInfo = goldInfo;
        }
        
        public ExchangeStartOkTaxCollectorMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(m_collectorId);
            writer.WriteShort(((short)(m_objectsInfos.Count)));
            int objectsInfosIndex;
            for (objectsInfosIndex = 0; (objectsInfosIndex < m_objectsInfos.Count); objectsInfosIndex = (objectsInfosIndex + 1))
            {
                ObjectItem objectToSend = m_objectsInfos[objectsInfosIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteVarInt(m_goldInfo);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_collectorId = reader.ReadInt();
            int objectsInfosCount = reader.ReadUShort();
            int objectsInfosIndex;
            m_objectsInfos = new System.Collections.Generic.List<ObjectItem>();
            for (objectsInfosIndex = 0; (objectsInfosIndex < objectsInfosCount); objectsInfosIndex = (objectsInfosIndex + 1))
            {
                ObjectItem objectToAdd = new ObjectItem();
                objectToAdd.Deserialize(reader);
                m_objectsInfos.Add(objectToAdd);
            }
            m_goldInfo = reader.ReadVarUhInt();
        }
    }
}
