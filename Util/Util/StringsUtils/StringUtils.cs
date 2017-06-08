using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Util.StringsUtils
{
    public class StringUtils
    {
        public static string removeSpecialChars(string str)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                str = str.Replace(c.ToString(), "");
            }
            return str;
        }
    }
}
