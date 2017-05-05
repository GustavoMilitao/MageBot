


















// Generated on 12/11/2014 19:02:04
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class FightResultExperienceData : FightResultAdditionalData
{

public new const int ID = 192;
public override int TypeId
{
    get { return ID; }
}

public double experience;
        public double experienceLevelFloor;
        public double experienceNextLevelFloor;
        public int experienceFightDelta;
        public int experienceForGuild;
        public int experienceForMount;
        public sbyte rerollExperienceMul;
          public bool showExperience;
      
      
      public bool showExperienceLevelFloor;
      
      
      public bool showExperienceNextLevelFloor;
      
      
      public bool showExperienceFightDelta;
      
      
      public bool showExperienceForGuild;
      
      public bool showExperienceForMount;
      
      public bool isIncarnationExperience;
        

public FightResultExperienceData()
{
}

public FightResultExperienceData(double experience, double experienceLevelFloor, double experienceNextLevelFloor, int experienceFightDelta, int experienceForGuild, int experienceForMount, sbyte rerollExperienceMul)
        {
            this.experience = experience;
            this.experienceLevelFloor = experienceLevelFloor;
            this.experienceNextLevelFloor = experienceNextLevelFloor;
            this.experienceFightDelta = experienceFightDelta;
            this.experienceForGuild = experienceForGuild;
            this.experienceForMount = experienceForMount;
            this.rerollExperienceMul = rerollExperienceMul;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteDouble(experience);
            writer.WriteDouble(experienceLevelFloor);
            writer.WriteDouble(experienceNextLevelFloor);
            writer.WriteVarInt(experienceFightDelta);
            writer.WriteVarInt(experienceForGuild);
            writer.WriteVarInt(experienceForMount);
            writer.WriteSByte(rerollExperienceMul);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
             byte b= reader.ReadByte();
            showExperience = BooleanByteWrapper.GetFlag(b,0);
            showExperienceLevelFloor = BooleanByteWrapper.GetFlag(b, 1);
            showExperienceNextLevelFloor = BooleanByteWrapper.GetFlag(b, 2);
            showExperienceFightDelta = BooleanByteWrapper.GetFlag(b, 3);
            showExperienceForGuild = BooleanByteWrapper.GetFlag(b, 4);
            showExperienceForMount = BooleanByteWrapper.GetFlag(b, 5);
            isIncarnationExperience = BooleanByteWrapper.GetFlag(b, 6);
            experience = reader.ReadVarUhLong();
            if (experience < 0 || experience > 9.007199254740992E15)
                throw new Exception("Forbidden value on experience = " + experience + ", it doesn't respect the following condition : experience < 0 || experience > 9.007199254740992E15");
            experienceLevelFloor = reader.ReadVarUhLong();
            if (experienceLevelFloor < 0 || experienceLevelFloor > 9.007199254740992E15)
                throw new Exception("Forbidden value on experienceLevelFloor = " + experienceLevelFloor + ", it doesn't respect the following condition : experienceLevelFloor < 0 || experienceLevelFloor > 9.007199254740992E15");
            experienceNextLevelFloor = reader.ReadDouble();
            if (experienceNextLevelFloor < 0 || experienceNextLevelFloor > 9.007199254740992E15)
                throw new Exception("Forbidden value on experienceNextLevelFloor = " + experienceNextLevelFloor + ", it doesn't respect the following condition : experienceNextLevelFloor < 0 || experienceNextLevelFloor > 9.007199254740992E15");
            experienceFightDelta = reader.ReadVarInt();
            experienceForGuild = reader.ReadVarInt();
            if (experienceForGuild < 0)
                throw new Exception("Forbidden value on experienceForGuild = " + experienceForGuild + ", it doesn't respect the following condition : experienceForGuild < 0");
            experienceForMount = reader.ReadVarInt();
            if (experienceForMount < 0)
                throw new Exception("Forbidden value on experienceForMount = " + experienceForMount + ", it doesn't respect the following condition : experienceForMount < 0");
            rerollExperienceMul = reader.ReadSByte();
            if (rerollExperienceMul < 0)
                throw new Exception("Forbidden value on rerollExperienceMul = " + rerollExperienceMul + ", it doesn't respect the following condition : rerollExperienceMul < 0");
            

}


}


}