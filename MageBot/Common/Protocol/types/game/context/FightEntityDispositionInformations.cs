//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Types.Game.Context
{


    public class FightEntityDispositionInformations : EntityDispositionInformations
    {
        
        public const int ProtocolId = 217;
        
        public override int TypeID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private double m_carryingCharacterId;
        
        public virtual double CarryingCharacterId
        {
            get
            {
                return m_carryingCharacterId;
            }
            set
            {
                m_carryingCharacterId = value;
            }
        }
        
        public FightEntityDispositionInformations(double carryingCharacterId)
        {
            m_carryingCharacterId = carryingCharacterId;
        }
        
        public FightEntityDispositionInformations()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(m_carryingCharacterId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_carryingCharacterId = reader.ReadDouble();
        }
    }
}
