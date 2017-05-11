//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Types.Game.Context
{
    using BlueSheep.Protocol.Types;


    public class EntityDispositionInformations : NetworkType
    {
        
        public const int ProtocolId = 60;
        
        public override int TypeID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private short m_cellId;
        
        public virtual short CellId
        {
            get
            {
                return m_cellId;
            }
            set
            {
                m_cellId = value;
            }
        }
        
        private byte m_direction;
        
        public virtual byte Direction
        {
            get
            {
                return m_direction;
            }
            set
            {
                m_direction = value;
            }
        }
        
        public EntityDispositionInformations(short cellId, byte direction)
        {
            m_cellId = cellId;
            m_direction = direction;
        }
        
        public EntityDispositionInformations()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(m_cellId);
            writer.WriteByte(m_direction);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_cellId = reader.ReadShort();
            m_direction = reader.ReadByte();
        }
    }
}
