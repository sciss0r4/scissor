using DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileRepositoryOperations
{
    public class ModifyFileRepositoryData
    {
        public void SubtractSpaceLeft(TextDataManagerContext context, FileRepository repo, long space)
        {
            var dbRepo = context.FileRepositories.Where(x => x.Id == repo.Id).First();
            dbRepo.SpaceLeft -= space;
            context.SaveChanges();
        }
    }
}
