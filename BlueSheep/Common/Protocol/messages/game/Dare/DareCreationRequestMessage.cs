//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Dare
{
    using BlueSheep.Common.Protocol.Types.Game.Dare;
    using System.Collections.Generic;
    using BlueSheep.Common;


    public class DareCreationRequestMessage : Message
    {
        
        public const int ProtocolId = 6665;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private bool m_isPrivate;
        
        public virtual bool IsPrivate
        {
            get
            {
                return m_isPrivate;
            }
            set
            {
                m_isPrivate = value;
            }
        }
        
        private bool m_isForGuild;
        
        public virtual bool IsForGuild
        {
            get
            {
                return m_isForGuild;
            }
            set
            {
                m_isForGuild = value;
            }
        }
        
        private bool m_isForAlliance;
        
        public virtual bool IsForAlliance
        {
            get
            {
                return m_isForAlliance;
            }
            set
            {
                m_isForAlliance = value;
            }
        }
        
        private bool m_needNotifications;
        
        public virtual bool NeedNotifications
        {
            get
            {
                return m_needNotifications;
            }
            set
            {
                m_needNotifications = value;
            }
        }
        
        private List<DareCriteria> m_criterions;
        
        public virtual List<DareCriteria> Criterions
        {
            get
            {
                return m_criterions;
            }
            set
            {
                m_criterions = value;
            }
        }
        
        private ulong m_subscriptionFee;
        
        public virtual ulong SubscriptionFee
        {
            get
            {
                return m_subscriptionFee;
            }
            set
            {
                m_subscriptionFee = value;
            }
        }
        
        private ulong m_jackpot;
        
        public virtual ulong Jackpot
        {
            get
            {
                return m_jackpot;
            }
            set
            {
                m_jackpot = value;
            }
        }
        
        private ushort m_maxCountWinners;
        
        public virtual ushort MaxCountWinners
        {
            get
            {
                return m_maxCountWinners;
            }
            set
            {
                m_maxCountWinners = value;
            }
        }
        
        private uint m_delayBeforeStart;
        
        public virtual uint DelayBeforeStart
        {
            get
            {
                return m_delayBeforeStart;
            }
            set
            {
                m_delayBeforeStart = value;
            }
        }
        
        private uint m_duration;
        
        public virtual uint Duration
        {
            get
            {
                return m_duration;
            }
            set
            {
                m_duration = value;
            }
        }
        
        public DareCreationRequestMessage(bool isPrivate, bool isForGuild, bool isForAlliance, bool needNotifications, List<DareCriteria> criterions, ulong subscriptionFee, ulong jackpot, ushort maxCountWinners, uint delayBeforeStart, uint duration)
        {
            m_isPrivate = isPrivate;
            m_isForGuild = isForGuild;
            m_isForAlliance = isForAlliance;
            m_needNotifications = needNotifications;
            m_criterions = criterions;
            m_subscriptionFee = subscriptionFee;
            m_jackpot = jackpot;
            m_maxCountWinners = maxCountWinners;
            m_delayBeforeStart = delayBeforeStart;
            m_duration = duration;
        }
        
        public DareCreationRequestMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            byte flag = new byte();
            BooleanByteWrapper.SetFlag(0, flag, m_isPrivate);
            BooleanByteWrapper.SetFlag(1, flag, m_isForGuild);
            BooleanByteWrapper.SetFlag(2, flag, m_isForAlliance);
            BooleanByteWrapper.SetFlag(3, flag, m_needNotifications);
            writer.WriteByte(flag);
            writer.WriteShort(((short)(m_criterions.Count)));
            int criterionsIndex;
            for (criterionsIndex = 0; (criterionsIndex < m_criterions.Count); criterionsIndex = (criterionsIndex + 1))
            {
                DareCriteria objectToSend = m_criterions[criterionsIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteVarLong(m_subscriptionFee);
            writer.WriteVarLong(m_jackpot);
            writer.WriteUShort(m_maxCountWinners);
            writer.WriteUInt(m_delayBeforeStart);
            writer.WriteUInt(m_duration);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            byte flag = reader.ReadByte();
            m_isPrivate = BooleanByteWrapper.GetFlag(flag, 0);
            m_isForGuild = BooleanByteWrapper.GetFlag(flag, 1);
            m_isForAlliance = BooleanByteWrapper.GetFlag(flag, 2);
            m_needNotifications = BooleanByteWrapper.GetFlag(flag, 3);
            int criterionsCount = reader.ReadUShort();
            int criterionsIndex;
            m_criterions = new System.Collections.Generic.List<DareCriteria>();
            for (criterionsIndex = 0; (criterionsIndex < criterionsCount); criterionsIndex = (criterionsIndex + 1))
            {
                DareCriteria objectToAdd = new DareCriteria();
                objectToAdd.Deserialize(reader);
                m_criterions.Add(objectToAdd);
            }
            m_subscriptionFee = reader.ReadVarUhLong();
            m_jackpot = reader.ReadVarUhLong();
            m_maxCountWinners = reader.ReadUShort();
            m_delayBeforeStart = reader.ReadUInt();
            m_duration = reader.ReadUInt();
        }
    }
}
