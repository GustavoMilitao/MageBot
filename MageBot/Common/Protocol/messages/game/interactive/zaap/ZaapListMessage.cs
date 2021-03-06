//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Interactive.Zaap
{


    public class ZaapListMessage : TeleportDestinationsListMessage
    {
        
        public const int ProtocolId = 1604;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private int m_spawnMapId;
        
        public virtual int SpawnMapId
        {
            get
            {
                return m_spawnMapId;
            }
            set
            {
                m_spawnMapId = value;
            }
        }
        
        public ZaapListMessage(int spawnMapId)
        {
            m_spawnMapId = spawnMapId;
        }
        
        public ZaapListMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(m_spawnMapId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_spawnMapId = reader.ReadInt();
        }
    }
}
