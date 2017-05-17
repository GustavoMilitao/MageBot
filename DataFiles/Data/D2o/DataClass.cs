using System.Collections.Generic;

namespace MageBot.DataFiles.Data.D2o
{
    //[DefaultMember("Field")]      
    public class DataClass
    {
        // Properties
        public object this[string Name]
        {
            get
            {
                if (!Fields.ContainsKey(Name))
                {
                    return null;
                }
                return Fields[Name];
            }
        }

        // Fields
        public Dictionary<string, object> Fields = new Dictionary<string, object>();
        public string Name;
    }


}
