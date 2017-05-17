//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Common.Protocol.Messages.Game.Actions.Fight
{


    public class GameActionFightDispellEffectMessage : GameActionFightDispellMessage
    {
        
        public const int ProtocolId = 6113;
        
        public override int MessageID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        private int m_boostUID;
        
        public virtual int BoostUID
        {
            get
            {
                return m_boostUID;
            }
            set
            {
                m_boostUID = value;
            }
        }
        
        public GameActionFightDispellEffectMessage(int boostUID)
        {
            m_boostUID = boostUID;
        }
        
        public GameActionFightDispellEffectMessage()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteInt(m_boostUID);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_boostUID = reader.ReadInt();
        }
    }
}