using DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexRepositoryOperations
{
    public class GetIndexRepositoryData
    {
        public IndexRepository GetTopIndexRepository(TextDataManagerContext context)
        {
            return context.IndexRepositories.First();
        }
    }
}
