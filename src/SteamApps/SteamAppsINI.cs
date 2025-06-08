namespace SteamApps;

public static class SteamAppsINIKeys
{
    public const string CSTRIKE = "cstrike";
    public const string DOD = "dod";
    public const string HL1 = "hl1";
    public const string HL1MP = "hl1mp";
    public const string HL2 = "hl2";
    public const string HL2MP = "hl2mp";
    public const string PORTAL = "portal";
    public const string SDKBASE2006 = "sdkbase2006";
    public const string SDKBASE2007 = "sdkbase2007";
    public const string SDKBASE2013MP = "sdkbase2013mp";
    public const string SDKBASE2013SP = "sdkbase2013sp";

    public static string[] AllKeys = [
        CSTRIKE,
        DOD,
        HL1,
        HL1MP,
        HL2,
        HL2MP,
        PORTAL,
        SDKBASE2006,
        SDKBASE2007,
        SDKBASE2013MP,
        SDKBASE2013SP,
    ];
}

public class SteamAppsINI : SortedDictionary<string, string>
{
    public SteamAppsINI()
    {
        foreach (var iniKey in SteamAppsINIKeys.AllKeys)
            Add(iniKey, string.Empty);
    }
}
