//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Inventory.Preset
{
    using System.Collections.Generic;
    using BlueSheep.Common;


    public class InventoryPresetUseResultMessage : Message
    {
        
        public const int ProtocolId = 6163;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private List<System.SByte> m_unlinkedPosition;
        
        public virtual List<System.SByte> UnlinkedPosition
        {
            get
            {
                return m_unlinkedPosition;
            }
            set
            {
                m_unlinkedPosition = value;
            }
        }
        
        private byte m_presetId;
        
        public virtual byte PresetId
        {
            get
            {
                return m_presetId;
            }
            set
            {
                m_presetId = value;
            }
        }
        
        private byte m_code;
        
        public virtual byte Code
        {
            get
            {
                return m_code;
            }
            set
            {
                m_code = value;
            }
        }
        
        public InventoryPresetUseResultMessage(List<System.SByte> unlinkedPosition, byte presetId, byte code)
        {
            m_unlinkedPosition = unlinkedPosition;
            m_presetId = presetId;
            m_code = code;
        }
        
        public InventoryPresetUseResultMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(((short)(m_unlinkedPosition.Count)));
            int unlinkedPositionIndex;
            for (unlinkedPositionIndex = 0; (unlinkedPositionIndex < m_unlinkedPosition.Count); unlinkedPositionIndex = (unlinkedPositionIndex + 1))
            {
                writer.WriteSByte(m_unlinkedPosition[unlinkedPositionIndex]);
            }
            writer.WriteByte(m_presetId);
            writer.WriteByte(m_code);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            int unlinkedPositionCount = reader.ReadUShort();
            int unlinkedPositionIndex;
            m_unlinkedPosition = new System.Collections.Generic.List<sbyte>();
            for (unlinkedPositionIndex = 0; (unlinkedPositionIndex < unlinkedPositionCount); unlinkedPositionIndex = (unlinkedPositionIndex + 1))
            {
                m_unlinkedPosition.Add(reader.ReadSByte());
            }
            m_presetId = reader.ReadByte();
            m_code = reader.ReadByte();
        }
    }
}
