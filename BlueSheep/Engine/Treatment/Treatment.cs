using BlueSheep.Engine.Handlers;
using BlueSheep.Engine.Types;
using BlueSheep.Common;
using System;
using System.Collections.Generic;
using System.Reflection;
using BlueSheep.Util.Text.Log;
using BlueSheep.Core.Account;

namespace BlueSheep.Engine.Treatment
{
    class Treatment
    {
        #region Fields
        private readonly List<InstanceInfo> m_Instances = new List<InstanceInfo>();
        private Account account;
        #endregion

        #region Constructeurs
        public Treatment(Account account)
        {
            GetTypes("BlueSheep");
            this.account = account;
        }
        #endregion

        #region Public methods
        public void Treat(int packetID, byte[] packetDatas)
        {

            List<InstanceInfo> enqueue = new List<InstanceInfo>();

            foreach (InstanceInfo instance in m_Instances)
            {
                if (instance.ProtocolID == packetID)
                    enqueue.Add(instance);
            }

            foreach (InstanceInfo instance in enqueue)
            {
                Message message = (Message)Activator.CreateInstance(instance.MessageType);
                MethodInfo method = instance.Method;
                account.Log(new DebugTextInformation("[RCV] " + packetID + " (" + method.Name.Remove(method.Name.IndexOf("Treatment")) + ")"), 0);

                if (method == null)
                    return;

                object[] parameters = { message, packetDatas, account };

                method.Invoke(null, parameters);
            }
        }
        #endregion

        #region Private methods
        private void GetTypes(string assemblyName)
        {
            Assembly assembly = Assembly.Load(assemblyName);

            foreach (Type type in assembly.GetTypes())
            {
                MethodInfo[] methods = type.GetMethods();

                foreach (MethodInfo method in methods)
                {
                    MessageHandler messageHandler = (MessageHandler)Attribute.GetCustomAttribute(method, typeof(MessageHandler), false);

                    if (messageHandler == null)
                        continue;

                    Message message = (Message)(Activator.CreateInstance(messageHandler.MessageType));

                    InstanceInfo instance = new InstanceInfo((uint)message.MessageID, messageHandler.MessageType, method);

                    m_Instances.Add(instance);
                }
            }
        }
        #endregion
    }
}
