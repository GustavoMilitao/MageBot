namespace  BlueSheep.Common.Protocol.Types {
public  class  TaxCollectorMovement {
public  static  const  uint protocolId  = 493 ;
public    uint movementType  = 0 ;
public    TaxCollectorBasicInformations basicInfos  ;
public    double playerId  = 0 ;
public    String playerName  = "" ;
private    FuncTree _basicInfostree  ;

public  uint getTypeId (  ) {return 493;
      }
public  void reset (  ) {
            movementType = 0;
            basicInfos = new TaxCollectorBasicInformations();
            playerName = "";
      }
public  void serializeAs_TaxCollectorMovement ( BigEndianWriter writer ) {param1.writeByte(movementType);
            basicInfos.serializeAs_TaxCollectorBasicInformations(param1);
         if(playerId < 0 || playerId > 9007199254740990)
         {
            throw new Exception("Forbidden value (" + playerId + ") on element playerId.");
         }
         param1.writeVarLong(playerId);
         param1.writeUTF(playerName);
      }
public  void deserializeAs_TaxCollectorMovement ( BigEndianReader reader ) {this._movementTypeFunc(param1);
            basicInfos = new TaxCollectorBasicInformations();
            basicInfos.Deserialize(param1);
         this._playerIdFunc(param1);
            _playerNameFunc(param1);
      }
public  void deserializeAsyncAs_TaxCollectorMovement ( FuncTree param1 ) {param1.addChild(this._movementTypeFunc);
            _basicInfostree = param1.addChild(this._basicInfostreeFunc);
         param1.addChild(this._playerIdFunc);
         param1.addChild(this._playerNameFunc);
      }
private  void _basicInfostreeFunc ( BigEndianReader reader ) {
            basicInfos = new TaxCollectorBasicInformations();
            basicInfos.deserializeAsync(_basicInfostree);
      }
private  void _playerNameFunc ( BigEndianReader reader ) {
            playerName = param1.readUTF();
      }

}
}