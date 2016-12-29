using Controllers;
using DatabaseModels;
using Microsoft.Win32;
using Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModels;

namespace TextDataManager
{
    class MainWindowViewModel
    {
        ObservableCollection<object> _children;

        public MainWindowViewModel()
        {
            var indxVm = new IndexManagementViewModel();
            var refresher = new InterVmRefresher(indxVm);
            var fileVm = new FileManagementViewModel(refresher);
            
            _children = new ObservableCollection<object>();
            _children.Add(fileVm);
            _children.Add(new IndexManagementViewModel());
            _children.Add(new SearchEngineViewModel());
        }

        public ObservableCollection<object> Children { get { return _children; } }
    }
}
