using System.IO;
using System.Linq;
using VSOfflineInstallerPackageDirectoryStructure;

namespace CleanVSOfflineInstallDirectory
{
    class Program
    {
        static void Main(string[] argv)
        {
            var VSOfflineInstallPath = argv[1];

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
