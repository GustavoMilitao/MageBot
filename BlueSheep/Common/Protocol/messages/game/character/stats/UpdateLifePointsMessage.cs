//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Character.Stats
{
    using BlueSheep.Common;


    public class UpdateLifePointsMessage : Message
    {
        
        public const int ProtocolId = 5658;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private uint m_lifePoints;
        
        public virtual uint LifePoints
        {
            get
            {
                return m_lifePoints;
            }
            set
            {
                m_lifePoints = value;
            }
        }
        
        private uint m_maxLifePoints;
        
        public virtual uint MaxLifePoints
        {
            get
            {
                return m_maxLifePoints;
            }
            set
            {
                m_maxLifePoints = value;
            }
        }
        
        public UpdateLifePointsMessage(uint lifePoints, uint maxLifePoints)
        {
            m_lifePoints = lifePoints;
            m_maxLifePoints = maxLifePoints;
        }
        
        public UpdateLifePointsMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(m_lifePoints);
            writer.WriteVarInt(m_maxLifePoints);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_lifePoints = reader.ReadVarUhInt();
            m_maxLifePoints = reader.ReadVarUhInt();
        }
    }
}
