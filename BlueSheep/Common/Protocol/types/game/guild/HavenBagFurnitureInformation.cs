namespace  BlueSheep.Common.Protocol.Types {
public  class  HavenBagFurnitureInformation {
public  static  const  uint protocolId  = 498 ;
public    uint cellId  = 0 ;
public    int funitureId  = 0 ;
public    uint orientation  = 0 ;

public  uint getTypeId (  ) {return 498;
      }
public  void reset (  ) {
            cellId = 0;
            funitureId = 0;
            orientation = 0;
      }
public  void serializeAs_HavenBagFurnitureInformation ( BigEndianWriter writer ) {if(cellId < 0)
         {
            throw new Exception("Forbidden value (" + cellId + ") on element cellId.");
         }
         param1.writeVarShort(cellId);
         param1.writeInt(funitureId);
         if(orientation < 0)
         {
            throw new Exception("Forbidden value (" + orientation + ") on element orientation.");
         }
         param1.writeByte(orientation);
      }
public  void deserializeAs_HavenBagFurnitureInformation ( BigEndianReader reader ) {this._cellIdFunc(param1);
            _funitureIdFunc(param1);
         this._orientationFunc(param1);
      }
public  void deserializeAsyncAs_HavenBagFurnitureInformation ( FuncTree param1 ) {param1.addChild(this._cellIdFunc);
         param1.addChild(this._funitureIdFunc);
         param1.addChild(this._orientationFunc);
      }
private  void _funitureIdFunc ( BigEndianReader reader ) {
            funitureId = param1.readInt();
      }

}
}