using System;
using System.Configuration;

namespace BlueSheep.Engine.Constants
{
    static class GameConstants
    {
        #region Fields
        public static sbyte Major = Convert.ToSByte(ConfigurationManager.AppSettings.Get("Major"));
        public static sbyte Minor = Convert.ToSByte(ConfigurationManager.AppSettings.Get("Minor"));
        public static sbyte Release = Convert.ToSByte(ConfigurationManager.AppSettings.Get("Release"));
        public static int Revision = Convert.ToInt32(ConfigurationManager.AppSettings.Get("Revision"));
        public static sbyte Patch = Convert.ToSByte(ConfigurationManager.AppSettings.Get("Patch"));
        public static sbyte BuildType = Convert.ToSByte(ConfigurationManager.AppSettings.Get("BuildType"));
        public static sbyte Install = Convert.ToSByte(ConfigurationManager.AppSettings.Get("Install"));
        public static sbyte Technology = Convert.ToSByte(ConfigurationManager.AppSettings.Get("Technology"));
        public static string Lang = ConfigurationManager.AppSettings.Get("Lang");
        public static short ServerID = Convert.ToInt16(ConfigurationManager.AppSettings.Get("ServerID"));
        public static bool AutoConnect = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("AutoConnect"));
        public static bool UseCertificate = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("UseCertificate"));
        public static bool UseLoginToken = Convert.ToBoolean(ConfigurationManager.AppSettings.Get("UseLoginToken"));
        public static long SessionOptionalSalt = Convert.ToInt64(ConfigurationManager.AppSettings.Get("ServerID"));
        #endregion
    }
}
