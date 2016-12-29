using DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiffFileOperations
{
    public class AddTiffFileData
    {
        public void AddTiffFiles(TextDataManagerContext context, List<TiffFile> tiffs)
        {
            context.TiffFiles.AddRange(tiffs);
            context.SaveChanges();
        }

        public void ConnectTiffsWithOcrs(TextDataManagerContext context, Dictionary<TiffFile,OcrFile> tiffsAndOcrs)
        {
            foreach(var e in tiffsAndOcrs)
            {
                context.TiffFiles.Where(t => t.Id == e.Key.Id).First().OcrId = e.Value.Id;
            }

            context.SaveChanges();
        }
    }
}
