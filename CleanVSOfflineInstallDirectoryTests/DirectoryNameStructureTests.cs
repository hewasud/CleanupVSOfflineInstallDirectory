using CleanVSOfflineInstallDirectory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

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
                    Assert.IsNotNull(new DirectoryNameSturcture(f));
                } else {
                    System.Console.WriteLine("Not a valid dir: " + f);
                }
            }
        }
    }
}
