//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Context.Fight
{
    using BlueSheep.Common;


    public class GameFightNewWaveMessage : Message
    {
        
        public const int ProtocolId = 6490;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private byte m_ObjectId;
        
        public virtual byte ObjectId
        {
            get
            {
                return m_ObjectId;
            }
            set
            {
                m_ObjectId = value;
            }
        }
        
        private byte m_teamId;
        
        public virtual byte TeamId
        {
            get
            {
                return m_teamId;
            }
            set
            {
                m_teamId = value;
            }
        }
        
        private short m_nbTurnBeforeNextWave;
        
        public virtual short NbTurnBeforeNextWave
        {
            get
            {
                return m_nbTurnBeforeNextWave;
            }
            set
            {
                m_nbTurnBeforeNextWave = value;
            }
        }
        
        public GameFightNewWaveMessage(byte objectId, byte teamId, short nbTurnBeforeNextWave)
        {
            m_ObjectId = objectId;
            m_teamId = teamId;
            m_nbTurnBeforeNextWave = nbTurnBeforeNextWave;
        }
        
        public GameFightNewWaveMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteByte(m_ObjectId);
            writer.WriteByte(m_teamId);
            writer.WriteShort(m_nbTurnBeforeNextWave);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_ObjectId = reader.ReadByte();
            m_teamId = reader.ReadByte();
            m_nbTurnBeforeNextWave = reader.ReadShort();
        }
    }
}
