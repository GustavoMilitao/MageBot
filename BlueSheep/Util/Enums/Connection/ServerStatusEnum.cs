using System.ComponentModel;

namespace DofusBot.Enums
{
    public enum ServerStatusEnum
    {
        [Description("Desconhecido")]
        StatusUnknown,
        [Description("Offline")]
        Offline,
        [Description("Iniciando")]
        Starting,
        [Description("Online")]
        Online,
        [Description("Sem entrada")]
        NoJoin,
        [Description("Salvando")]
        Saving,
        [Description("Parando")]
        Stoping,
        [Description("Cheio")]
        Full       
    }
}
