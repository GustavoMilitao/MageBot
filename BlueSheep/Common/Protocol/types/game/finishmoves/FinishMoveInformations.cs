namespace  BlueSheep.Common.Protocol.Types {
public  class  FinishMoveInformations {
public  static  const  uint protocolId  = 506 ;
public    uint finishMoveId  = 0 ;
public    bool finishMoveState  = false ;

public  uint getTypeId (  ) {return 506;
      }
public  void reset (  ) {
            finishMoveId = 0;
            finishMoveState = false;
      }
public  void serializeAs_FinishMoveInformations ( BigEndianWriter writer ) {if(finishMoveId < 0)
         {
            throw new Exception("Forbidden value (" + finishMoveId + ") on element finishMoveId.");
         }
         param1.writeInt(finishMoveId);
         param1.writeBoolean(finishMoveState);
      }
public  void deserializeAs_FinishMoveInformations ( BigEndianReader reader ) {this._finishMoveIdFunc(param1);
            _finishMoveStateFunc(param1);
      }
public  void deserializeAsyncAs_FinishMoveInformations ( FuncTree param1 ) {param1.addChild(this._finishMoveIdFunc);
         param1.addChild(this._finishMoveStateFunc);
      }
private  void _finishMoveStateFunc ( BigEndianReader reader ) {
            finishMoveState = param1.readBoolean();
      }

}
}