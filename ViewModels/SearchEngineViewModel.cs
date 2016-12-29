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
    public class SearchEngineViewModel : INotifyPropertyChanged
    {
        private IndexRepositoryController _indexRepoController;
        private IndexSearchController _indexSearchController;
        private bool _isBusy;

        public bool ProgressBarIndeterminate3
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                NotifyPropertyChanged("ProgressBarIndeterminate3");
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

        private ObservableCollection<QueryResultViewModel> _queryResults;
        public ObservableCollection<QueryResultViewModel> QueryResults
        {
            get
            {
                return _queryResults;
            }

            set
            {
                _queryResults = value;
                NotifyPropertyChanged("QueryResults");
            }
        }

        private IndexViewModel _selectedIndex;
        public IndexViewModel SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }

            set
            {
                _selectedIndex = value;
                NotifyPropertyChanged("SelectedIndex");
            }
        }

        private string _query;
        public string Query
        {
            get
            {
                return _query;
            }
            set
            {
                _query = value;
                NotifyPropertyChanged("Query");
            }
        }

        private RelayCommand _executeQuery;
        public ICommand ExecuteQuery
        {
            get
            {
                if (_executeQuery == null)
                {
                    _executeQuery = new RelayCommand(p => ExecuteExecuteQuery(), p => CanExecuteQuery());
                }

                return _executeQuery;
            }
        }

        public void ExecuteExecuteQuery()
        {
            ProgressBarIndeterminate3 = true;
            Dictionary<string, int> docs = new Dictionary<string, int>();

            try
            {
                docs = _indexSearchController.RunQuery(SelectedIndex.Id, Query);
            }
            catch
            {
                // logging
            }

            QueryResults = ViewModelMapper.MapDictionaryResultsToViewModelCollection(docs);
            ProgressBarIndeterminate3 = false;
        }

        public bool CanExecuteQuery()
        {
            return !String.IsNullOrWhiteSpace(Query) && SelectedIndex != null;
        }

        public SearchEngineViewModel()
        {
            _indexRepoController = new IndexRepositoryController();
            _indexSearchController = new IndexSearchController();
            PopulateIndexes();
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
