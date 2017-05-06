using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class VersionExtended : Version
    {
        public new const int ID = 393;

        public virtual int TypeID { get { return ID; } }

        public sbyte Install { get; set; }
        public sbyte Technology { get; set; }

        public VersionExtended()
        {
        }

        public VersionExtended(sbyte major, sbyte minor, sbyte release, int revision, sbyte patch, sbyte buildType, sbyte install, sbyte technology)
         : base(major, minor, release, revision, patch, buildType)
        {
            this.Install = install;
            this.Technology = technology;
        }

        public void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteSByte(Install);
            writer.WriteSByte(Technology);
        }

        public void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            Install = reader.ReadSByte();
            if (Install < 0)
                throw new Exception("Forbidden value on Install = " + Install + ", it doesn't respect the following condition : install < 0");
            Technology = reader.ReadSByte();
            if (Technology < 0)
                throw new Exception("Forbidden value on Technology = " + Technology + ", it doesn't respect the following condition : technology < 0");
        }

    }

}