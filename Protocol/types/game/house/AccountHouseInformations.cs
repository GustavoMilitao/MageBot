//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Types.Game.House
{


    public class AccountHouseInformations : HouseInformations
    {
        
        public const int ProtocolId = 390;
        
        public override int TypeID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private HouseInstanceInformations m_houseInfos;
        
        public virtual HouseInstanceInformations HouseInfos
        {
            get
            {
                return m_houseInfos;
            }
            set
            {
                m_houseInfos = value;
            }
        }
        
        private ulong m_realPrice;
        
        public virtual ulong RealPrice
        {
            get
            {
                return m_realPrice;
            }
            set
            {
                m_realPrice = value;
            }
        }
        
        private bool m_isLocked;
        
        public virtual bool IsLocked
        {
            get
            {
                return m_isLocked;
            }
            set
            {
                m_isLocked = value;
            }
        }
        
        private short m_worldX;
        
        public virtual short WorldX
        {
            get
            {
                return m_worldX;
            }
            set
            {
                m_worldX = value;
            }
        }
        
        private short m_worldY;
        
        public virtual short WorldY
        {
            get
            {
                return m_worldY;
            }
            set
            {
                m_worldY = value;
            }
        }
        
        private int m_mapId;
        
        public virtual int MapId
        {
            get
            {
                return m_mapId;
            }
            set
            {
                m_mapId = value;
            }
        }
        
        private ushort m_subAreaId;
        
        public virtual ushort SubAreaId
        {
            get
            {
                return m_subAreaId;
            }
            set
            {
                m_subAreaId = value;
            }
        }
        
        public AccountHouseInformations(HouseInstanceInformations houseInfos, ulong realPrice, bool isLocked, short worldX, short worldY, int mapId, ushort subAreaId)
        {
            m_houseInfos = houseInfos;
            m_realPrice = realPrice;
            m_isLocked = isLocked;
            m_worldX = worldX;
            m_worldY = worldY;
            m_mapId = mapId;
            m_subAreaId = subAreaId;
        }
        
        public AccountHouseInformations()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUShort(((ushort)(m_houseInfos.TypeID)));
            m_houseInfos.Serialize(writer);
            writer.WriteVarLong(m_realPrice);
            writer.WriteBoolean(m_isLocked);
            writer.WriteShort(m_worldX);
            writer.WriteShort(m_worldY);
            writer.WriteInt(m_mapId);
            writer.WriteVarShort(m_subAreaId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_houseInfos = ProtocolTypeManager.GetInstance<HouseInstanceInformations>(reader.ReadUShort());
            m_houseInfos.Deserialize(reader);
            m_realPrice = reader.ReadVarUhLong();
            m_isLocked = reader.ReadBoolean();
            m_worldX = reader.ReadShort();
            m_worldY = reader.ReadShort();
            m_mapId = reader.ReadInt();
            m_subAreaId = reader.ReadVarUhShort();
        }
    }
}
