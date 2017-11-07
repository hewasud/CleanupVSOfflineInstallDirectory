using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace VSOfflineInstallerPackageDirectoryStructureTests
{
    [TestClass]
    public class VersionTests
    {
        [TestMethod]
        public void CreateVersionInfoWithVersionAsStringArguement()
        {
            Assert.IsNotNull(new Version(@"1.2.3.4"));
            Assert.IsNotNull(new Version(@"1.2.3"));
            Assert.IsNotNull(new Version(@"1.2"));
        }

        [TestMethod]
        public void CreateVersionWithInvalidStringShouldFail()
        {
            Assert.ThrowsException<ArgumentException>(() => new Version(@"InvalidVersion"));
            Assert.ThrowsException<ArgumentException>(() => new Version(@"1"));
            Assert.ThrowsException<ArgumentException>(() => new Version(@"1.2.3.4.5"));
        }
    }
}
