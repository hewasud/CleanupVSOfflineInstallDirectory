using System.IO;
using System.Linq;

namespace CleanVSOfflineInstallDirectory
{
    class Program
    {
        static void Main()
        {
            const string ArchiveDirectory = @"D:\archive";
            const string VSOfflineInstallPath = @"C:\vs2017";

            var dirNames = Directory.GetDirectories(VSOfflineInstallPath)
                .Where(s => DirectoryNameSturcture.IsValidPackageDir(s));

            var DNStruct = new PackageList(dirNames);

            if (! Directory.Exists(ArchiveDirectory)) {
                Directory.CreateDirectory(ArchiveDirectory);
            }

            foreach(var p in DNStruct.OldVersions)
            {
                System.Console.WriteLine($"move \"{p.FullPath}\" \"{ArchiveDirectory}\"\n");
                //Directory.Delete(p.FullPath, true);
                //Directory.Move(p.FullPath, ArchiveDirectory);
                // test change
            }
        }
    }
}
