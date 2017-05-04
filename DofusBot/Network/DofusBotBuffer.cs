using DofusBot.Core;
using System;
using System.Collections.Generic;

namespace DofusBot.Network
{
    public class DofusBotBuffer
    {
        private Queue<byte> _byteQueue = new Queue<byte>();
        public event EventHandler<PacketBufferEventArg> ReceivePacketBuffer;
        public delegate void ReceivePacketBufferEventHandler(PacketBufferEventArg e);

        protected virtual void OnReceivePacketBuffer(PacketBufferEventArg e)
        {
            ReceivePacketBuffer.Raise(this, e);
        }

        public void Enqueu(byte[] buffer)
        {
            foreach (byte b in buffer)
            {
                _byteQueue.Enqueue(b); // On ajoute chaque byte du buffer à _byteQueue
            }
            ProcessQueue(); // A voir plus bas
        }

        private void ProcessQueue()
        {
            while (_byteQueue.Count > 0) // Tant que _byteQueue n'est pas vide
            {
                if (_byteQueue.Count < 2) // Si le nombre de byte contenue est plus petit que 2, on ne pourra pas lire le header du packet, dans ce cas, on sort de la méthode 
                    return;

                BigEndianReader temp = new BigEndianReader(_byteQueue.ToArray()); // On crée un reader temporaire pour lire le header

                short header = temp.ReadShort(); // On lit le header

                short packetId = (short)(header >> 2); // On récupère l'id du packet

                short lenghtSize = (short)(header & 3); // On récupère la taille de la taille du packet

                if (_byteQueue.Count < 2 + lenghtSize) // Si on ne peu pas lire la taille du packet, on quitte la méthode
                    return;

                int lenght; // On récupère la taille du packet

                switch (lenghtSize)
                {
                    case 0: lenght = 0; break;
                    case 1: lenght = temp.ReadByte(); break;
                    case 2: lenght = temp.ReadUShort(); break;
                    case 3: lenght = (temp.ReadByte() << 16) + (temp.ReadByte() << 8) + temp.ReadByte(); break;
                    default:
                        throw new Exception("Unknow lenght");
                }

                if (_byteQueue.Count < lenght + lenghtSize + 2) // Si le nombre de byte contenue dans le buffer est plus petit que : 2 (taille du header) + lenghtSize(taille de la longueur du packet) + lenght (taille du packet) alors on sort de la méthode, on va recevoir prochainement la suite du packet.
                    return;

                DequeueByteUtils(lenghtSize); // On supprime de la _byteQueue les bytes utilisé

                byte[] data = new byte[lenght]; // On crée le data du packet

                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = _byteQueue.Dequeue(); // On le remplit
                }

                OnReceivePacketBuffer(new PacketBufferEventArg(packetId, data));
            }
        }

        private void DequeueByteUtils(short lenghtSize)
        {
            _byteQueue.Dequeue();//
            _byteQueue.Dequeue();// Header

            if (lenghtSize > 0)
                _byteQueue.Dequeue();
            if (lenghtSize > 1)
                _byteQueue.Dequeue();
            if (lenghtSize > 2)
                _byteQueue.Dequeue();
        }
    }
}
