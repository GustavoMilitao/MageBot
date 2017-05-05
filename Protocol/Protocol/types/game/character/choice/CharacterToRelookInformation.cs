


















// Generated on 12/11/2014 19:02:03
using BlueSheep.Common.IO;


namespace BlueSheep.Common.Protocol.Types
{

    public class CharacterToRelookInformation : AbstractCharacterToRefurbishInformation
{

public new const short ID = 399;
public override short TypeId
{
    get { return ID; }
}



public CharacterToRelookInformation()
{
}

public CharacterToRelookInformation(uint id, int[] colors, int cosmeticId)
         : base(id, colors, cosmeticId)
        {
        }
        

public override void Serialize(BigEndianWriter writer)
{

base.Serialize(writer);
            

}

public override void Deserialize(BigEndianReader reader)
{

base.Deserialize(reader);
            

}


}


}