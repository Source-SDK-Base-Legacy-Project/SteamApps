using System.IO.Abstractions;
using ValveResourceFormat.IO;
namespace SteamApps;

internal class Program
{
    class SteamAppsOptions
    {
        public string OutputPath { get; set; } = string.Empty;
    }

    static int Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Too few arguments.");
            Console.Error.WriteLine("Syntax: -o \"<output path>\"");
            return 1;
        }

        if (args[0] != "-o")
        {
            Console.Error.WriteLine("Invalid argument(s).");
            Console.Error.WriteLine("Syntax: -o \"<output path>\"");
            return 1;
        }

        return RunSteamAppsAndReturnExitCode(new SteamAppsOptions()
        {
            OutputPath = args[1]
        });
    }

    static int RunSteamAppsAndReturnExitCode(SteamAppsOptions options)
    {
        List<(int, string)> SteamAppsIDsToINIKey = [
            (215, SteamAppsINIKeys.SDKBASE2006),
            (218, SteamAppsINIKeys.SDKBASE2007),
            (220, SteamAppsINIKeys.HL2),
            (240, SteamAppsINIKeys.CSTRIKE),
            (280, SteamAppsINIKeys.HL1),
            (300, SteamAppsINIKeys.DOD),
            (320, SteamAppsINIKeys.HL2MP),
            (360, SteamAppsINIKeys.HL1MP),
            (400, SteamAppsINIKeys.PORTAL),
            (243730, SteamAppsINIKeys.SDKBASE2013SP),
            (243750, SteamAppsINIKeys.SDKBASE2013MP),
        ];

        try
        {
            var steamAppsINI = new SteamAppsINI();
            foreach (var (appID, iniKey) in SteamAppsIDsToINIKey)
            {
                var steamAppInfo = GameFolderLocator.FindSteamGameByAppId(appID);
                if (steamAppInfo != null)
                    steamAppsINI[iniKey] = steamAppInfo.Value.GamePath;
            }

            Console.WriteLine($"Writing {options.OutputPath}");

            // Ensure the output directory exists.
            var dir = Path.GetDirectoryName(options.OutputPath);
            if (dir is null)
            {
                Console.WriteLine($"Failed to get output path directory info");
                return 1;
            }

            if (dir == string.Empty)
                dir = Environment.CurrentDirectory;

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            new SteamAppsWriter(new FileSystem()).Write(steamAppsINI, options.OutputPath);
            return 0;
        }
        catch (Exception e)
        {
            Console.Error.WriteLine(e.Message);
            return 1;
        }
    }
}
