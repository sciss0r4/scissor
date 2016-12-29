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
using System.Windows.Forms;
using System.Windows.Input;

namespace ViewModels
{
    public class FileManagementViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<FileRepositoryViewModel> _repositoryList;
        private ObservableCollection<TiffFileViewModel> _tiffFilesList; 
        private IFileRepositoryController _repoController;
        private ITiffFileController _tiffController;
        private RelayCommand _loadDataToRepo;
        private bool _dataAreProcessing;
        private RelayCommand _extractOcr;
        private FileRepositoryViewModel _selectedRepository;
        private TiffFileViewModel _selectedTiff;
        private bool _isBusy;

        public bool ProgressBarIndeterminate
        {
            get
            {
                return _isBusy;
            }

            set
            {
                _isBusy = value;
                NotifyPropertyChanged("ProgressBarIndeterminate");
            }
        }

        public ObservableCollection<FileRepositoryViewModel> RepositoryList
        {
            get
            {
                return _repositoryList;
            }

            set
            {
                _repositoryList = value;
                NotifyPropertyChanged("RepositoryList");
            }
        }

        public ObservableCollection<TiffFileViewModel> TiffFilesList
        {
            get
            {
                return _tiffFilesList;
            }

            set
            {
                _tiffFilesList = value;
                NotifyPropertyChanged("TiffFilesList");
            }
        }

        public ICommand LoadDataToRepo
        {
            get
            {
                if(_loadDataToRepo == null)
                {
                    _loadDataToRepo = new RelayCommand(p => ExecuteLoadDataToRepo(_repoController), p => CanExecuteLoadDataToRepo());
                }

                return _loadDataToRepo;
            }
        }

        public ICommand ExtractOcr
        {
            get
            {
                if (_extractOcr == null)
                {
                    _extractOcr = new RelayCommand(p => ExecuteExtractOcr(_tiffController), p => CanExecuteExtractOcr());
                }

                return _extractOcr;
            }
        }

        public FileRepositoryViewModel SelectedRepository
        {
            get { return _selectedRepository; }

            set
            {
                _selectedRepository = value;
                NotifyPropertyChanged("SelectedRepository");
            }
        }

        public TiffFileViewModel SelectedTiff
        {
            get { return _selectedTiff; }

            set
            {
                _selectedTiff = value;
                NotifyPropertyChanged("SelectedTiff");
                NotifyPropertyChanged("SelectedTiffs");
            }
        }

        public List<TiffFileViewModel> SelectedTiffs
        {
            get
            {
                if(TiffFilesList == null)
                {
                    return null;
                }

                return TiffFilesList.Where(x => x.IsSelected).ToList();
            }
        }

        private InterVmRefresher _interRefresher;

        public FileManagementViewModel(InterVmRefresher refresher)
        {
            _repoController = new FileRepositoryController();
            _tiffController = new TiffFileController();
            _interRefresher = refresher;
            ProgressBarIndeterminate = false;

            RepositoryList = ViewModelMapper.MapRepoListToViewModelCollection(_repoController.GetRepositories());
        }

        private ObservableCollection<TiffFileViewModel> GetTiffs(ITiffFileController tiffController, FileRepositoryViewModel repoVM)
        {
            return ViewModelMapper.MapTiffFileListToViewModelCollection(tiffController.GetTiffFiles(repoVM.Id));
        }

        private void ExecuteLoadDataToRepo(IFileRepositoryController repoController)
        {
            System.Windows.Forms.OpenFileDialog oFileDialog = new System.Windows.Forms.OpenFileDialog();
            oFileDialog.Filter = "Zip Packages|*.zip";
            oFileDialog.Title = "Select a Zip Package";

            if (oFileDialog.ShowDialog() == DialogResult.OK)
            {
                _dataAreProcessing = true;
                ProgressBarIndeterminate = true;
                Task.Run(() => repoController.LoadDataToRepository(oFileDialog.FileName, SelectedRepository.Id)).ContinueWith(x => RefreshAfterLoading());
            }
        }

        private void RefreshAfterLoading()
        {
            TiffFilesList = GetTiffs(_tiffController, SelectedRepository);
            _dataAreProcessing = false;
            NotifyPropertyChanged("ExtractOcr");
            NotifyPropertyChanged("LoadDataToRepo");
            _interRefresher.RefreshTiffViewTree();
            ProgressBarIndeterminate = false;
        }

        private bool CanExecuteLoadDataToRepo()
        {
            return SelectedRepository != null && !_dataAreProcessing;
        }

        private void ExecuteExtractOcr(ITiffFileController tiffController)
        {
            ProgressBarIndeterminate = true;
            _dataAreProcessing = true;
            Task.Run(() => tiffController.ExtractOcr(SelectedTiffs.Select(x => x.Id).ToArray())).ContinueWith(x => RefreshAfterLoading());
        }

        private bool CanExecuteExtractOcr()
        {
            return SelectedTiffs != null && SelectedTiffs.Count != 0 && !_dataAreProcessing && SelectedTiffs.All(x => !x.HasOcr);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

            if(propertyName == "SelectedRepository" && SelectedRepository != null)
            {
                TiffFilesList = GetTiffs(_tiffController, SelectedRepository);
            }
        }
    }
}
