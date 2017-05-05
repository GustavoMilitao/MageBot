


















// Generated on 12/11/2014 19:02:09
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

public class ObjectEffectDuration : ObjectEffect
{

public new const int ID = 75;
public override int TypeId
{
    get { return ID; }
}

public int days;
        public sbyte hours;
        public sbyte minutes;
        

public ObjectEffectDuration()
{
}

public ObjectEffectDuration(int actionId, int days, sbyte hours, sbyte minutes)
         : base(actionId)
        {
            this.days = days;
            this.hours = hours;
            this.minutes = minutes;
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            writer.WriteVarShort((short)days);
            writer.WriteSByte(hours);
            writer.WriteSByte(minutes);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            days = reader.ReadVarUhShort();
            if (days < 0)
                throw new Exception("Forbidden value on days = " + days + ", it doesn't respect the following condition : days < 0");
            hours = reader.ReadSByte();
            if (hours < 0)
                throw new Exception("Forbidden value on hours = " + hours + ", it doesn't respect the following condition : hours < 0");
            minutes = reader.ReadSByte();
            if (minutes < 0)
                throw new Exception("Forbidden value on minutes = " + minutes + ", it doesn't respect the following condition : minutes < 0");
            

}


}


}