//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Guild
{
    using BlueSheep.Common;


    public class GuildPaddockTeleportRequestMessage : Message
    {
        
        public const int ProtocolId = 5957;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
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
        
        public GuildPaddockTeleportRequestMessage(int paddockId)
        {
            m_paddockId = paddockId;
        }
        
        public GuildPaddockTeleportRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(m_paddockId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_paddockId = reader.ReadInt();
        }
    }
}
