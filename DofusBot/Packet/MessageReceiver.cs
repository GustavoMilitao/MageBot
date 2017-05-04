using DofusBot.Core.Network;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DofusBot.Packet
{
    public static class MessageReceiver
    {

        private static readonly Dictionary<uint, Type> messages = new Dictionary<uint, Type>();

        static MessageReceiver()
        {
            Assembly asm = Assembly.GetAssembly(typeof(MessageReceiver));

            foreach (Type type in asm.GetTypes())
            {
                if (!type.IsSubclassOf(typeof(NetworkMessage)))
                    continue;

                FieldInfo fieldId = type.GetField("ProtocolId");

                if (fieldId != null)
                {
                    var id = (uint)fieldId.GetValue(type);
                    if (messages.ContainsKey(id))
                        throw new AmbiguousMatchException(
                            string.Format(
                                "MessageReceiver() => {0} item is already in the dictionary, old type is : {1}, new type is  {2}",
                                id, messages[id], type));

                    messages.Add(id, type);
                }
            }
        }

        public static NetworkMessage BuildMessage(uint id, IDataReader reader)
        {
            try
            {
                if (!messages.ContainsKey(id))
                    throw new Exception(string.Format("NetworkMessage <id:{0}> doesn't exist", id));

                NetworkMessage message = (NetworkMessage)Activator.CreateInstance(messages[id]);

                if (message == null)
                    throw new Exception(string.Format("Constructors[{0}] (delegate {1}) does not exist", id, messages[id]));

                message.Unpack(reader);

                return message;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Can't BuildMessage for id {0} ({1}).", id, ex.Message));
                return null;
            }
        }

        public static Type GetMessageType(uint id)
        {
            if (!messages.ContainsKey(id))
                throw new Exception(string.Format("NetworkMessage <id:{0}> doesn't exist", id));

            return messages[id];
        }

    }
}
    
