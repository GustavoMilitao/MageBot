


















// Generated on 12/11/2014 19:02:11
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class PrismFightersInformation
    {

        public new const int ID = 443;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public int subAreaId;
        public Types.ProtectedEntityWaitingForHelpInfo waitingForHelpInfo;
        public Types.CharacterMinimalPlusLookInformations[] allyCharactersInformations;
        public Types.CharacterMinimalPlusLookInformations[] enemyCharactersInformations;


        public PrismFightersInformation()
        {
        }

        public PrismFightersInformation(int subAreaId, Types.ProtectedEntityWaitingForHelpInfo waitingForHelpInfo, Types.CharacterMinimalPlusLookInformations[] allyCharactersInformations, Types.CharacterMinimalPlusLookInformations[] enemyCharactersInformations)
        {
            this.subAreaId = subAreaId;
            this.waitingForHelpInfo = waitingForHelpInfo;
            this.allyCharactersInformations = allyCharactersInformations;
            this.enemyCharactersInformations = enemyCharactersInformations;
        }


        public virtual void Serialize(BigEndianWriter writer)
        {

            writer.WriteVarShort((short)subAreaId);
            waitingForHelpInfo.Serialize(writer);
            writer.WriteUShort((ushort)allyCharactersInformations.Length);
            foreach (var entry in allyCharactersInformations)
            {
                writer.WriteShort((short)entry.TypeId);
                entry.Serialize(writer);
            }
            writer.WriteUShort((ushort)enemyCharactersInformations.Length);
            foreach (var entry in enemyCharactersInformations)
            {
                writer.WriteShort((short)entry.TypeId);
                entry.Serialize(writer);
            }


        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            subAreaId = reader.ReadVarUhShort();
            if (subAreaId < 0)
                throw new Exception("Forbidden value on subAreaId = " + subAreaId + ", it doesn't respect the following condition : subAreaId < 0");
            waitingForHelpInfo = new Types.ProtectedEntityWaitingForHelpInfo();
            waitingForHelpInfo.Deserialize(reader);
            var limit = reader.ReadUShort();
            allyCharactersInformations = new Types.CharacterMinimalPlusLookInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                allyCharactersInformations[i] = Types.ProtocolTypeManager.GetInstance<Types.CharacterMinimalPlusLookInformations>(reader.ReadUShort());
                allyCharactersInformations[i].Deserialize(reader);
            }
            limit = reader.ReadUShort();
            enemyCharactersInformations = new Types.CharacterMinimalPlusLookInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                enemyCharactersInformations[i] = Types.ProtocolTypeManager.GetInstance<Types.CharacterMinimalPlusLookInformations>(reader.ReadUShort());
                enemyCharactersInformations[i].Deserialize(reader);
            }


        }


    }


}