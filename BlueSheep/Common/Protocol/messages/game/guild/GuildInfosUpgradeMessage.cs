









// Generated on 12/11/2014 19:01:44
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.Protocol.Types;
using BlueSheep.Common.IO;
using BlueSheep.Engine.Types;

namespace BlueSheep.Common.Protocol.Messages
{
    public class GuildInfosUpgradeMessage : Message
    {
        public new const uint ID =5636;
        public override uint ProtocolID
        {
            get { return ID; }
        }
        
        public sbyte maxTaxCollectorsCount;
        public sbyte taxCollectorsCount;
        public int taxCollectorLifePoints;
        public int taxCollectorDamagesBonuses;
        public int taxCollectorPods;
        public int taxCollectorProspecting;
        public int taxCollectorWisdom;
        public int boostPoints;
        public int[] spellId;
        public sbyte[] spellLevel;
        
        public GuildInfosUpgradeMessage()
        {
        }
        
        public GuildInfosUpgradeMessage(sbyte maxTaxCollectorsCount, sbyte taxCollectorsCount, int taxCollectorLifePoints, int taxCollectorDamagesBonuses, int taxCollectorPods, int taxCollectorProspecting, int taxCollectorWisdom, int boostPoints, int[] spellId, sbyte[] spellLevel)
        {
            this.maxTaxCollectorsCount = maxTaxCollectorsCount;
            this.taxCollectorsCount = taxCollectorsCount;
            this.taxCollectorLifePoints = taxCollectorLifePoints;
            this.taxCollectorDamagesBonuses = taxCollectorDamagesBonuses;
            this.taxCollectorPods = taxCollectorPods;
            this.taxCollectorProspecting = taxCollectorProspecting;
            this.taxCollectorWisdom = taxCollectorWisdom;
            this.boostPoints = boostPoints;
            this.spellId = spellId;
            this.spellLevel = spellLevel;
        }
        
        public override void Serialize(BigEndianWriter writer)
        {
            writer.WriteSByte(maxTaxCollectorsCount);
            writer.WriteSByte(taxCollectorsCount);
            writer.WriteVarShort((short)taxCollectorLifePoints);
            writer.WriteVarShort((short)taxCollectorDamagesBonuses);
            writer.WriteVarShort((short)taxCollectorPods);
            writer.WriteVarShort((short)taxCollectorProspecting);
            writer.WriteVarShort((short)taxCollectorWisdom);
            writer.WriteVarShort((short)boostPoints);
            writer.WriteUShort((ushort)spellId.Length);
            foreach (var entry in spellId)
            {
                 writer.WriteVarShort((short)entry);
            }
            writer.WriteUShort((ushort)spellLevel.Length);
            foreach (var entry in spellLevel)
            {
                 writer.WriteSByte(entry);
            }
        }
        
        public override void Deserialize(BigEndianReader reader)
        {
            maxTaxCollectorsCount = reader.ReadSByte();
            if (maxTaxCollectorsCount < 0)
                throw new Exception("Forbidden value on maxTaxCollectorsCount = " + maxTaxCollectorsCount + ", it doesn't respect the following condition : maxTaxCollectorsCount < 0");
            taxCollectorsCount = reader.ReadSByte();
            if (taxCollectorsCount < 0)
                throw new Exception("Forbidden value on taxCollectorsCount = " + taxCollectorsCount + ", it doesn't respect the following condition : taxCollectorsCount < 0");
            taxCollectorLifePoints = reader.ReadVarUhShort();
            if (taxCollectorLifePoints < 0)
                throw new Exception("Forbidden value on taxCollectorLifePoints = " + taxCollectorLifePoints + ", it doesn't respect the following condition : taxCollectorLifePoints < 0");
            taxCollectorDamagesBonuses = reader.ReadVarUhShort();
            if (taxCollectorDamagesBonuses < 0)
                throw new Exception("Forbidden value on taxCollectorDamagesBonuses = " + taxCollectorDamagesBonuses + ", it doesn't respect the following condition : taxCollectorDamagesBonuses < 0");
            taxCollectorPods = reader.ReadVarUhShort();
            if (taxCollectorPods < 0)
                throw new Exception("Forbidden value on taxCollectorPods = " + taxCollectorPods + ", it doesn't respect the following condition : taxCollectorPods < 0");
            taxCollectorProspecting = reader.ReadVarUhShort();
            if (taxCollectorProspecting < 0)
                throw new Exception("Forbidden value on taxCollectorProspecting = " + taxCollectorProspecting + ", it doesn't respect the following condition : taxCollectorProspecting < 0");
            taxCollectorWisdom = reader.ReadVarUhShort();
            if (taxCollectorWisdom < 0)
                throw new Exception("Forbidden value on taxCollectorWisdom = " + taxCollectorWisdom + ", it doesn't respect the following condition : taxCollectorWisdom < 0");
            boostPoints = reader.ReadVarUhShort();
            if (boostPoints < 0)
                throw new Exception("Forbidden value on boostPoints = " + boostPoints + ", it doesn't respect the following condition : boostPoints < 0");
            var limit = reader.ReadUShort();
            spellId = new int[limit];
            for (int i = 0; i < limit; i++)
            {
                 spellId[i] = reader.ReadVarUhShort();
            }
            limit = reader.ReadUShort();
            spellLevel = new sbyte[limit];
            for (int i = 0; i < limit; i++)
            {
                 spellLevel[i] = reader.ReadSByte();
            }
        }
        
    }
    
}