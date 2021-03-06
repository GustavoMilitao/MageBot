//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MageBot.Protocol.Messages.Game.Inventory.Exchanges
{
    using MageBot.Protocol.Types.Game.Mount;
    using System.Collections.Generic;


    public class ExchangeStartOkMountMessage : ExchangeStartOkMountWithOutPaddockMessage
    {
        
        public override int ProtocolId { get; } = 5979;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private List<MountClientData> m_paddockedMountsDescription;
        
        public virtual List<MountClientData> PaddockedMountsDescription
        {
            get
            {
                return m_paddockedMountsDescription;
            }
            set
            {
                m_paddockedMountsDescription = value;
            }
        }
        
        public ExchangeStartOkMountMessage(List<MountClientData> paddockedMountsDescription)
        {
            m_paddockedMountsDescription = paddockedMountsDescription;
        }
        
        public ExchangeStartOkMountMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(((short)(m_paddockedMountsDescription.Count)));
            int paddockedMountsDescriptionIndex;
            for (paddockedMountsDescriptionIndex = 0; (paddockedMountsDescriptionIndex < m_paddockedMountsDescription.Count); paddockedMountsDescriptionIndex = (paddockedMountsDescriptionIndex + 1))
            {
                MountClientData objectToSend = m_paddockedMountsDescription[paddockedMountsDescriptionIndex];
                objectToSend.Serialize(writer);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            int paddockedMountsDescriptionCount = reader.ReadUShort();
            int paddockedMountsDescriptionIndex;
            m_paddockedMountsDescription = new System.Collections.Generic.List<MountClientData>();
            for (paddockedMountsDescriptionIndex = 0; (paddockedMountsDescriptionIndex < paddockedMountsDescriptionCount); paddockedMountsDescriptionIndex = (paddockedMountsDescriptionIndex + 1))
            {
                MountClientData objectToAdd = new MountClientData();
                objectToAdd.Deserialize(reader);
                m_paddockedMountsDescription.Add(objectToAdd);
            }
        }
    }
}
