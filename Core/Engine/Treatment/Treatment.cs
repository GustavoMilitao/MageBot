using BlueSheep.Engine.Types;
using System;
using System.Collections.Generic;
using System.Reflection;
using BotForge.Core.Account;
using System.Linq;
using BotForgeAPI.Protocol.Types;
using Core.Engine.Handlers;

namespace BlueSheep.Engine.Treatment
{
    public class Treatment
    {
        #region Fields
        private readonly List<InstanceInfo> Instances = new List<InstanceInfo>();
        private Account account;
        #endregion

        #region Constructeurs
        public Treatment(Account account)
        {
            GetTypes("Core");
            this.account = account;
        }
        #endregion

        #region Public methods
        public void Treat(BotForgeAPI.Network.NetworkMessage message)
        {

            InstanceInfo instance = Instances.FirstOrDefault(inst => inst.MessageType == message.GetType());
            if (instance != null)
            {
                MethodInfo method = instance.Method;
                if (method != null)
                {
                    account.Logger.Log("[RCV] " + message.ProtocolId + " (" + method.Name.Remove(method.Name.IndexOf("Treatment")) + ")", BotForgeAPI.Logger.LogEnum.Debug);
                    object[] parameters = { message, account };
                    method.Invoke(null, parameters);
                }
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

                    InstanceInfo instance = new InstanceInfo(messageHandler.MessageType, method);

                    Instances.Add(instance);
                }
            }
        }
        #endregion
    }
}
