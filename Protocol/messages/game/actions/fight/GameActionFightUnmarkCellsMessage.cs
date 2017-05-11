//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Actions.Fight
{
    using BlueSheep.Protocol.Messages.Game.Actions;


    public class GameActionFightUnmarkCellsMessage : AbstractGameActionMessage
    {
        
        public const int ProtocolId = 5570;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private short m_markId;
        
        public virtual short MarkId
        {
            get
            {
                return m_markId;
            }
            set
            {
                m_markId = value;
            }
        }
        
        public GameActionFightUnmarkCellsMessage(short markId)
        {
            m_markId = markId;
        }
        
        public GameActionFightUnmarkCellsMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(m_markId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_markId = reader.ReadShort();
        }
    }
}
