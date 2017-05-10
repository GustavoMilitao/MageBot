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
    using BlueSheep.Common.Protocol.Types.Game.House;
    using System.Collections.Generic;
    using BlueSheep.Common;


    public class GuildHousesInformationMessage : Message
    {
        
        public const int ProtocolId = 5919;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private List<HouseInformationsForGuild> m_housesInformations;
        
        public virtual List<HouseInformationsForGuild> HousesInformations
        {
            get
            {
                return m_housesInformations;
            }
            set
            {
                m_housesInformations = value;
            }
        }
        
        public GuildHousesInformationMessage(List<HouseInformationsForGuild> housesInformations)
        {
            m_housesInformations = housesInformations;
        }
        
        public GuildHousesInformationMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(((short)(m_housesInformations.Count)));
            int housesInformationsIndex;
            for (housesInformationsIndex = 0; (housesInformationsIndex < m_housesInformations.Count); housesInformationsIndex = (housesInformationsIndex + 1))
            {
                HouseInformationsForGuild objectToSend = m_housesInformations[housesInformationsIndex];
                objectToSend.Serialize(writer);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            int housesInformationsCount = reader.ReadUShort();
            int housesInformationsIndex;
            m_housesInformations = new System.Collections.Generic.List<HouseInformationsForGuild>();
            for (housesInformationsIndex = 0; (housesInformationsIndex < housesInformationsCount); housesInformationsIndex = (housesInformationsIndex + 1))
            {
                HouseInformationsForGuild objectToAdd = new HouseInformationsForGuild();
                objectToAdd.Deserialize(reader);
                m_housesInformations.Add(objectToAdd);
            }
        }
    }
}
