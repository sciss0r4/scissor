using DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexRepositoryOperations
{
    public class GetIndexData
    {
        public List<Index> GetAllIndexes(TextDataManagerContext context)
        {
            return context.Indexes.ToList();
        }

        public Index GetIndex(TextDataManagerContext context, int indexId)
        {
            return context.Indexes.Where(i => i.Id == indexId).First();
        }

        public IndexDocument GetIndexDocument(TextDataManagerContext context, int indexId, int indexDocumentId)
        {
            return context.IndexDocuments.Where(i => i.IndexId == indexId && i.LuceneIndexId == indexDocumentId).First();
        }
    }
}
