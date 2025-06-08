using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO.Abstractions.TestingHelpers;

namespace SteamApps.Tests;

[TestClass]
public class TestSteamAppsWriter
{
    [TestMethod]
    public void TestDefault()
    {
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData> { });

        SteamAppsINI steamAppsINI = new();
        var steamAppsWriter = new SteamAppsWriter(fileSystem);
        steamAppsWriter.Write(steamAppsINI, "C:/steamapps.ini");

        Assert.AreEqual(File.ReadAllText("data/default.ini"), fileSystem.File.ReadAllText("C:/steamapps.ini"));
    }

    [TestMethod]
    [DataRow(SteamAppsINIKeys.CSTRIKE, @"C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Source", "data/cstrike_only.ini")]
    [DataRow(SteamAppsINIKeys.DOD, @"C:\Program Files (x86)\Steam\steamapps\common\Day of Defeat Source", "data/dod_only.ini")]
    [DataRow(SteamAppsINIKeys.HL1, @"C:\Program Files (x86)\Steam\steamapps\common\Half-Life 2", "data/hl1_only.ini")]
    [DataRow(SteamAppsINIKeys.HL1MP, @"C:\Program Files (x86)\Steam\steamapps\common\Half-Life 1 Source Deathmatch", "data/hl1mp_only.ini")]
    [DataRow(SteamAppsINIKeys.HL2, @"C:\Program Files (x86)\Steam\steamapps\common\Half-Life 2", "data/hl2_only.ini")]
    [DataRow(SteamAppsINIKeys.HL2MP, @"C:\Program Files (x86)\Steam\steamapps\common\Half-Life 2 Deathmatch", "data/hl2mp_only.ini")]
    [DataRow(SteamAppsINIKeys.PORTAL, @"C:\Program Files (x86)\Steam\steamapps\common\Portal", "data/portal_only.ini")]
    [DataRow(SteamAppsINIKeys.SDKBASE2006, @"C:\Program Files (x86)\Steam\steamapps\common\Source SDK Base", "data/sdkbase2006_only.ini")]
    [DataRow(SteamAppsINIKeys.SDKBASE2007, @"C:\Program Files (x86)\Steam\steamapps\common\Source SDK Base 2007", "data/sdkbase2007_only.ini")]
    [DataRow(SteamAppsINIKeys.SDKBASE2013MP, @"C:\Program Files (x86)\Steam\steamapps\common\Source SDK Base 2013 Multiplayer", "data/sdkbase2013mp_only.ini")]
    [DataRow(SteamAppsINIKeys.SDKBASE2013SP, @"C:\Program Files (x86)\Steam\steamapps\common\Source SDK Base 2013 Singleplayer", "data/sdkbase2013sp_only.ini")]
    public void TestSingleGameOnly(string iniKey, string iniValue, string expectedINIFile)
    {
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData> { });

        SteamAppsINI steamAppsINI = new();
        steamAppsINI[iniKey] = iniValue;

        var steamAppsWriter = new SteamAppsWriter(fileSystem);
        steamAppsWriter.Write(steamAppsINI, "C:/steamapps.ini");

        Assert.AreEqual(File.ReadAllText(expectedINIFile), fileSystem.File.ReadAllText("C:/steamapps.ini"));
    }

    [TestMethod]
    public void TestAll()
    {
        var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData> { });

        SteamAppsINI steamAppsINI = new();
        steamAppsINI[SteamAppsINIKeys.CSTRIKE] = @"C:\Program Files (x86)\Steam\steamapps\common\Counter-Strike Source";
        steamAppsINI[SteamAppsINIKeys.DOD] = @"C:\Program Files (x86)\Steam\steamapps\common\Day of Defeat Source";
        steamAppsINI[SteamAppsINIKeys.HL1] = @"C:\Program Files (x86)\Steam\steamapps\common\Half-Life 2";
        steamAppsINI[SteamAppsINIKeys.HL1MP] = @"C:\Program Files (x86)\Steam\steamapps\common\Half-Life 1 Source Deathmatch";
        steamAppsINI[SteamAppsINIKeys.HL2] = @"C:\Program Files (x86)\Steam\steamapps\common\Half-Life 2";
        steamAppsINI[SteamAppsINIKeys.HL2MP] = @"C:\Program Files (x86)\Steam\steamapps\common\Half-Life 2 Deathmatch";
        steamAppsINI[SteamAppsINIKeys.PORTAL] = @"C:\Program Files (x86)\Steam\steamapps\common\Portal";
        steamAppsINI[SteamAppsINIKeys.SDKBASE2006] = @"C:\Program Files (x86)\Steam\steamapps\common\Source SDK Base";
        steamAppsINI[SteamAppsINIKeys.SDKBASE2007] = @"C:\Program Files (x86)\Steam\steamapps\common\Source SDK Base 2007";
        steamAppsINI[SteamAppsINIKeys.SDKBASE2013MP] = @"C:\Program Files (x86)\Steam\steamapps\common\Source SDK Base 2013 Multiplayer";
        steamAppsINI[SteamAppsINIKeys.SDKBASE2013SP] = @"C:\Program Files (x86)\Steam\steamapps\common\Source SDK Base 2013 Singleplayer";

        var steamAppsWriter = new SteamAppsWriter(fileSystem);
        steamAppsWriter.Write(steamAppsINI, "C:/steamapps.ini");

        Assert.AreEqual(File.ReadAllText("data/all.ini"), fileSystem.File.ReadAllText("C:/steamapps.ini"));
    }
}