









// Generated on 12/11/2014 19:01:33
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class AccountHouseMessage : Message
    {
        public new const uint ID =6315;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.AccountHouseInformations[] houses;
        
        public AccountHouseMessage()
        {
        }
        
        public AccountHouseMessage(Types.AccountHouseInformations[] houses)
        {
            this.houses = houses;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUShort((ushort)houses.Length);
            foreach (var entry in houses)
            {
                 entry.Serialize(writer);
            }
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            var limit = reader.ReadUShort();
            houses = new Types.AccountHouseInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 houses[i] = new Types.AccountHouseInformations();
                 houses[i].Deserialize(reader);
            }
        }
        
    }
    
}