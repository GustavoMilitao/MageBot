//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlueSheep.Protocol.Types.Game.Context.Roleplay
{
    using BlueSheep.Protocol.Types.Game.Context;


    public class GameRolePlayActorInformations : GameContextActorInformations
    {
        
        public const int ProtocolId = 141;
        
        public override int TypeID
        {
            get
            {
                return ProtocolId;
            }
        }
        
        public GameRolePlayActorInformations()
        {
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
        }
    }
}
