


















// Generated on 12/11/2014 19:02:06
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GameRolePlayGroupMonsterWaveInformations : GameRolePlayGroupMonsterInformations
    {

        public new const int ID = 464;
        public override int TypeId
        {
            get { return ID; }
        }

        public byte nbWaves;
        public Types.GroupMonsterStaticInformations[] alternatives;


        public GameRolePlayGroupMonsterWaveInformations()
        {
        }

        public GameRolePlayGroupMonsterWaveInformations(ulong contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, Types.GroupMonsterStaticInformations staticInfos, int ageBonus, byte lootShare, byte alignmentSide, byte nbWaves, Types.GroupMonsterStaticInformations[] alternatives, double creationTime)
                 : base(contextualId, look, disposition, staticInfos, ageBonus, lootShare, alignmentSide, creationTime)
        {
            this.nbWaves = nbWaves;
            this.alternatives = alternatives;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteByte(nbWaves);
            writer.WriteUShort((ushort)alternatives.Length);
            foreach (var entry in alternatives)
            {
                writer.WriteShort((short)entry.TypeId);
                entry.Serialize(writer);
            }


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            nbWaves = reader.ReadByte();
            if (nbWaves < 0)
                throw new Exception("Forbidden value on nbWaves = " + nbWaves + ", it doesn't respect the following condition : nbWaves < 0");
            var limit = reader.ReadUShort();
            alternatives = new Types.GroupMonsterStaticInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                alternatives[i] = Types.ProtocolTypeManager.GetInstance<GroupMonsterStaticInformations>(reader.ReadUShort());
                alternatives[i].Deserialize(reader);
            }


        }


    }


}