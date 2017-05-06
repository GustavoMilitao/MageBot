using BlueSheep.Common.Protocol.Types.Game.Approach;
using BlueSheep.Engine.Types;
using System.Collections.Generic;

namespace BlueSheep.Common.Protocol.Messages.Game.Approach
{
    public class ServerSessionConstantsMessage : Message
    {
        public new const int ID = 6434;
        public override int MessageID { get { return ID; } }

        public List<ServerSessionConstant> Variables;

        public ServerSessionConstantsMessage() { }

        public ServerSessionConstantsMessage(List<ServerSessionConstant> variables)
        {
            Variables = variables;
        }

        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort((short)(Variables.Count));
            for (int i = 0; i < Variables.Count; i++)
            {
                ServerSessionConstant objectToSend = Variables[i];
                writer.WriteShort((short)objectToSend.TypeID);
                objectToSend.Serialize(writer);
            }
        }

        public override void Deserialize(IDataReader reader)
        {
            int length = reader.ReadUShort();
            Variables = new List<ServerSessionConstant>();
            for (int i = 0; i < length; i++)
            {
                ServerSessionConstant objectToAdd = new ServerSessionConstant(reader.ReadUShort());
                objectToAdd.Deserialize(reader);
                Variables.Add(objectToAdd);
            }
        }
    }
}
