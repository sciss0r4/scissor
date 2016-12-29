using DatabaseModels;
using Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    internal static class FolderStructuresCreator
    {
        internal static DirectoryInfo CreateDefinedFolderStructure(string repoPath, string prefix, int begSuffix)
        {
            string folder = String.Empty;

            do
            {
                folder = Path.Combine(repoPath, String.Format("{0}{1}", prefix, begSuffix));
                ++begSuffix;
            } while (Directory.Exists(folder));

            Directory.CreateDirectory(folder);

            return new DirectoryInfo(folder);
        }

        internal static string GetZipPathForExistingFolderStructure(DirectoryInfo folderStructure)
        {
            return Path.Combine(folderStructure.FullName, Constants.ExtractionFileName);
        }
        
        internal static List<TiffFile> RefolderTiffsToRootRepoPath(FileRepository repo, List<TiffFile> tiffs)
        {
            foreach(var tiff in tiffs)
            {
                tiff.FileRepositoryId = repo.Id;
                tiff.Path = RefolderTiffToRepository(repo, tiff);
            }

            return tiffs;
        }    

        private static string RefolderTiffToRepository(FileRepository repo, TiffFile tiff)
        {
            var extension = Path.GetExtension(tiff.OriginalFileName);

            var destPath = Path.Combine(repo.Path, tiff.Label + extension);
            File.Move(tiff.Path, destPath);

            return destPath;
        }
    }
}
