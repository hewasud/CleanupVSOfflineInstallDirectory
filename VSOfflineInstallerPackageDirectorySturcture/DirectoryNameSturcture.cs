using System;
using System.Text.RegularExpressions;

namespace VSOfflineInstallerPackageDirectoryStructure
{
    internal class DirProperty
    {
        public string Name { get; set; }
    }

    public class DirectoryNameSturcture
    {
        // Regular Expressions
        private static readonly string FullPathMatchStr =
            @"(..*\\)([^,]+),version=([^,]+)(,chip=[^,]+)?(,language=[^,]+)?";
        private static readonly string ChipMatchStr = @",chip=(.+)";
        private static readonly string LanguageMatchStr = @",language=(.+)";

        private static readonly Regex FullPathMatchRegEx = new Regex(FullPathMatchStr);
        private static readonly Regex ChipMatchRegEx = new Regex(ChipMatchStr);
        private static readonly Regex LanguageMatchRegEx = new Regex(LanguageMatchStr);

        public static bool IsValidPackageDir(string name)
        {
            var match = FullPathMatchRegEx.Match(name);
            return match.Success;
        }

        public DirectoryNameSturcture(string dirName)
        {
            var match = FullPathMatchRegEx.Match(dirName);

            if (! match.Success) throw new ArgumentException(match.ToString());

            var chip = ChipMatchRegEx.Match(match.Groups[4].Value).Groups[1].Value;
            var lang = LanguageMatchRegEx.Match(match.Groups[5].Value).Groups[1].Value;

            Version = new Version(match.Groups[3].Value);
            Chip = (chip.Equals("")) ? null : chip;
            Language = (lang.Equals("")) ? null : lang;
            FullPath = match.Groups[0].Value;
            ParentPath = match.Groups[1].Value;
            PackageName = match.Groups[2].Value;
        }

        public string PackageName { get; }
        public Version Version { get; }
        public string Chip { get; }
        public string Language { get; }
        public string FullPath { get; }
        public string ParentPath { get; }
    }
}
