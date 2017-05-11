//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Types.Game.Mount
{
    using BlueSheep.Protocol.Types.Game.Data.Items.Effects;
    using System.Collections.Generic;
    using BlueSheep.Protocol.Types;


    public class MountClientData : NetworkType
    {
        
        public const int ProtocolId = 178;
        
        public override int TypeID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private bool m_sex;
        
        public virtual bool Sex
        {
            get
            {
                return m_sex;
            }
            set
            {
                m_sex = value;
            }
        }
        
        private bool m_isRideable;
        
        public virtual bool IsRideable
        {
            get
            {
                return m_isRideable;
            }
            set
            {
                m_isRideable = value;
            }
        }
        
        private bool m_isWild;
        
        public virtual bool IsWild
        {
            get
            {
                return m_isWild;
            }
            set
            {
                m_isWild = value;
            }
        }
        
        private bool m_isFecondationReady;
        
        public virtual bool IsFecondationReady
        {
            get
            {
                return m_isFecondationReady;
            }
            set
            {
                m_isFecondationReady = value;
            }
        }
        
        private bool m_useHarnessColors;
        
        public virtual bool UseHarnessColors
        {
            get
            {
                return m_useHarnessColors;
            }
            set
            {
                m_useHarnessColors = value;
            }
        }
        
        private List<System.Int32> m_ancestor;
        
        public virtual List<System.Int32> Ancestor
        {
            get
            {
                return m_ancestor;
            }
            set
            {
                m_ancestor = value;
            }
        }
        
        private List<System.Int32> m_behaviors;
        
        public virtual List<System.Int32> Behaviors
        {
            get
            {
                return m_behaviors;
            }
            set
            {
                m_behaviors = value;
            }
        }
        
        private List<ObjectEffectInteger> m_effectList;
        
        public virtual List<ObjectEffectInteger> EffectList
        {
            get
            {
                return m_effectList;
            }
            set
            {
                m_effectList = value;
            }
        }
        
        private double m_ObjectId;
        
        public virtual double ObjectId
        {
            get
            {
                return m_ObjectId;
            }
            set
            {
                m_ObjectId = value;
            }
        }
        
        private uint m_model;
        
        public virtual uint Model
        {
            get
            {
                return m_model;
            }
            set
            {
                m_model = value;
            }
        }
        
        private string m_name;
        
        public virtual string Name
        {
            get
            {
                return m_name;
            }
            set
            {
                m_name = value;
            }
        }
        
        private int m_ownerId;
        
        public virtual int OwnerId
        {
            get
            {
                return m_ownerId;
            }
            set
            {
                m_ownerId = value;
            }
        }
        
        private ulong m_experience;
        
        public virtual ulong Experience
        {
            get
            {
                return m_experience;
            }
            set
            {
                m_experience = value;
            }
        }
        
        private ulong m_experienceForLevel;
        
        public virtual ulong ExperienceForLevel
        {
            get
            {
                return m_experienceForLevel;
            }
            set
            {
                m_experienceForLevel = value;
            }
        }
        
        private double m_experienceForNextLevel;
        
        public virtual double ExperienceForNextLevel
        {
            get
            {
                return m_experienceForNextLevel;
            }
            set
            {
                m_experienceForNextLevel = value;
            }
        }
        
        private byte m_level;
        
        public virtual byte Level
        {
            get
            {
                return m_level;
            }
            set
            {
                m_level = value;
            }
        }
        
        private uint m_maxPods;
        
        public virtual uint MaxPods
        {
            get
            {
                return m_maxPods;
            }
            set
            {
                m_maxPods = value;
            }
        }
        
        private uint m_stamina;
        
        public virtual uint Stamina
        {
            get
            {
                return m_stamina;
            }
            set
            {
                m_stamina = value;
            }
        }
        
        private uint m_staminaMax;
        
        public virtual uint StaminaMax
        {
            get
            {
                return m_staminaMax;
            }
            set
            {
                m_staminaMax = value;
            }
        }
        
        private uint m_maturity;
        
        public virtual uint Maturity
        {
            get
            {
                return m_maturity;
            }
            set
            {
                m_maturity = value;
            }
        }
        
        private uint m_maturityForAdult;
        
        public virtual uint MaturityForAdult
        {
            get
            {
                return m_maturityForAdult;
            }
            set
            {
                m_maturityForAdult = value;
            }
        }
        
        private uint m_energy;
        
        public virtual uint Energy
        {
            get
            {
                return m_energy;
            }
            set
            {
                m_energy = value;
            }
        }
        
        private uint m_energyMax;
        
        public virtual uint EnergyMax
        {
            get
            {
                return m_energyMax;
            }
            set
            {
                m_energyMax = value;
            }
        }
        
        private int m_serenity;
        
        public virtual int Serenity
        {
            get
            {
                return m_serenity;
            }
            set
            {
                m_serenity = value;
            }
        }
        
        private int m_aggressivityMax;
        
        public virtual int AggressivityMax
        {
            get
            {
                return m_aggressivityMax;
            }
            set
            {
                m_aggressivityMax = value;
            }
        }
        
        private uint m_serenityMax;
        
        public virtual uint SerenityMax
        {
            get
            {
                return m_serenityMax;
            }
            set
            {
                m_serenityMax = value;
            }
        }
        
        private uint m_love;
        
        public virtual uint Love
        {
            get
            {
                return m_love;
            }
            set
            {
                m_love = value;
            }
        }
        
        private uint m_loveMax;
        
        public virtual uint LoveMax
        {
            get
            {
                return m_loveMax;
            }
            set
            {
                m_loveMax = value;
            }
        }
        
        private int m_fecondationTime;
        
        public virtual int FecondationTime
        {
            get
            {
                return m_fecondationTime;
            }
            set
            {
                m_fecondationTime = value;
            }
        }
        
        private int m_boostLimiter;
        
        public virtual int BoostLimiter
        {
            get
            {
                return m_boostLimiter;
            }
            set
            {
                m_boostLimiter = value;
            }
        }
        
        private double m_boostMax;
        
        public virtual double BoostMax
        {
            get
            {
                return m_boostMax;
            }
            set
            {
                m_boostMax = value;
            }
        }
        
        private int m_reproductionCount;
        
        public virtual int ReproductionCount
        {
            get
            {
                return m_reproductionCount;
            }
            set
            {
                m_reproductionCount = value;
            }
        }
        
        private uint m_reproductionCountMax;
        
        public virtual uint ReproductionCountMax
        {
            get
            {
                return m_reproductionCountMax;
            }
            set
            {
                m_reproductionCountMax = value;
            }
        }
        
        private ushort m_harnessGID;
        
        public virtual ushort HarnessGID
        {
            get
            {
                return m_harnessGID;
            }
            set
            {
                m_harnessGID = value;
            }
        }
        
        public MountClientData(
                    bool sex, 
                    bool isRideable, 
                    bool isWild, 
                    bool isFecondationReady, 
                    bool useHarnessColors, 
                    List<System.Int32> ancestor, 
                    List<System.Int32> behaviors, 
                    List<ObjectEffectInteger> effectList, 
                    double objectId, 
                    uint model, 
                    string name, 
                    int ownerId, 
                    ulong experience, 
                    ulong experienceForLevel, 
                    double experienceForNextLevel, 
                    byte level, 
                    uint maxPods, 
                    uint stamina, 
                    uint staminaMax, 
                    uint maturity, 
                    uint maturityForAdult, 
                    uint energy, 
                    uint energyMax, 
                    int serenity, 
                    int aggressivityMax, 
                    uint serenityMax, 
                    uint love, 
                    uint loveMax, 
                    int fecondationTime, 
                    int boostLimiter, 
                    double boostMax, 
                    int reproductionCount, 
                    uint reproductionCountMax, 
                    ushort harnessGID)
        {
            m_sex = sex;
            m_isRideable = isRideable;
            m_isWild = isWild;
            m_isFecondationReady = isFecondationReady;
            m_useHarnessColors = useHarnessColors;
            m_ancestor = ancestor;
            m_behaviors = behaviors;
            m_effectList = effectList;
            m_ObjectId = objectId;
            m_model = model;
            m_name = name;
            m_ownerId = ownerId;
            m_experience = experience;
            m_experienceForLevel = experienceForLevel;
            m_experienceForNextLevel = experienceForNextLevel;
            m_level = level;
            m_maxPods = maxPods;
            m_stamina = stamina;
            m_staminaMax = staminaMax;
            m_maturity = maturity;
            m_maturityForAdult = maturityForAdult;
            m_energy = energy;
            m_energyMax = energyMax;
            m_serenity = serenity;
            m_aggressivityMax = aggressivityMax;
            m_serenityMax = serenityMax;
            m_love = love;
            m_loveMax = loveMax;
            m_fecondationTime = fecondationTime;
            m_boostLimiter = boostLimiter;
            m_boostMax = boostMax;
            m_reproductionCount = reproductionCount;
            m_reproductionCountMax = reproductionCountMax;
            m_harnessGID = harnessGID;
        }
        
        public MountClientData()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            byte flag = new byte();
            BooleanByteWrapper.SetFlag(0, flag, m_sex);
            BooleanByteWrapper.SetFlag(1, flag, m_isRideable);
            BooleanByteWrapper.SetFlag(2, flag, m_isWild);
            BooleanByteWrapper.SetFlag(3, flag, m_isFecondationReady);
            BooleanByteWrapper.SetFlag(4, flag, m_useHarnessColors);
            writer.WriteByte(flag);
            writer.WriteShort(((short)(m_ancestor.Count)));
            int ancestorIndex;
            for (ancestorIndex = 0; (ancestorIndex < m_ancestor.Count); ancestorIndex = (ancestorIndex + 1))
            {
                writer.WriteInt(m_ancestor[ancestorIndex]);
            }
            writer.WriteShort(((short)(m_behaviors.Count)));
            int behaviorsIndex;
            for (behaviorsIndex = 0; (behaviorsIndex < m_behaviors.Count); behaviorsIndex = (behaviorsIndex + 1))
            {
                writer.WriteInt(m_behaviors[behaviorsIndex]);
            }
            writer.WriteShort(((short)(m_effectList.Count)));
            int effectListIndex;
            for (effectListIndex = 0; (effectListIndex < m_effectList.Count); effectListIndex = (effectListIndex + 1))
            {
                ObjectEffectInteger objectToSend = m_effectList[effectListIndex];
                objectToSend.Serialize(writer);
            }
            writer.WriteDouble(m_ObjectId);
            writer.WriteVarInt(m_model);
            writer.WriteUTF(m_name);
            writer.WriteInt(m_ownerId);
            writer.WriteVarLong(m_experience);
            writer.WriteVarLong(m_experienceForLevel);
            writer.WriteDouble(m_experienceForNextLevel);
            writer.WriteByte(m_level);
            writer.WriteVarInt(m_maxPods);
            writer.WriteVarInt(m_stamina);
            writer.WriteVarInt(m_staminaMax);
            writer.WriteVarInt(m_maturity);
            writer.WriteVarInt(m_maturityForAdult);
            writer.WriteVarInt(m_energy);
            writer.WriteVarInt(m_energyMax);
            writer.WriteInt(m_serenity);
            writer.WriteInt(m_aggressivityMax);
            writer.WriteVarInt(m_serenityMax);
            writer.WriteVarInt(m_love);
            writer.WriteVarInt(m_loveMax);
            writer.WriteInt(m_fecondationTime);
            writer.WriteInt(m_boostLimiter);
            writer.WriteDouble(m_boostMax);
            writer.WriteInt(m_reproductionCount);
            writer.WriteVarInt(m_reproductionCountMax);
            writer.WriteVarShort(m_harnessGID);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            byte flag = reader.ReadByte();
            m_sex = BooleanByteWrapper.GetFlag(flag, 0);
            m_isRideable = BooleanByteWrapper.GetFlag(flag, 1);
            m_isWild = BooleanByteWrapper.GetFlag(flag, 2);
            m_isFecondationReady = BooleanByteWrapper.GetFlag(flag, 3);
            m_useHarnessColors = BooleanByteWrapper.GetFlag(flag, 4);
            int ancestorCount = reader.ReadUShort();
            int ancestorIndex;
            m_ancestor = new System.Collections.Generic.List<int>();
            for (ancestorIndex = 0; (ancestorIndex < ancestorCount); ancestorIndex = (ancestorIndex + 1))
            {
                m_ancestor.Add(reader.ReadInt());
            }
            int behaviorsCount = reader.ReadUShort();
            int behaviorsIndex;
            m_behaviors = new System.Collections.Generic.List<int>();
            for (behaviorsIndex = 0; (behaviorsIndex < behaviorsCount); behaviorsIndex = (behaviorsIndex + 1))
            {
                m_behaviors.Add(reader.ReadInt());
            }
            int effectListCount = reader.ReadUShort();
            int effectListIndex;
            m_effectList = new System.Collections.Generic.List<ObjectEffectInteger>();
            for (effectListIndex = 0; (effectListIndex < effectListCount); effectListIndex = (effectListIndex + 1))
            {
                ObjectEffectInteger objectToAdd = new ObjectEffectInteger();
                objectToAdd.Deserialize(reader);
                m_effectList.Add(objectToAdd);
            }
            m_ObjectId = reader.ReadDouble();
            m_model = reader.ReadVarUhInt();
            m_name = reader.ReadUTF();
            m_ownerId = reader.ReadInt();
            m_experience = reader.ReadVarUhLong();
            m_experienceForLevel = reader.ReadVarUhLong();
            m_experienceForNextLevel = reader.ReadDouble();
            m_level = reader.ReadByte();
            m_maxPods = reader.ReadVarUhInt();
            m_stamina = reader.ReadVarUhInt();
            m_staminaMax = reader.ReadVarUhInt();
            m_maturity = reader.ReadVarUhInt();
            m_maturityForAdult = reader.ReadVarUhInt();
            m_energy = reader.ReadVarUhInt();
            m_energyMax = reader.ReadVarUhInt();
            m_serenity = reader.ReadInt();
            m_aggressivityMax = reader.ReadInt();
            m_serenityMax = reader.ReadVarUhInt();
            m_love = reader.ReadVarUhInt();
            m_loveMax = reader.ReadVarUhInt();
            m_fecondationTime = reader.ReadInt();
            m_boostLimiter = reader.ReadInt();
            m_boostMax = reader.ReadDouble();
            m_reproductionCount = reader.ReadInt();
            m_reproductionCountMax = reader.ReadVarUhInt();
            m_harnessGID = reader.ReadVarUhShort();
        }
    }
}
