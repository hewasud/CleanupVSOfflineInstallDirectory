using System;
using System.Collections.Generic;
using System.IO;

namespace CleanVSOfflineInstallDirectory
{
    class Program
    {
        static void Main()
        {
            const string VSOfflineInstallPath = @"C:\Users\hewasud\Downloads\vs2017";
            IList<string> directories = Directory.GetDirectories(VSOfflineInstallPath);
            var DNStruct = new List<DirectoryNameSturcture>();
            foreach (var d in directories)
            {
                if(DirectoryNameSturcture.IsValidPackageDir(d))
                {
                    DNStruct.Add(new DirectoryNameSturcture(d));
                } else {
                    Console.WriteLine(">> NM >> "+d);
                }
            }
           Console.ReadLine();
        }
    }
}
