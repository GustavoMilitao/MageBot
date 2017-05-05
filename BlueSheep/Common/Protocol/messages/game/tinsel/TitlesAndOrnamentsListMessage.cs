









// Generated on 12/11/2014 19:02:00
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class TitlesAndOrnamentsListMessage : Message
    {
        public new const uint ID =6367;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int[] titles;
        public int[] ornaments;
        public int activeTitle;
        public int activeOrnament;
        
        public TitlesAndOrnamentsListMessage()
        {
        }
        
        public TitlesAndOrnamentsListMessage(int[] titles, int[] ornaments, int activeTitle, int activeOrnament)
        {
            this.titles = titles;
            this.ornaments = ornaments;
            this.activeTitle = activeTitle;
            this.activeOrnament = activeOrnament;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteUShort((ushort)titles.Length);
            foreach (var entry in titles)
            {
                 writer.WriteVarShort((short)entry);
            }
            writer.WriteUShort((ushort)ornaments.Length);
            foreach (var entry in ornaments)
            {
                 writer.WriteVarShort((short)entry);
            }
            writer.WriteVarShort((short)activeTitle);
            writer.WriteVarShort((short)activeOrnament);
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            var limit = reader.ReadUShort();
            titles = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 titles[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            ornaments = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 ornaments[i] = reader.ReadVarUhShort();
            }
            activeTitle = reader.ReadVarUhShort();
            if (activeTitle < 0)
                throw new Exception("Forbidden value on activeTitle = " + activeTitle + ", it doesn't respect the following condition : activeTitle < 0");
            activeOrnament = reader.ReadVarUhShort();
            if (activeOrnament < 0)
                throw new Exception("Forbidden value on activeOrnament = " + activeOrnament + ", it doesn't respect the following condition : activeOrnament < 0");
        }
        
    }
    
}