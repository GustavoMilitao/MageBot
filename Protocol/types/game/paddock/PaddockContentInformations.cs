//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Types.Game.Paddock
{
    using System.Collections.Generic;


    public class PaddockContentInformations : PaddockInformations
    {
        
        public const int ProtocolId = 183;
        
        public override int TypeID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private List<MountInformationsForPaddock> m_mountsInformations;
        
        public virtual List<MountInformationsForPaddock> MountsInformations
        {
            get
            {
                return m_mountsInformations;
            }
            set
            {
                m_mountsInformations = value;
            }
        }
        
        private int m_paddockId;
        
        public virtual int PaddockId
        {
            get
            {
                return m_paddockId;
            }
            set
            {
                m_paddockId = value;
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
        
        private bool m_abandonned;
        
        public virtual bool Abandonned
        {
            get
            {
                return m_abandonned;
            }
            set
            {
                m_abandonned = value;
            }
        }
        
        public PaddockContentInformations(List<MountInformationsForPaddock> mountsInformations, int paddockId, short worldX, short worldY, int mapId, ushort subAreaId, bool abandonned)
        {
            m_mountsInformations = mountsInformations;
            m_paddockId = paddockId;
            m_worldX = worldX;
            m_worldY = worldY;
            m_mapId = mapId;
            m_subAreaId = subAreaId;
            m_abandonned = abandonned;
        }
        
        public PaddockContentInformations()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(((short)(m_mountsInformations.Count)));
            int mountsInformationsIndex;
            for (mountsInformationsIndex = 0; (mountsInformationsIndex < m_mountsInformations.Count); mountsInformationsIndex = (mountsInformationsIndex + 1))
            {
                MountInformationsForPaddock objectToSend = m_mountsInformations[mountsInformationsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteInt(m_paddockId);
            writer.WriteShort(m_worldX);
            writer.WriteShort(m_worldY);
            writer.WriteInt(m_mapId);
            writer.WriteVarShort(m_subAreaId);
            writer.WriteBoolean(m_abandonned);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            int mountsInformationsCount = reader.ReadUShort();
            int mountsInformationsIndex;
            m_mountsInformations = new System.Collections.Generic.List<MountInformationsForPaddock>();
            for (mountsInformationsIndex = 0; (mountsInformationsIndex < mountsInformationsCount); mountsInformationsIndex = (mountsInformationsIndex + 1))
            {
                MountInformationsForPaddock objectToAdd = new MountInformationsForPaddock();
                objectToAdd.Deserialize(reader);
                m_mountsInformations.Add(objectToAdd);
            }
            m_paddockId = reader.ReadInt();
            m_worldX = reader.ReadShort();
            m_worldY = reader.ReadShort();
            m_mapId = reader.ReadInt();
            m_subAreaId = reader.ReadVarUhShort();
            m_abandonned = reader.ReadBoolean();
        }
    }
}
