//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Context.Roleplay.Objects
{
    using BlueSheep.Common;


    public class ObjectGroundRemovedMessage : Message
    {
        
        public const int ProtocolId = 3014;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ushort m_cell;
        
        public virtual ushort Cell
        {
            get
            {
                return m_cell;
            }
            set
            {
                m_cell = value;
            }
        }
        
        public ObjectGroundRemovedMessage(ushort cell)
        {
            m_cell = cell;
        }
        
        public ObjectGroundRemovedMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarShort(m_cell);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_cell = reader.ReadVarUhShort();
        }
    }
}
