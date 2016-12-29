using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class ZipHelpers
    {
        public static bool ZipContainsGivenExtensionsOnly (ZipArchive zip, IEnumerable<string> extensions)
        {
            return zip.Entries.All(ent => extensions.Any(ex => ex.Equals(Path.GetExtension(ent.FullName),StringComparison.InvariantCultureIgnoreCase)));
        }

        public static long EstimateZipSize(ZipArchive zip, FileInfo zipInfo)
        {
            long size = 0;

            size += zipInfo.Length;
            zip.Entries.Select(e => size += e.Length);
            double origSize = size;
            double handicapSize = size * 1.1;

            return (long)handicapSize;
        }

        public static void CopyExtractRemoveZipFile(string srcZipPath, string destZipPath)
        {
            File.Copy(srcZipPath, destZipPath);
            ZipFile.ExtractToDirectory(destZipPath, Path.GetDirectoryName(destZipPath));
            File.Delete(destZipPath);
        }
    }
}
