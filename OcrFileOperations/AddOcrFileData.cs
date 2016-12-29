using DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcrFileOperations
{
    public class AddOcrFileData
    {
        public void AddOcrFiles(TextDataManagerContext context, List<OcrFile> ocrs)
        {
            context.OcrFiles.AddRange(ocrs);
            context.SaveChanges();
        }
    }
}
