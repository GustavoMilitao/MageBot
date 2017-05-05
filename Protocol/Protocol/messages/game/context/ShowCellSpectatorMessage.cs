









// Generated on 12/11/2014 19:01:27
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ShowCellSpectatorMessage : ShowCellMessage
    {
        public new const uint ID =6158;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public string playerName;
        
        public ShowCellSpectatorMessage()
        {
        }
        
        public ShowCellSpectatorMessage(int sourceId, short cellId, string playerName)
         : base(sourceId, cellId)
        {
            this.playerName = playerName;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteUTF(playerName);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            playerName = reader.ReadUTF();
        }
        
    }
    
}