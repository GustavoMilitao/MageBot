//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MageBot.Protocol.Messages.Game.Context.Roleplay.Houses
{
    using MageBot.Protocol.Types.Game.House;
    using System.Collections.Generic;


    public class HouseToSellListMessage : Message
    {
        
        public override int ProtocolId { get; } = 6140;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private List<HouseInformationsForSell> m_houseList;
        
        public virtual List<HouseInformationsForSell> HouseList
        {
            get
            {
                return m_houseList;
            }
            set
            {
                m_houseList = value;
            }
        }
        
        private ushort m_pageIndex;
        
        public virtual ushort PageIndex
        {
            get
            {
                return m_pageIndex;
            }
            set
            {
                m_pageIndex = value;
            }
        }
        
        private ushort m_totalPage;
        
        public virtual ushort TotalPage
        {
            get
            {
                return m_totalPage;
            }
            set
            {
                m_totalPage = value;
            }
        }
        
        public HouseToSellListMessage(List<HouseInformationsForSell> houseList, ushort pageIndex, ushort totalPage)
        {
            m_houseList = houseList;
            m_pageIndex = pageIndex;
            m_totalPage = totalPage;
        }
        
        public HouseToSellListMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(((short)(m_houseList.Count)));
            int houseListIndex;
            for (houseListIndex = 0; (houseListIndex < m_houseList.Count); houseListIndex = (houseListIndex + 1))
            {
                HouseInformationsForSell objectToSend = m_houseList[houseListIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteVarShort(m_pageIndex);
            writer.WriteVarShort(m_totalPage);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            int houseListCount = reader.ReadUShort();
            int houseListIndex;
            m_houseList = new System.Collections.Generic.List<HouseInformationsForSell>();
            for (houseListIndex = 0; (houseListIndex < houseListCount); houseListIndex = (houseListIndex + 1))
            {
                HouseInformationsForSell objectToAdd = new HouseInformationsForSell();
                objectToAdd.Deserialize(reader);
                m_houseList.Add(objectToAdd);
            }
            m_pageIndex = reader.ReadVarUhShort();
            m_totalPage = reader.ReadVarUhShort();
        }
    }
}
