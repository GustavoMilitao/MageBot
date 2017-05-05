


















// Generated on 12/11/2014 19:02:05
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class GameFightFighterInformations : GameContextActorInformations
    {

        public new const int ID = 143;
        public override int TypeId
        {
            get { return ID; }
        }

        public sbyte teamId;
        public sbyte wave;
        public bool alive;
        public Types.GameFightMinimalStats stats;
        public int[] previousPositions;


        public GameFightFighterInformations()
        {
        }

        public GameFightFighterInformations(ulong contextualId, Types.EntityLook look, Types.EntityDispositionInformations disposition, sbyte teamId, sbyte wave, bool alive, Types.GameFightMinimalStats stats, int[] previousPositions)
                 : base(contextualId, look, disposition)
        {
            this.teamId = teamId;
            this.wave = wave;
            this.alive = alive;
            this.stats = stats;
            this.previousPositions = previousPositions;
        }


        public override void Serialize(BigEndianWriter writer)
        {

            base.Serialize(writer);
            writer.WriteSByte(teamId);
            writer.WriteSByte(wave);
            writer.WriteBoolean(alive);
            writer.WriteShort((short)stats.TypeId);
            stats.Serialize(writer);
            writer.WriteUShort((ushort)previousPositions.Length);
            foreach (var entry in previousPositions)
            {
                writer.WriteVarShort((short)entry);
            }


        }

        public override void Deserialize(BigEndianReader reader)
        {

            base.Deserialize(reader);
            teamId = reader.ReadSByte();
            if (teamId < 0)
                throw new Exception("Forbidden value on teamId = " + teamId + ", it doesn't respect the following condition : teamId < 0");
            wave = reader.ReadSByte();
            if (wave < 0)
                throw new Exception("Forbidden value on wave = " + wave + ", it doesn't respect the following condition : wave < 0");
            alive = reader.ReadBoolean();
            stats = Types.ProtocolTypeManager.GetInstance<Types.GameFightMinimalStats>(reader.ReadUShort());
            stats.Deserialize(reader);
            var limit = reader.ReadUShort();
            previousPositions = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                previousPositions[i] = reader.ReadVarUhShort();
            }


        }


    }


}