using DatabaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public interface IFileRepositoryController
    {
        List<FileRepository> GetRepositories();
        void LoadDataToRepository(string zipFilePath, int repositoryId);
    }
}
