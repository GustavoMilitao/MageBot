//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Inventory.Items
{
    using BlueSheep.Protocol.Types.Game.Data.Items;
    using BlueSheep.Protocol;


    public class MimicryObjectPreviewMessage : Message
    {
        
        public const int ProtocolId = 6458;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private ObjectItem m_result;
        
        public virtual ObjectItem Result
        {
            get
            {
                return m_result;
            }
            set
            {
                m_result = value;
            }
        }
        
        public MimicryObjectPreviewMessage(ObjectItem result)
        {
            m_result = result;
        }
        
        public MimicryObjectPreviewMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            m_result.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            m_result = new ObjectItem();
            m_result.Deserialize(reader);
        }
    }
}
