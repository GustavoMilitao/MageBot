//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Types.Game.Mount
{
    using BlueSheep.Protocol.Types;


    public class UpdateMountBoost : NetworkType
    {
        
        public const int ProtocolId = 356;
        
        public override int TypeID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private byte m_type;
        
        public virtual byte Type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }
        
        public UpdateMountBoost(byte type)
        {
            m_type = type;
        }
        
        public UpdateMountBoost()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(m_type);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_type = reader.ReadByte();
        }
    }
}
