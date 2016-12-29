using DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexRepositoryOperations
{
    public class AddIndexData
    {
        public void AddIndex(TextDataManagerContext context,Index indx)
        {
            context.Indexes.Add(indx);

            context.SaveChanges();
        }

        public void AddIndexDocument(TextDataManagerContext context, IndexDocument document)
        {
            context.IndexDocuments.Add(document);

            context.SaveChanges();
        }
    }
}
