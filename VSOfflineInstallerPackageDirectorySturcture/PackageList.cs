using System.Collections.Generic;
using System.Linq;

namespace VSOfflineInstallerPackageDirectoryStructure
{
    public class PackageList
    {
        private readonly IEnumerable<string> listOfDirNames;
        public readonly IEnumerable<IGrouping<
            (string PackageName, string Language, string Chip),
            DirectoryNameSturcture>> PackageListGrouped;

        public PackageList(IEnumerable<string> listOfDirNames)
        {
            this.listOfDirNames = listOfDirNames;
            PackageListGrouped = listOfDirNames
                .Where(n => DirectoryNameSturcture.IsValidPackageDir(n))
                .Select(d => new DirectoryNameSturcture(d))
                .GroupBy(g => (g.PackageName, g.Language, g.Chip));
        }

        public IEnumerable<DirectoryNameSturcture> LatestVersions
        {
            get => PackageListGrouped
                .Select(g => g.OrderByDescending(o => o.Version).Take(1))
                .SelectMany(e => e);
        }

        public IEnumerable<DirectoryNameSturcture> OldVersions
        {
            get => PackageListGrouped
                .Select(g => g.OrderByDescending(o => o.Version).Skip(1))
                .SelectMany(e => e);
        }

        public IEnumerable<DirectoryNameSturcture> FullList
        {
            get => PackageListGrouped
                .Select(g => g.OrderByDescending(o => o.Version))
                .SelectMany(e => e);
        }
    }
}
