//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Types.Game.Context.Fight
{


    public class GameFightMutantInformations : GameFightFighterNamedInformations
    {
        
        public const int ProtocolId = 50;
        
        public override int TypeID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private byte m_powerLevel;
        
        public virtual byte PowerLevel
        {
            get
            {
                return m_powerLevel;
            }
            set
            {
                m_powerLevel = value;
            }
        }
        
        public GameFightMutantInformations(byte powerLevel)
        {
            m_powerLevel = powerLevel;
        }
        
        public GameFightMutantInformations()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteByte(m_powerLevel);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_powerLevel = reader.ReadByte();
        }
    }
}