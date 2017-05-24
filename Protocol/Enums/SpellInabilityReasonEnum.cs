using System.ComponentModel;

namespace MageBot.Util.Enums.Fight
{
    public enum SpellInabilityReason
    {
        /// <summary>Les points d'action sont insuffisants</summary>
        [Description("Action points")]
        ActionPoints = 3,
        /// <summary>Le temps de recharge du sort est actif</summary>
        [Description("Cooldown")]
        Cooldown = 2,
        /// <summary>L'état n'est pas autorisé</summary>
        [Description("Forbidden state")]
        ForbiddenState = 13,
        /// <summary>La ligne de vue n'est pas dégagée</summary>
        [Description("Line of sight")]
        LineOfSight = 8,
        /// <summary>La distance maximum du sort a été dépassée</summary>
        [Description("Max Range")]
        MaxRange = 5,
        /// <summary>La distance minimum du sort a été dépassée</summary>
        [Description("Min Range")]
        MinRange = 6,
        /// <summary>La cellule doit être vide</summary>
        [Description("Need free cell")]
        NeedFreeCell = 10,
        /// <summary>La cellule doit être prise</summary>
        [Description("Need taken cell")]
        NeedTakenCell = 11,
        /// <summary>Le sort peut être lancé</summary>
        [Description("None")]
        None = 0,
        /// <summary>Le sort n'est pas lancé en ligne alors qu'il le devrait</summary>
        [Description("Not in line")]
        NotInLine = 7,
        /// <summary>L'état requis est manquant</summary>
        [Description("Required state")]
        RequiredState = 12,
        /// <summary>Le nombre d'invoquation est dépassé</summary>
        [Description("Too many invocations")]
        TooManyInvocations = 9,
        /// <summary>Le sort a été lancé trop de fois ce tour</summary>
        [Description("Too many launch")]
        TooManyLaunch = 1,
        /// <summary>Le sort a été lancé trop de fois sur cette cellule</summary>
        [Description("Too many launch on cell")]
        TooManyLaunchOnCell = 4,
        /// <summary>Le sort ne peut pas être lancé pour une raison inconnue</summary>
        [Description("Unknown")]
        Unknown = 16,
        /// <summary>Le personnage ne possède pas le sort indiqué</summary>
        [Description("Unknown spell")]
        UnknownSpell = 15,
        /// <summary>Le sort n'est pas lancé en diagonale alors qu'il le devrait</summary>
        [Description("Not in diagonal")]
        NotInDiagonal = 14

    }
}
