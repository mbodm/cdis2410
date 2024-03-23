using System.Reflection;

namespace ChangeDisplayInputSourceDellU2410
{
    internal static class Helper
    {
        private const string Tail = "(by MBODM 2024)";

        public static string AppName => "ChangeDisplayInputSource Dell U2410";
        public static string AppVersion => GetVersionFromProject();
        public static string AppTitle => $"{AppName} {AppVersion} {Tail}";
        public static string ExeName => $"{GetAssemblyNameFromProject().ToLower()}.exe";
        public static string ExeDescription => $"The {ExeName} is a tiny CLI tool, using DDC to get/set the input source of my Dell U2410 display.";

        private static string GetVersionFromProject()
        {
            // For Console Apps this seems to be the most simple way, in .NET 5 or later.
            // It's the counterpart of the "Version" entry, declared in the .csproj file.

            return Assembly.
                GetEntryAssembly()?.
                GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.
                InformationalVersion ?? "0.0.0";
        }

        private static string GetAssemblyNameFromProject()
        {
            // Not the project's name shall be used as exe/assembly file name, when published.
            // It's the counterpart of the "AssemblyName" entry, declared in the .csproj file.

            return Assembly.
                GetExecutingAssembly()?.
                GetName()?.
                Name ?? "UNKNOWN";
        }
    }
}