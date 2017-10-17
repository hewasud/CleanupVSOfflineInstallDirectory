using System;
using System.Text.RegularExpressions;

namespace CleanVSOfflineInstallDirectory
{
    internal class DirProperty
    {
        public string Name { get; set; }
    }

    public class DirectoryNameSturcture
    {
        private static readonly string regExStr =
            @"(..*\\)([^,]+),version=([^,]+)(,chip=[^,]+)?(,language=[^,]+)?";
        private static readonly Regex regex = new Regex(regExStr);

        public static bool IsValidPackageDir(string name)
        {
            return regex.Match(name).Success;
        }

        public DirectoryNameSturcture(string dirName)
        {
            var match = regex.Match(dirName);

            if (! match.Success) throw new ArgumentException(match.ToString());

            var chipRegEx = new Regex(@",chip=(.+)");
            var languageRegEx = new Regex(@",language=(.+)");

            var chip = chipRegEx.Match(match.Groups[4].Value).Groups[1].Value;
            var lang = languageRegEx.Match(match.Groups[5].Value).Groups[1].Value;

            Version = new Version(match.Groups[3].Value);
            Chip = (chip.Equals("")) ? null : chip;
            Language = (lang.Equals("")) ? null : lang;
        }

        public string PackageName { get; set; }
        public Version Version { get; set; }
        public string Chip { get; set; }
        public string Language { get; set; }
    }
}
