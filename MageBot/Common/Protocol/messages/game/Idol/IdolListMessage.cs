//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Idol
{
    using BlueSheep.Common.Protocol.Types.Game.Idol;
    using System.Collections.Generic;
    using BlueSheep.Common;
	using BlueSheep.Common.Protocol.Types;


    public class IdolListMessage : Message
    {
        
        public const int ProtocolId = 6585;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private List<System.UInt16> m_chosenIdols;
        
        public virtual List<System.UInt16> ChosenIdols
        {
            get
            {
                return m_chosenIdols;
            }
            set
            {
                m_chosenIdols = value;
            }
        }
        
        private List<System.UInt16> m_partyChosenIdols;
        
        public virtual List<System.UInt16> PartyChosenIdols
        {
            get
            {
                return m_partyChosenIdols;
            }
            set
            {
                m_partyChosenIdols = value;
            }
        }
        
        private List<PartyIdol> m_partyIdols;
        
        public virtual List<PartyIdol> PartyIdols
        {
            get
            {
                return m_partyIdols;
            }
            set
            {
                m_partyIdols = value;
            }
        }
        
        public IdolListMessage(List<System.UInt16> chosenIdols, List<System.UInt16> partyChosenIdols, List<PartyIdol> partyIdols)
        {
            m_chosenIdols = chosenIdols;
            m_partyChosenIdols = partyChosenIdols;
            m_partyIdols = partyIdols;
        }
        
        public IdolListMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(((short)(m_chosenIdols.Count)));
            int chosenIdolsIndex;
            for (chosenIdolsIndex = 0; (chosenIdolsIndex < m_chosenIdols.Count); chosenIdolsIndex = (chosenIdolsIndex + 1))
            {
                writer.WriteVarShort(m_chosenIdols[chosenIdolsIndex]);
            }
            writer.WriteShort(((short)(m_partyChosenIdols.Count)));
            int partyChosenIdolsIndex;
            for (partyChosenIdolsIndex = 0; (partyChosenIdolsIndex < m_partyChosenIdols.Count); partyChosenIdolsIndex = (partyChosenIdolsIndex + 1))
            {
                writer.WriteVarShort(m_partyChosenIdols[partyChosenIdolsIndex]);
            }
            writer.WriteShort(((short)(m_partyIdols.Count)));
            int partyIdolsIndex;
            for (partyIdolsIndex = 0; (partyIdolsIndex < m_partyIdols.Count); partyIdolsIndex = (partyIdolsIndex + 1))
            {
                PartyIdol objectToSend = m_partyIdols[partyIdolsIndex];
                writer.WriteUShort(((ushort)(objectToSend.TypeID)));
                objectToSend.Serialize(writer);
            }
        }
        
        public override void Deserialize(IDataReader reader)
        {
            int chosenIdolsCount = reader.ReadUShort();
            int chosenIdolsIndex;
            m_chosenIdols = new System.Collections.Generic.List<ushort>();
            for (chosenIdolsIndex = 0; (chosenIdolsIndex < chosenIdolsCount); chosenIdolsIndex = (chosenIdolsIndex + 1))
            {
                m_chosenIdols.Add(reader.ReadVarUhShort());
            }
            int partyChosenIdolsCount = reader.ReadUShort();
            int partyChosenIdolsIndex;
            m_partyChosenIdols = new System.Collections.Generic.List<ushort>();
            for (partyChosenIdolsIndex = 0; (partyChosenIdolsIndex < partyChosenIdolsCount); partyChosenIdolsIndex = (partyChosenIdolsIndex + 1))
            {
                m_partyChosenIdols.Add(reader.ReadVarUhShort());
            }
            int partyIdolsCount = reader.ReadUShort();
            int partyIdolsIndex;
            m_partyIdols = new System.Collections.Generic.List<PartyIdol>();
            for (partyIdolsIndex = 0; (partyIdolsIndex < partyIdolsCount); partyIdolsIndex = (partyIdolsIndex + 1))
            {
                PartyIdol objectToAdd = ProtocolTypeManager.GetInstance<PartyIdol>(reader.ReadUShort());
                objectToAdd.Deserialize(reader);
                m_partyIdols.Add(objectToAdd);
            }
        }
    }
}
