//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Types.Game.Context.Fight
{
    using BlueSheep.Protocol.Types.Game.Character.Status;


    public class GameFightFighterNamedInformations : GameFightFighterInformations
    {
        
        public const int ProtocolId = 158;
        
        public override int TypeID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private PlayerStatus m_status;
        
        public virtual PlayerStatus Status
        {
            get
            {
                return m_status;
            }
            set
            {
                m_status = value;
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
        
        public GameFightFighterNamedInformations(PlayerStatus status, string name)
        {
            m_status = status;
            m_name = name;
        }
        
        public GameFightFighterNamedInformations()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            m_status.Serialize(writer);
            writer.WriteUTF(m_name);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_name = reader.ReadUTF();
            m_status = new PlayerStatus();
            m_status.Deserialize(reader);
        }
    }
}
