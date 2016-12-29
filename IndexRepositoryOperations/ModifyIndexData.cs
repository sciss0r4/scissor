using DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexRepositoryOperations
{
    public class ModifyIndexData
    {
        public void UpdateLastIndexId(TextDataManagerContext context, int indxId, int newLastIndexId)
        {
            var index = context.Indexes
                .Where(i => i.Id == indxId)
                .First();

            index.LastLuceneIndexId = newLastIndexId;

            context.SaveChanges();
        }
    }
}
