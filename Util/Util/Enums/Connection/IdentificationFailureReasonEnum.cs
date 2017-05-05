namespace DofusBot.Enums
{
    public enum IdentificationFailureReasonEnum
    {
        BadVersion = 1,
        WrongCredentials = 2,
        Banned = 3,
        Kicked = 4,
        InMaintenance = 5,
        TooManyOnIP = 6,
        TimeOut = 7,
        BadIPRange = 8,
        CredentialsReset = 9,
        EmailUnvalidated = 10,
        OTPTimeOut = 11,
        Locked = 12,
        ServiceUnavailable = 53,
        ExternalAccountLinkRefused = 61,
        ExternalAccountAlteradyLinked = 62,
        UnknownAuthError = 99,
        Spare = 100,
    }
}
