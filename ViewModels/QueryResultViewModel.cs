using Controllers;
using CustomViews;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ViewModels
{
    public class QueryResultViewModel
    {
        public string OriginalFileName { get; set; }
        public int OcrId { get; set; }

        private IndexSearchController _indexSearch;

        private RelayCommand _resultsClickCommand;
        public ICommand ResultsClickCommand
        {
            get
            {
                if (_resultsClickCommand == null)
                {
                    _resultsClickCommand = new RelayCommand(p => ExecuteResultsClickCommand(), p => CanExecuteResultsClickCommand());
                }

                return _resultsClickCommand;
            }
        }

        public QueryResultViewModel()
        {
            _indexSearch = new IndexSearchController();
        }

        public void ExecuteResultsClickCommand()
        {
            var path = _indexSearch.GetOcrPath(OcrId);
            Window w = new TextQueryHighlighted(path, String.Empty);
            w.ShowDialog();
        }

        public bool CanExecuteResultsClickCommand()
        {
            return true;
        }
    }
}
