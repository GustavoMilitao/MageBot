//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Inventory.Exchanges
{
    using BlueSheep.Common.Protocol.Types.Game.Mount;
    using System.Collections.Generic;
    using BlueSheep.Common;


    public class ExchangeStartOkMountWithOutPaddockMessage : Message
    {
        
        public const int ProtocolId = 5991;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private List<MountClientData> m_stabledMountsDescription;
        
        public virtual List<MountClientData> StabledMountsDescription
        {
            get
            {
                return m_stabledMountsDescription;
            }
            set
            {
                m_stabledMountsDescription = value;
            }
        }
        
        public ExchangeStartOkMountWithOutPaddockMessage(List<MountClientData> stabledMountsDescription)
        {
            m_stabledMountsDescription = stabledMountsDescription;
        }
        
        public ExchangeStartOkMountWithOutPaddockMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(((short)(m_stabledMountsDescription.Count)));
            int stabledMountsDescriptionIndex;
            for (stabledMountsDescriptionIndex = 0; (stabledMountsDescriptionIndex < m_stabledMountsDescription.Count); stabledMountsDescriptionIndex = (stabledMountsDescriptionIndex + 1))
            {
                MountClientData objectToSend = m_stabledMountsDescription[stabledMountsDescriptionIndex];
                objectToSend.Serialize(writer);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            int stabledMountsDescriptionCount = reader.ReadUShort();
            int stabledMountsDescriptionIndex;
            m_stabledMountsDescription = new System.Collections.Generic.List<MountClientData>();
            for (stabledMountsDescriptionIndex = 0; (stabledMountsDescriptionIndex < stabledMountsDescriptionCount); stabledMountsDescriptionIndex = (stabledMountsDescriptionIndex + 1))
            {
                MountClientData objectToAdd = new MountClientData();
                objectToAdd.Deserialize(reader);
                m_stabledMountsDescription.Add(objectToAdd);
            }
        }
    }
}
