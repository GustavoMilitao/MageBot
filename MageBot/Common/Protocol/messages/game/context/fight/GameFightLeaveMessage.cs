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


    public class GameFightLeaveMessage : Message
    {
        
        public const int ProtocolId = 721;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private double m_charId;
        
        public virtual double CharId
        {
            get
            {
                return m_charId;
            }
            set
            {
                m_charId = value;
            }
        }
        
        public GameFightLeaveMessage(double charId)
        {
            m_charId = charId;
        }
        
        public GameFightLeaveMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteDouble(m_charId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_charId = reader.ReadDouble();
        }
    }
}
