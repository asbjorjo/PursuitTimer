using System.IO;

namespace PursuitTimer.Helpers;

public static class FilePathHelper
{
    public static string GetFilePath(string filename)
    {
        return Path.Combine(FileSystem.Current.AppDataDirectory, filename);
    }
}
