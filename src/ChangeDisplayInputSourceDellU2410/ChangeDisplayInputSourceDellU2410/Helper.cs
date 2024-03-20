using System.Reflection;

namespace ChangeDisplayInputSourceDellU2410
{
    internal static class Helper
    {
        private const string Tail = "[cdis2410.exe] (by MBODM 2024)";

        public static string ExeName = "cdis2410.exe";

        public static string AppName = "ChangeDisplayInputSource Dell U2410";

        // For Console Apps this seems to be the most simple way, in .NET 5 or later.
        // It's the counterpart of the "Version" entry, declared in the .csproj file.
        public static string AppVersion => Assembly.
            GetEntryAssembly()?.
            GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.
            InformationalVersion ?? string.Empty;

        public static string AppTitle => AppVersion == string.Empty ? $"{AppName} {Tail}" : $"{AppName} {AppVersion} {Tail}";
    }
}