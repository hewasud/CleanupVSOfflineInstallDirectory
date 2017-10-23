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
            var dnsList = directoryNames
                .Where(d => DirectoryNameSturcture.IsValidPackageDir(d))
                .Select(d => new DirectoryNameSturcture(d))
                .GroupBy(g => (g.PackageName, g.Language, g.Chip));

            //foreach(var g in dnsList)
            //{
            //    Console.WriteLine(g.Key);

            //    foreach(var v in g)
            //    {
            //        Console.WriteLine("{0} {1} {2} {3}",
            //          v.PackageName, v.Version, v.Language??"", v.Chip??"");
            //    }
            //}

            var dnsList2 = dnsList
                .Select(g => g.OrderByDescending(o => o.Version))
                .SelectMany(e => e);

            Console.WriteLine($"Count: {dnsList2.Count()}");

            dnsList2 = dnsList
                .Select(g => g.OrderByDescending(o => o.Version).Take(1))
                .SelectMany(e => e);

            Console.WriteLine($"Count: {dnsList2.Count()}");

            dnsList2 = dnsList
                .Select(g => g.OrderByDescending(o => o.Version).Skip(1))
                .SelectMany(e => e);

            Console.WriteLine($"Count: {dnsList2.Count()}");

            foreach (var d in dnsList2)
            {
                Console.WriteLine(d.FullPath);
            }
        }
    }
}
