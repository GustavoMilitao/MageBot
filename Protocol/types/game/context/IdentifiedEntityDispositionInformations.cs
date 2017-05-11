//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Types.Game.Context
{


    public class IdentifiedEntityDispositionInformations : EntityDispositionInformations
    {
        
        public const int ProtocolId = 107;
        
        public override int TypeID
        {
            get
            {
                return ProtocolId;
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
        
        public IdentifiedEntityDispositionInformations(double objectId)
        {
            m_ObjectId = objectId;
        }
        
        public IdentifiedEntityDispositionInformations()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteDouble(m_ObjectId);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            m_ObjectId = reader.ReadDouble();
        }
    }
}