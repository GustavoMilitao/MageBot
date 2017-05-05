









// Generated on 12/11/2014 19:01:40
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class QuestStepInfoMessage : Message
    {
        public new const uint ID =5625;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.QuestActiveInformations infos;
        
        public QuestStepInfoMessage()
        {
        }
        
        public QuestStepInfoMessage(Types.QuestActiveInformations infos)
        {
            this.infos = infos;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteShort(infos.TypeId);
            infos.Serialize(writer);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            infos = Types.ProtocolTypeManager.GetInstance<Types.QuestActiveInformations>(reader.ReadShort());
            infos.Deserialize(reader);
        }
        
    }
    
}