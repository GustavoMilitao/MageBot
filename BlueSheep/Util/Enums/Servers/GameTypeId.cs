using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace BlueSheep.Util.Enums.Servers
{
    public enum GameTypeId
    {
        [Description("Normal")]
        Normal = 0,
        [Description("Heróico")]
        Heroico = 1,
        [Description("Teste")]
        Teste = 2,
        [Description("Torneios")]
        Torneios = 3,
        [Description("Épico")]
        Epico = 4
    }
}
