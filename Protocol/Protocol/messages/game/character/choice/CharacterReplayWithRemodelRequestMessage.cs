









// Generated on 12/11/2014 19:01:22
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class CharacterReplayWithRemodelRequestMessage : CharacterReplayRequestMessage
    {
        public new const uint ID =6551;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.RemodelingInformation remodel;
        
        public CharacterReplayWithRemodelRequestMessage()
        {
        }
        
        public CharacterReplayWithRemodelRequestMessage(int characterId, Types.RemodelingInformation remodel)
         : base(characterId)
        {
            this.remodel = remodel;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            remodel.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            remodel = new Types.RemodelingInformation();
            remodel.Deserialize(reader);
        }
        
    }
    
}