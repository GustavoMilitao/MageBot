


















// Generated on 12/11/2014 19:02:03
using System;
using System.Collections.Generic;
using System.Linq;
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class ActorRestrictionsInformations
    {

        public new const int ID = 204;
        public virtual int TypeId
        {
            get { return ID; }
        }

        public bool cantBeAggressed = false;
        public bool cantBeChallenged = false;
        public bool cantTrade = false;
        public bool cantBeAttackedByMutant = false;
        public bool cantRun = false;
        public bool forceSlowWalk = false;
        public bool cantMinimize = false;
        public bool cantMove = false;
        public bool cantAggress = false;
        public bool cantChallenge = false;
        public bool cantExchange = false;
        public bool cantAttack = false;
        public bool cantChat = false;
        public bool cantBeMerchant = false;
        public bool cantUseObject = false;
        public bool cantUseTaxCollector = false;
        public bool cantUseInteractive = false;
        public bool cantSpeakToNPC = false;
        public bool cantChangeZone = false;
        public bool cantAttackMonster = false;
        public bool cantWalk8Directions = false;

        public ActorRestrictionsInformations()
        {
        }



        public virtual void Serialize(BigEndianWriter writer)
        {



        }

        public virtual void Deserialize(BigEndianReader reader)
        {

            var _loc_2 = reader.ReadByte();
            cantBeAggressed = BooleanByteWrapper.GetFlag(_loc_2, 0);
            cantBeChallenged = BooleanByteWrapper.GetFlag(_loc_2, 1);
            cantTrade = BooleanByteWrapper.GetFlag(_loc_2, 2);
            cantBeAttackedByMutant = BooleanByteWrapper.GetFlag(_loc_2, 3);
            cantRun = BooleanByteWrapper.GetFlag(_loc_2, 4);
            forceSlowWalk = BooleanByteWrapper.GetFlag(_loc_2, 5);
            cantMinimize = BooleanByteWrapper.GetFlag(_loc_2, 6);
            cantMove = BooleanByteWrapper.GetFlag(_loc_2, 7);
            var _loc_3 = reader.ReadByte();
            cantAggress = BooleanByteWrapper.GetFlag(_loc_3, 0);
            cantChallenge = BooleanByteWrapper.GetFlag(_loc_3, 1);
            cantExchange = BooleanByteWrapper.GetFlag(_loc_3, 2);
            cantAttack = BooleanByteWrapper.GetFlag(_loc_3, 3);
            cantChat = BooleanByteWrapper.GetFlag(_loc_3, 4);
            cantBeMerchant = BooleanByteWrapper.GetFlag(_loc_3, 5);
            cantUseObject = BooleanByteWrapper.GetFlag(_loc_3, 6);
            cantUseTaxCollector = BooleanByteWrapper.GetFlag(_loc_3, 7);
            var _loc_4 = reader.ReadByte();
            cantUseInteractive = BooleanByteWrapper.GetFlag(_loc_4, 0);
            cantSpeakToNPC = BooleanByteWrapper.GetFlag(_loc_4, 1);
            cantChangeZone = BooleanByteWrapper.GetFlag(_loc_4, 2);
            cantAttackMonster = BooleanByteWrapper.GetFlag(_loc_4, 3);
            cantWalk8Directions = BooleanByteWrapper.GetFlag(_loc_4, 4);

        }


    }


}