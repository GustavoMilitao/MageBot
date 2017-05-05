









// Generated on 12/11/2014 19:01:40
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class QuestListMessage : Message
    {
        public new const uint ID =5626;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int[] finishedQuestsIds;
        public int[] finishedQuestsCounts;
        public Types.QuestActiveInformations[] activeQuests;
        
        public QuestListMessage()
        {
        }
        
        public QuestListMessage(int[] finishedQuestsIds, int[] finishedQuestsCounts, Types.QuestActiveInformations[] activeQuests)
        {
            this.finishedQuestsIds = finishedQuestsIds;
            this.finishedQuestsCounts = finishedQuestsCounts;
            this.activeQuests = activeQuests;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUShort((ushort)finishedQuestsIds.Length);
            foreach (var entry in finishedQuestsIds)
            {
                 writer.WriteVarShort((short)entry);
            }
            writer.WriteUShort((ushort)finishedQuestsCounts.Length);
            foreach (var entry in finishedQuestsCounts)
            {
                 writer.WriteVarShort((short)entry);
            }
            writer.WriteUShort((ushort)activeQuests.Length);
            foreach (var entry in activeQuests)
            {
                 writer.WriteShort((short)entry.TypeId);
                 entry.Serialize(writer);
            }
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            var limit = reader.ReadUShort();
            finishedQuestsIds = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 finishedQuestsIds[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            finishedQuestsCounts = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 finishedQuestsCounts[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            activeQuests = new Types.QuestActiveInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 activeQuests[i] = Types.ProtocolTypeManager.GetInstance<Types.QuestActiveInformations>(reader.ReadUShort());
                 activeQuests[i].Deserialize(reader);
            }
        }
        
    }
    
}