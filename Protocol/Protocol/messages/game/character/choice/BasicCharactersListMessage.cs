









// Generated on 12/11/2014 19:01:22
using BlueSheep.Common.IO;

namespace BlueSheep.Common.Protocol.Messages
{
    public class BasicCharactersListMessage : Message
    {
        public new const uint ID =6475;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public Types.CharacterBaseInformations[] characters;
        
        public BasicCharactersListMessage()
        {
        }
        
        public BasicCharactersListMessage(Types.CharacterBaseInformations[] characters)
        {
            this.characters = characters;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUShort((ushort)characters.Length);
            foreach (var entry in characters)
            {
                 writer.WriteShort(entry.TypeId);
                 entry.Serialize(writer);
            }
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            var limit = reader.ReadUShort();
            characters = new Types.CharacterBaseInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                var aux = (short)reader.ReadUShort();
                 characters[i] = Types.ProtocolTypeManager.GetInstance<Types.CharacterBaseInformations>(aux);
                 characters[i].Deserialize(reader);
            }
        }
        
    }
    
}