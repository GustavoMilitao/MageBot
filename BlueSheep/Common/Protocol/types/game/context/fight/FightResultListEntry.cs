


















// Generated on 12/11/2014 19:02:04
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class FightResultListEntry
{

public new const int ID = 16;
public virtual int TypeId
{
    get { return ID; }
}

public int outcome;
        public sbyte wave;
        public Types.FightLoot rewards;
        

public FightResultListEntry()
{
}

public FightResultListEntry(int outcome, sbyte wave, Types.FightLoot rewards)
        {
            this.outcome = outcome;
            this.wave = wave;
            this.rewards = rewards;
        }
        

public virtual void Serialize(BigEndianWriter writer)
{

writer.WriteVarShort((short)outcome);
            writer.WriteSByte(wave);
            rewards.Serialize(writer);
            

}

public virtual void Deserialize(BigEndianReader reader)
{

outcome = reader.ReadVarUhShort();
            if (outcome < 0)
                throw new Exception("Forbidden value on outcome = " + outcome + ", it doesn't respect the following condition : outcome < 0");
            wave = reader.ReadSByte();
            if (wave < 0)
                throw new Exception("Forbidden value on wave = " + wave + ", it doesn't respect the following condition : wave < 0");
            rewards = new Types.FightLoot();
            rewards.Deserialize(reader);
            

}


}


}