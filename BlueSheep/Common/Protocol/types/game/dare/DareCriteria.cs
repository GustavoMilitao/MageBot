using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;
using System.Collections.Generic;
using System;

namespace BlueSheep.Common.Protocol.Types
{
    public class DareCriteria : Message
    {
        public const uint ID = 501;

        public List<int> parameters;

        public uint type = 0;

        public override uint ProtocolID => ID;

        public override void Deserialize(BigEndianReader reader)
        {
            int _loc4_ = 0;
            _typeFunc(reader);
            uint _loc2_ = reader.ReadUShort();
            uint _loc3_ = 0;
            while (_loc3_ < _loc2_)
            {
                _loc4_ = reader.ReadInt();
                parameters.Add(_loc4_);
                _loc3_++;
            }
        }

        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteByte((byte)type);
            writer.WriteShort((short)parameters.Count);
            uint _loc2_ = 0;
            while (_loc2_ < parameters.Count)
            {
                writer.WriteInt(parameters[(int)_loc2_]);
                _loc2_++;
            }

        }

        private void _paramsFunc(BigEndianReader reader)
        {
            int _loc2_ = reader.ReadInt();
            parameters.Add(_loc2_);
        }


        private void _typeFunc(BigEndianReader reader)
        {
            type = reader.ReadByte();
            if (this.type < 0)
            {
                throw new Exception("Forbidden value (" + type + ") on element of DareCriteria.type.");
            }
        }

    }
}