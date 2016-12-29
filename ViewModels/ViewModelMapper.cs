using AutoMapper;
using DatabaseModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace ViewModels
{
    public static class ViewModelMapper
    {
        public static ObservableCollection<TiffFileViewModel> MapTiffFileListToViewModelCollection (List<TiffFile> tiffFiles)
        {
            ObservableCollection<TiffFileViewModel> resultCollection = new ObservableCollection<TiffFileViewModel>();
            Mapper.CreateMap<TiffFile, TiffFileViewModel>();

            foreach (var t in tiffFiles)
            {
                var tiffVM = Mapper.Map<TiffFileViewModel>(t);
                tiffVM.HasOcr = t.OcrId.HasValue;
                resultCollection.Add(tiffVM);
            }

            return resultCollection;
        }

        public static List<TiffFile> MapTiffViewModelsToTiffFileList (List<TiffFileViewModel> tiffViewModels)
        {
            List<TiffFile> resultCollection = new List<TiffFile>();
            Mapper.CreateMap<TiffFileViewModel, TiffFile>();

            foreach (var t in tiffViewModels)
            {
                resultCollection.Add(Mapper.Map<TiffFile>(t));
            }

            return resultCollection;
        }

        public static ObservableCollection<FileRepositoryViewModel> MapRepoListToViewModelCollection (List<FileRepository> repos)
        {
            ObservableCollection<FileRepositoryViewModel> resultCollection = new ObservableCollection<FileRepositoryViewModel>();
            Mapper.CreateMap<FileRepository, FileRepositoryViewModel>();

            foreach (var t in repos)
            {
                resultCollection.Add(Mapper.Map<FileRepositoryViewModel>(t));
            }

            return resultCollection;
        }

        public static ObservableCollection<IndexViewModel> MapIndexesListToViewModelCollection(List<Index> indexes)
        {
            ObservableCollection<IndexViewModel> resultCollection = new ObservableCollection<IndexViewModel>();
            Mapper.CreateMap<Index, IndexViewModel>();

            foreach (var t in indexes)
            {
                resultCollection.Add(Mapper.Map<IndexViewModel>(t));
            }

            return resultCollection;
        }

        public static ObservableCollection<QueryResultViewModel> MapDictionaryResultsToViewModelCollection(Dictionary<string,int> queryResults)
        {
            var resultCollection = new ObservableCollection<QueryResultViewModel>();

            foreach (var q in queryResults)
            {
                QueryResultViewModel qrvm = new QueryResultViewModel()
                {
                    OriginalFileName = q.Key,
                    OcrId = q.Value
                };

                resultCollection.Add(qrvm);
            }

            return resultCollection;
        }
    }
}
