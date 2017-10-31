using CleanVSOfflineInstallDirectory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CleanVSOfflineInstallDirectoryTests
{
    [TestClass]
    public class DirectoryNameStructureTests
    {
        private readonly IEnumerable<string> directoryNames
            = File.ReadLines(@"dirs.txt");

        [TestMethod]
        public void ParseDirectoryNamesTest()
        {
            foreach (var f in directoryNames)
            {
                if (DirectoryNameSturcture.IsValidPackageDir(f)) {
                    var d = new DirectoryNameSturcture(f);
                    Assert.IsNotNull(d);
                    Assert.IsNotNull(d.FullPath);
                    Assert.IsNotNull(d.PackageName);
                    Assert.IsNotNull(d.Version);
                } else {
                    System.Console.WriteLine("Not a valid dir: " + f);
                    Assert.ThrowsException<ArgumentException>(
                        () => new DirectoryNameSturcture(f));
                }
            }
        }

        [TestMethod]
        public void TestFindAllPackagesWithMultipleVersionsAndListLatestVersion()
        {
            var dnsList = new PackageList(directoryNames);

            //foreach(var g in dnsList)
            //{
            //    Console.WriteLine(g.Key);

            //    foreach(var v in g)
            //    {
            //        Console.WriteLine("{0} {1} {2} {3}",
            //          v.PackageName, v.Version, v.Language??"", v.Chip??"");
            //    }
            //}

            Console.WriteLine($"Count: {dnsList.FullList.Count()}");

            Console.WriteLine($"Keep Count: {dnsList.LatestVersions.Count()}");

            Console.WriteLine($"Delete Count: {dnsList.OldVersions.Count()}");

            foreach (var d in dnsList.OldVersions)
            {
                Console.WriteLine(d.ParentPath + d.PackageName  + " " + d.Version + " " + d.Chip);
            }
        }
    }
}
