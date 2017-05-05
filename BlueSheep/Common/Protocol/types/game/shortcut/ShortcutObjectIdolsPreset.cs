using BlueSheep.Common.IO;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class ShortcutObjectIdolsPreset : ShortcutObject
    {
        public new const int ID = 492;
        public uint presetId = 0;
        public ShortcutObjectIdolsPreset()
        {

        }
        public virtual void Serialize(BigEndianWriter writer)
        {
            base.Serialize(writer);
            if (presetId < 0)
            {
                throw new Exception("Forbidden value (" + presetId + ") on element presetId.");
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
            presetId = reader.ReadByte();
            if (presetId < 0)
            {
                throw new Exception("Forbidden value (" + presetId + ") on element of ShortcutObjectIdolsPreset.presetId.");
            }
        }

    }
}