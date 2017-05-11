//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Messages.Game.Interactive
{
    using BlueSheep.Protocol.Types.Game.Interactive;
    using System.Collections.Generic;
    using BlueSheep.Protocol;


    public class StatedMapUpdateMessage : Message
    {
        
        public const int ProtocolId = 5716;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private List<StatedElement> m_statedElements;
        
        public virtual List<StatedElement> StatedElements
        {
            get
            {
                return m_statedElements;
            }
            set
            {
                m_statedElements = value;
            }
        }
        
        public StatedMapUpdateMessage(List<StatedElement> statedElements)
        {
            m_statedElements = statedElements;
        }
        
        public StatedMapUpdateMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(((short)(m_statedElements.Count)));
            int statedElementsIndex;
            for (statedElementsIndex = 0; (statedElementsIndex < m_statedElements.Count); statedElementsIndex = (statedElementsIndex + 1))
            {
                StatedElement objectToSend = m_statedElements[statedElementsIndex];
                objectToSend.Serialize(writer);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            int statedElementsCount = reader.ReadUShort();
            int statedElementsIndex;
            m_statedElements = new System.Collections.Generic.List<StatedElement>();
            for (statedElementsIndex = 0; (statedElementsIndex < statedElementsCount); statedElementsIndex = (statedElementsIndex + 1))
            {
                StatedElement objectToAdd = new StatedElement();
                objectToAdd.Deserialize(reader);
                m_statedElements.Add(objectToAdd);
            }
        }
    }
}
