using MageBot.Util.IO;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MageBot.DataFiles.Data.D2o
{
    internal sealed class Class16
    {
        // Methods
        internal Class16(string string_1, string string_2)
        {
            string_0 = string_1;
        }

        internal DataClass method_0(string string_1, BigEndianReader dofusReader_0)
	{
		DataClass class2 = new DataClass { Name = string_0 };
		//Class17 class3 = default(Class17);
		foreach (Class17 class3 in list_0) {
			class2.Fields.Add(class3.string_0, RuntimeHelpers.GetObjectValue(class3.delegate0_0.Invoke(string_1, dofusReader_0)));
		}
		return class2;
	}

        internal void method_1(string string_1, BigEndianReader dofusReader_0)
        {
            Class17 item = new Class17(string_1, dofusReader_0);
            list_0.Add(item);
        }


        // Fields
        private List<Class17> list_0 = new List<Class17>();
        private string string_0;
    }
}
