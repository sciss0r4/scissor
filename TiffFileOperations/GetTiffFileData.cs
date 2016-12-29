using DatabaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiffFileOperations
{
    public class GetTiffFileData
    {
        public TiffFile GetLastTiffInRepository(TextDataManagerContext context, FileRepository repo)
        {
            return context.TiffFiles.Where(x => x.FileRepositoryId == repo.Id)
                .OrderByDescending(x => x.Id)
                .FirstOrDefault();
        }

        public List<TiffFile> GetTiffFiles(TextDataManagerContext context ,FileRepository repo)
        {
            return context.TiffFiles
                .Where(t => t.FileRepositoryId == repo.Id)
                .ToList();
        }

        public List<TiffFile> GetOcredTiffFiles(TextDataManagerContext context, FileRepository repo)
        {
            return context.TiffFiles
                .Where(t => t.FileRepositoryId == repo.Id && t.OcrId != null)
                .ToList();
        }

        public List<TiffFile> GetTiffFiles(TextDataManagerContext context, int[] tiffIds)
        {
            return context.TiffFiles
                .Join(tiffIds, tiff => tiff.Id, array => array, (t,a) => t)
                .ToList();
        }

        public TiffFile GetTiffFile(TextDataManagerContext context, int ocrId)
        {
            return context.TiffFiles.Where(t => t.OcrId == ocrId).FirstOrDefault();
        }
    }
}
