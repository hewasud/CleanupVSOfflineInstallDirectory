using System.IO;
using System.Linq;
using VSOfflineInstallerPackageDirectoryStructure;

namespace CleanVSOfflineInstallDirectory
{
    class Program
    {
        static void Main(string[] argv)
        {
            if (argv.Length != 1)
            {
                System.Console.WriteLine(
                    "\nUsage: dotnet CleanupOfflineInstallDirectory c:\\offlineinstalldir\n\n");
                return;
            }
            System.Console.WriteLine($"\nCleanup: {argv[0]}\n\n");

            var VSOfflineInstallPath = argv[0];

            var dirNames = Directory.GetDirectories(VSOfflineInstallPath)
                .Where(s => DirectoryNameSturcture.IsValidPackageDir(s));

            var DNStruct = new PackageList(dirNames);

            foreach(var p in DNStruct.OldVersions) {
                System.Console.WriteLine($"Deleing:  \"{p.FullPath}\"");
                Directory.Delete(p.FullPath, true);
            }
        }
    }
}
