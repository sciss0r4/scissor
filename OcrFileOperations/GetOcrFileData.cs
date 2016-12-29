using DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcrFileOperations
{
    public class GetOcrFileData
    {
        public List<OcrFile> GetOcrFiles(TextDataManagerContext context, int[] ocrIds)
        {
            return context.OcrFiles
                .Join(ocrIds, ocr => ocr.Id, array => array, (t, a) => t)
                .ToList();
        }

        public string GetOcrPath(TextDataManagerContext context, int ocrId)
        {
            return context.OcrFiles
                .Where(o => o.Id == ocrId)
                .Select(o => o.Path)
                .First();
        }
    }
}
