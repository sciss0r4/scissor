using DatabaseModels;
using FileRepositoryOperations;
using OcrFileOperations;
using Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiffFileOperations;

namespace Controllers
{
    public class TiffFileController : ITiffFileController
    {
        private GetTiffFileData _getTiffFileData;
        private TiffOcr _tiffOcr;
        private AddOcrFileData _addOcrFileData;
        private AddTiffFileData _addTiffFileData;
        private GetFileRepositoryData _getFileRepoData;
        private ModifyFileRepositoryData _modFileRepoData;
        public TiffFileController()
        {
            _getTiffFileData = new GetTiffFileData();
            _tiffOcr = new TiffOcr();
            _addOcrFileData = new AddOcrFileData();
            _addTiffFileData = new AddTiffFileData();
            _getFileRepoData = new GetFileRepositoryData();
            _modFileRepoData = new ModifyFileRepositoryData();
        }
        public List<TiffFile> GetTiffFiles(int repositoryId)
        {
            var repo = _getFileRepoData.GetRepository(new TextDataManagerContext(), repositoryId);
            return _getTiffFileData.GetTiffFiles(new TextDataManagerContext(), repo);
        }

        public List<TiffFile> GetOcredTiffFiles(int repositoryId)
        {
            var repo = _getFileRepoData.GetRepository(new TextDataManagerContext(), repositoryId);
            return _getTiffFileData.GetOcredTiffFiles(new TextDataManagerContext(), repo);
        }

        public void ExtractOcr(int[] tiffIds)
        {
            var tiffs = _getTiffFileData.GetTiffFiles(new TextDataManagerContext(),tiffIds);
            var ocrs = _tiffOcr.OcrTiffs(tiffs);
            _addOcrFileData.AddOcrFiles(new TextDataManagerContext(),ocrs.Values.ToList());
            var repo = _getFileRepoData.GetRepository(new TextDataManagerContext(), tiffs.First().FileRepositoryId);
            _modFileRepoData.SubtractSpaceLeft(new TextDataManagerContext(), repo, ocrs.Values.Sum(x => x.Size));
            _addTiffFileData.ConnectTiffsWithOcrs(new TextDataManagerContext(), ocrs);
        }
    }
}
