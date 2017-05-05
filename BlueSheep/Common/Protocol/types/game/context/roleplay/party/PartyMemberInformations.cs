


















// Generated on 12/11/2014 19:02:08
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class PartyMemberInformations : CharacterBaseInformations
{

public new const int ID = 90;
public override int TypeId
{
    get { return ID; }
}

public int lifePoints;
        public int maxLifePoints;
        public int prospecting;
        public byte regenRate;
        public int initiative;
        public sbyte alignmentSide;
        public int worldX;
        public int worldY;
        public int mapId;
        public int subAreaId;
        public Types.PlayerStatus status;
        public Types.PartyCompanionMemberInformations[] companions;
        

public PartyMemberInformations()
{
}

public PartyMemberInformations(uint id, byte level, string name, Types.EntityLook entityLook, sbyte breed, bool sex, int lifePoints, int maxLifePoints, int prospecting, byte regenRate, int initiative, sbyte alignmentSide, int worldX, int worldY, int mapId, int subAreaId, Types.PlayerStatus status, Types.PartyCompanionMemberInformations[] companions)
         : base(id, level, name, entityLook, breed, sex)
        {
            this.lifePoints = lifePoints;
            this.maxLifePoints = maxLifePoints;
            this.prospecting = prospecting;
            this.regenRate = regenRate;
            this.initiative = initiative;
            this.alignmentSide = alignmentSide;
            this.worldX = worldX;
            this.worldY = worldY;
            this.mapId = mapId;
            this.subAreaId = subAreaId;
            this.status = status;
            this.companions = companions;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteVarInt(lifePoints);
            writer.WriteVarInt(maxLifePoints);
            writer.WriteVarShort((short)prospecting);
            writer.WriteByte(regenRate);
            writer.WriteVarShort((short)initiative);
            writer.WriteSByte(alignmentSide);
            writer.WriteShort((short)worldX);
            writer.WriteShort((short)worldY);
            writer.WriteInt(mapId);
            writer.WriteVarShort((short)subAreaId);
            writer.WriteShort((short)status.TypeId);
            status.Serialize(writer);
            writer.WriteUShort((ushort)companions.Length);
            foreach (var entry in companions)
            {
                 entry.Serialize(writer);
            }
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            lifePoints = reader.ReadVarInt();
            if (lifePoints < 0)
                throw new Exception("Forbidden value on lifePoints = " + lifePoints + ", it doesn't respect the following condition : lifePoints < 0");
            maxLifePoints = reader.ReadVarInt();
            if (maxLifePoints < 0)
                throw new Exception("Forbidden value on maxLifePoints = " + maxLifePoints + ", it doesn't respect the following condition : maxLifePoints < 0");
            prospecting = reader.ReadVarUhShort();
            if (prospecting < 0)
                throw new Exception("Forbidden value on prospecting = " + prospecting + ", it doesn't respect the following condition : prospecting < 0");
            regenRate = reader.ReadByte();
            if (regenRate < 0 || regenRate > 255)
                throw new Exception("Forbidden value on regenRate = " + regenRate + ", it doesn't respect the following condition : regenRate < 0 || regenRate > 255");
            initiative = reader.ReadVarUhShort();
            if (initiative < 0)
                throw new Exception("Forbidden value on initiative = " + initiative + ", it doesn't respect the following condition : initiative < 0");
            alignmentSide = reader.ReadSByte();
            worldX = reader.ReadShort();
            if (worldX < -255 || worldX > 255)
                throw new Exception("Forbidden value on worldX = " + worldX + ", it doesn't respect the following condition : worldX < -255 || worldX > 255");
            worldY = reader.ReadShort();
            if (worldY < -255 || worldY > 255)
                throw new Exception("Forbidden value on worldY = " + worldY + ", it doesn't respect the following condition : worldY < -255 || worldY > 255");
            mapId = reader.ReadInt();
            subAreaId = reader.ReadVarUhShort();
            if (subAreaId < 0)
                throw new Exception("Forbidden value on subAreaId = " + subAreaId + ", it doesn't respect the following condition : subAreaId < 0");
            status = Types.ProtocolTypeManager.GetInstance<Types.PlayerStatus>(reader.ReadUShort());
            status.Deserialize(reader);
            var limit = reader.ReadUShort();
            companions = new Types.PartyCompanionMemberInformations[limit];
            for (int i = 0; i < limit; i++)
            {
                 companions[i] = new Types.PartyCompanionMemberInformations();
                 companions[i].Deserialize(reader);
            }
            

}


}


}