using DatabaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileRepositoryOperations
{
    public class GetFileRepositoryData
    {
        public FileRepository GetRepository(TextDataManagerContext context, int Id)
        {
            return context.FileRepositories.Single(x => x.Id == Id);
        }

        public List<FileRepository> GetRepositories(TextDataManagerContext context)
        {
            return context.FileRepositories.ToList();
        }

        public FileRepository GetRepositoryWithEnoughSpace(TextDataManagerContext context, long space)
        {
            var repos = context.FileRepositories;
            var actualRepo = repos.Where(r => r.SpaceLeft >= space).
                        OrderBy(r => r.Id).
                        FirstOrDefault();

            if(actualRepo == null)
            {
                throw new ApplicationException("There's no repository with enough space. Please contact with IT Storage.");
            }

            return actualRepo;
        }
    }
}
