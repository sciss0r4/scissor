using Controllers;
using Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels
{
    public class IndexManagementViewModel : INotifyPropertyChanged
    {
        private IFileRepositoryController _repoController;
        private ITiffFileController _tiffController;
        private IndexRepositoryController _indexRepoController;
        private bool _isBusy;

        public bool ProgressBarIndeterminate2
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                NotifyPropertyChanged("ProgressBarIndeterminate2");
            }
        }


        private ObservableCollection<OcrDocsTreeItem> _treeItems;
        public ObservableCollection<OcrDocsTreeItem> TreeItems
        {
           get
           {
                return _treeItems;
           }
           set
           {
                _treeItems = value;
                NotifyPropertyChanged("TreeItems");
           }
        }

        private ObservableCollection<IndexViewModel> _indexes;
        public ObservableCollection<IndexViewModel> Indexes
        {
            get
            {
                return _indexes;
            }

            set
            {
                _indexes = value;
                NotifyPropertyChanged("Indexes");
            }
        }
        public string IndexLabel { get; set; }
        private RelayCommand _createIndex;
        public ICommand CreateIndex
        {
            get
            {
                if (_createIndex == null)
                {
                    _createIndex = new RelayCommand(p => ExecuteCreateIndex(), p => CanExecuteCreateIndex());
                }

                return _createIndex;
            }
        }
        public IndexManagementViewModel()
        {
            _repoController = new FileRepositoryController();
            _tiffController = new TiffFileController();
            _indexRepoController = new IndexRepositoryController();
            RefreshView();
        }

        public void RefreshView()
        {
            PopulateTree();
            PopulateIndexes();
            NotifyPropertyChanged("Indexes");
            ProgressBarIndeterminate2 = false;
        }

        private bool CanExecuteCreateIndex()
        {
            return TreeItems.Any(x => x.Tiffs.Any(t => t.IsSelected)) && !String.IsNullOrWhiteSpace(IndexLabel);
        }

        private void ExecuteCreateIndex()
        {
            ProgressBarIndeterminate2 = true;
            var tiffIds = TreeItems.SelectMany(t => t.Tiffs)
                .Where(t => t.IsSelected)
                .Select(t => t.Id)
                .ToArray();

            try
            {
                _indexRepoController.CreateIndex(tiffIds, IndexLabel);
            }
            catch
            {
                // logging
            }

            RefreshView();
        }

        private void PopulateTree()
        {
            var treeItems = new ObservableCollection<OcrDocsTreeItem>();
            var repositories = ViewModelMapper.MapRepoListToViewModelCollection(_repoController.GetRepositories());

            foreach(var r in repositories)
            {
                var ocrDocsTreeItem = new OcrDocsTreeItem()
                {
                    Repository = r,
                    Tiffs = ViewModelMapper.MapTiffFileListToViewModelCollection(_tiffController.GetOcredTiffFiles(r.Id))
                };

                treeItems.Add(ocrDocsTreeItem);
            }

            TreeItems = treeItems;
        }

        private void PopulateIndexes()
        {
            var indexes = _indexRepoController.GetIndexes();
            Indexes = ViewModelMapper.MapIndexesListToViewModelCollection(indexes);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
