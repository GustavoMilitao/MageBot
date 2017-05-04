using Microsoft.VisualBasic;
using System;

public class FlashKey
{
    public static string GetRandomFlashKey()
    {
        dynamic _loc_1 = "";
        dynamic _loc_2 = 17;
        dynamic _loc_3 = 0;
        while (_loc_3 < _loc_2)
        {
            _loc_1 += GetRandomChar();
            _loc_3 += 1;
        }
        return _loc_1 + Checksum(_loc_1) + "#01";
    }

    private static object Checksum(string toCheck)
    {
        char[] hex_chars = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};

        dynamic checkSumIndex = 0;
        dynamic cpt = 0;
        Array CharArray = default(Array);
        CharArray = toCheck.ToCharArray();
        while ((cpt < toCheck.Length))
        {
            checkSumIndex = checkSumIndex + Strings.Asc(CharArray.GetValue(cpt)) % 16;
            cpt = cpt + 1;
        }
        return hex_chars[checkSumIndex % 16];
    }

    private static object GetRandomChar()
    {
        int y = 0;
        int rnd = 0;
        Random test = new Random();
        while (!((y >= 48 & y <= 57) | (y >= 65 & y <= 90) | (y >= 97 & y <= 122)))
        {
            rnd = test.Next();
            y = (122 - 48 + 1) * rnd + 48;
        }
        return Strings.Chr(y);
    }
}
