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


    public class GameFightTurnStartMessage : Message
    {
        
        public const int ProtocolId = 714;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private double m_ObjectId;
        
        public virtual double ObjectId
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
        
        private uint m_waitTime;
        
        public virtual uint WaitTime
        {
            get
            {
                return m_waitTime;
            }
            set
            {
                m_waitTime = value;
            }
        }
        
        public GameFightTurnStartMessage(double objectId, uint waitTime)
        {
            m_ObjectId = objectId;
            m_waitTime = waitTime;
        }
        
        public GameFightTurnStartMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(m_ObjectId);
            writer.WriteVarInt(m_waitTime);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_ObjectId = reader.ReadDouble();
            m_waitTime = reader.ReadVarUhInt();
        }
    }
}
