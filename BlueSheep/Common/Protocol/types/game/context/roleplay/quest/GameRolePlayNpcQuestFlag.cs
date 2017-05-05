


















// Generated on 12/11/2014 19:02:08
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GameRolePlayNpcQuestFlag
    {

        public new const int ID = 384;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public int[] questsToValidId;
        public int[] questsToStartId;


        public GameRolePlayNpcQuestFlag()
        {
        }

        public GameRolePlayNpcQuestFlag(int[] questsToValidId, int[] questsToStartId)
        {
            this.questsToValidId = questsToValidId;
            this.questsToStartId = questsToStartId;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteUShort((ushort)questsToValidId.Length);
            foreach (var entry in questsToValidId)
            {
                writer.WriteVarShort((short)entry);
            }
            writer.WriteUShort((ushort)questsToStartId.Length);
            foreach (var entry in questsToStartId)
            {
                writer.WriteVarShort((short)entry);
            }


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            var limit = reader.ReadUShort();
            questsToValidId = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                questsToValidId[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            questsToStartId = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                questsToStartId[i] = reader.ReadVarUhShort();
            }
        }


    }


}