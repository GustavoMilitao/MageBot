using MageBot.Util.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MageBot.DataFiles.Data.D2o
{
    internal sealed class D2oData
    {
        // Methods
        internal D2oData(string D2oFile)
        {
            D2oFileStream = new FileStream(D2oFile, FileMode.Open, FileAccess.Read);
            dictionary_1 = new Dictionary<int, int>();
            dictionary_0 = new Dictionary<int, Class16>();
            Reader = new BigEndianReader(D2oFileStream);
            Encoding.Default.GetString(Reader.ReadBytes(3));
            D2oFileStream.Position = Reader.ReadInt();
            int num = Reader.ReadInt();
            int i = 1;
            while ((i <= num))
            {
                dictionary_1.Add(Reader.ReadInt(), Reader.ReadInt());
                int_0 += 1;
                i = (i + 8);
            }
            int num3 = Reader.ReadInt();
            int j = 1;
            while ((j <= num3))
            {
                method_2(Reader.ReadInt());
                j += 1;
            }
        }

        internal DataClass DataObject(string File, int Id)
        {
            if (!Id_Data.ContainsKey(Id))
            {
                if (!dictionary_1.ContainsKey(Id))
                {
                    return null;
                }
                D2oFileStream.Position = Convert.ToInt64(dictionary_1[Id]);
                int key = Reader.ReadInt();
                if (dictionary_0.ContainsKey(key))
                {
                    Id_Data.Add(Id, dictionary_0[key].method_0(File, Reader));
                }
                else
                {
                    Id_Data.Add(Id, null);
                }
            }
            return Id_Data[Id];
        }

        internal DataClass[] DataObjects(string string_0)
        {
            List<DataClass> list = new List<DataClass>();
            int num = 0;
            foreach (int num_loopVariable in dictionary_1.Keys)
            {
                num = num_loopVariable;
                D2oFileStream.Position = Convert.ToInt64(dictionary_1[num]);
                int key = Reader.ReadInt();
                if (dictionary_0.ContainsKey(key))
                {
                    list.Add(dictionary_0[key].method_0(string_0, Reader));
                }
            }
            return list.ToArray();
        }

        private void method_2(int int_1)
        {
            Class16 class2 = new Class16(Reader.ReadUTF(), Reader.ReadUTF());
            int num2 = Reader.ReadInt();
            int i = 1;
            while ((i <= num2))
            {
                class2.method_1(Reader.ReadUTF(), Reader);
                i += 1;
            }
            dictionary_0.Add(int_1, class2);
        }


        // Fields
        internal Dictionary<int, Class16> dictionary_0;
        private Dictionary<int, int> dictionary_1;
        private Dictionary<int, DataClass> Id_Data = new Dictionary<int, DataClass>();
        private BigEndianReader Reader;
        private FileStream D2oFileStream;
        private int int_0 = 0;
    }
}
