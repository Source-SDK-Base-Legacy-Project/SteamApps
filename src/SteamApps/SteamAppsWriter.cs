using System.IO.Abstractions;
using System.Text;

namespace SteamApps;

public interface ISteamAppsWriter
{
    void Write(SteamAppsINI steamAppsINI, string outputPath);
}

public class SteamAppsWriter(IFileSystem fileSystem) : ISteamAppsWriter
{
    public void Write(SteamAppsINI steamAppsINI, string outputPath)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("[steamapps]");
        foreach (var (iniKey, installDir) in steamAppsINI)
            sb.AppendLine($"{iniKey}={FixPath(installDir)}");

        fileSystem.File.WriteAllText(outputPath, sb.ToString().TrimEnd('\n', '\r', '\t'));
    }
    static string FixPath(string path)
    {
        return path.TrimEnd('\\')
                .TrimEnd('/');
    }
}
