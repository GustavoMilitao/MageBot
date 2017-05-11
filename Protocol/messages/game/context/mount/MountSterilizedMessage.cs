//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Context.Mount
{
    using BlueSheep.Protocol;


    public class MountSterilizedMessage : Message
    {
        
        public const int ProtocolId = 5977;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private int m_mountId;
        
        public virtual int MountId
        {
            get
            {
                return m_mountId;
            }
            set
            {
                m_mountId = value;
            }
        }
        
        public MountSterilizedMessage(int mountId)
        {
            m_mountId = mountId;
        }
        
        public MountSterilizedMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteVarInt(m_mountId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_mountId = reader.ReadVarInt();
        }
    }
}
