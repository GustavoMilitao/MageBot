









// Generated on 12/11/2014 19:01:34
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class HouseToSellListMessage : Message
    {
        public new const uint ID =6140;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public int pageIndex;
        public int totalPage;
        public Types.HouseInformationsForSell[] houseList;
        
        public HouseToSellListMessage()
        {
        }
        
        public HouseToSellListMessage(int pageIndex, int totalPage, Types.HouseInformationsForSell[] houseList)
        {
            this.pageIndex = pageIndex;
            this.totalPage = totalPage;
            this.houseList = houseList;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteVarShort((short)pageIndex);
            writer.WriteVarShort((short)totalPage);
            writer.WriteUShort((ushort)houseList.Length);
            foreach (var entry in houseList)
            {
                 entry.Serialize(writer);
            }
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            pageIndex = reader.ReadVarUhShort();
            if (pageIndex < 0)
                throw new Exception("Forbidden value on pageIndex = " + pageIndex + ", it doesn't respect the following condition : pageIndex < 0");
            totalPage = reader.ReadVarUhShort();
            if (totalPage < 0)
                throw new Exception("Forbidden value on totalPage = " + totalPage + ", it doesn't respect the following condition : totalPage < 0");
            var limit = reader.ReadUShort();
            houseList = new Types.HouseInformationsForSell[limit];
            for (int i = 0; i < limit; i++)
            {
                 houseList[i] = new Types.HouseInformationsForSell();
                 houseList[i].Deserialize(reader);
            }
        }
        
    }
    
}