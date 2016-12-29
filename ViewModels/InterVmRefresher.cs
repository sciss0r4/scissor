using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class InterVmRefresher
    {
        private IndexManagementViewModel _indexManagementViewModel;
        public InterVmRefresher(IndexManagementViewModel indexManagementViewModel)
        {
            _indexManagementViewModel = indexManagementViewModel;
        }

        public void RefreshTiffViewTree()
        {
            _indexManagementViewModel.RefreshView();
        }
    }
}
