using BlueSheep.Common.IO;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class ShortcutObjectIdolsPreset : ShortcutObject
    {
        public new const uint ID = 492;
        public uint presetId = 0;

        public virtual void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            if (this.presetId < 0)
            {
                throw new Exception("Forbidden value (" + this.presetId + ") on element presetId.");
            }
            writer.WriteByte((byte)presetId);
        }

        public virtual void Deserialize(BigEndianReader reader)
        {
            base.Deserialize(reader);
            _presetIdFunc(reader);
        }

        private void _presetIdFunc(BigEndianReader reader)
        {
            this.presetId = reader.ReadByte();
            if (this.presetId < 0)
            {
                throw new Exception("Forbidden value (" + this.presetId + ") on element of ShortcutObjectIdolsPreset.presetId.");
            }
        }

    }
}