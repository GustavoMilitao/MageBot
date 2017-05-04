namespace  BlueSheep.Common.Protocol.Types {
public  class  DareVersatileInformations {
public  static  const  uint protocolId  = 504 ;
public    double dareId  = 0 ;
public    uint countEntrants  = 0 ;
public    uint countWinners  = 0 ;

public  uint getTypeId (  ) {return 504;
      }
public  void reset (  ) {
            dareId = 0;
            countEntrants = 0;
            countWinners = 0;
      }
public  void serializeAs_DareVersatileInformations ( BigEndianWriter writer ) {if(dareId < 0 || dareId > 9007199254740990)
         {
            throw new Exception("Forbidden value (" + dareId + ") on element dareId.");
         }
         param1.writeDouble(dareId);
         if(countEntrants < 0)
         {
            throw new Exception("Forbidden value (" + countEntrants + ") on element countEntrants.");
         }
         param1.writeInt(countEntrants);
         if(countWinners < 0)
         {
            throw new Exception("Forbidden value (" + countWinners + ") on element countWinners.");
         }
         param1.writeInt(countWinners);
      }
public  void deserializeAs_DareVersatileInformations ( BigEndianReader reader ) {this._dareIdFunc(param1);
            _countEntrantsFunc(param1);
         this._countWinnersFunc(param1);
      }
public  void deserializeAsyncAs_DareVersatileInformations ( FuncTree param1 ) {param1.addChild(this._dareIdFunc);
         param1.addChild(this._countEntrantsFunc);
         param1.addChild(this._countWinnersFunc);
      }
private  void _countEntrantsFunc ( BigEndianReader reader ) {
            countEntrants = param1.readInt();
         if(countEntrants < 0)
         {
            throw new Exception("Forbidden value (" + countEntrants + ") on element of DareVersatileInformations.countEntrants.");
         }
      }

}
}