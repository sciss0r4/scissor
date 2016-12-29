using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class FileHelpers
    {
        public static void RemoveDirectory(DirectoryInfo dir)
        {
            foreach (FileInfo file in dir.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                d.Delete(true);
            }

            Directory.Delete(dir.FullName);
        }

        public static List<string> GetFiles(string[] fileExtensions, string directoryPath)
        {
            List<string> tiffFiles = new List<string>();
            string[] fixedFileExtensions = FixFileExtensions(fileExtensions);

            foreach (var ext in fixedFileExtensions)
            {
                tiffFiles.AddRange(Directory.GetFiles(directoryPath, ext, SearchOption.AllDirectories));
            }

            return tiffFiles;
        }

        private static string [] FixFileExtensions(string[] extensions)
        {
            string[] fixedFileExtensions = new string[extensions.Length];

            for(int i = 0;i < extensions.Length;++i)
            {
                fixedFileExtensions[i] = extensions[i];

                if(!fixedFileExtensions[i].StartsWith("."))
                {
                    fixedFileExtensions[i] = "." + fixedFileExtensions[i];
                }

                fixedFileExtensions[i] = "*" + fixedFileExtensions[i];
            }

            return fixedFileExtensions;
        }
    }
}
