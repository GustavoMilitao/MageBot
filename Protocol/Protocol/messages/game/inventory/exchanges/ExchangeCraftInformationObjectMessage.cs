









// Generated on 12/11/2014 19:01:48
using System;
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class ExchangeCraftInformationObjectMessage : ExchangeCraftResultWithObjectIdMessage
    {
        public new const uint ID =5794;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int playerId;
        
        public ExchangeCraftInformationObjectMessage()
        {
        }
        
        public ExchangeCraftInformationObjectMessage(sbyte craftResult, short objectGenericId, int playerId)
         : base(craftResult, objectGenericId)
        {
            this.playerId = playerId;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            writer.WriteVarInt(playerId);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            playerId = reader.ReadVarInt();
            if (playerId < 0)
                throw new Exception("Forbidden value on playerId = " + playerId + ", it doesn't respect the following condition : playerId < 0");
        }
        
    }
    
}