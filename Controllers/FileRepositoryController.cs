using DatabaseModels;
using FileRepositoryOperations;
using Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiffFileOperations;

namespace Controllers
{
    public class FileRepositoryController : IFileRepositoryController
    {
        private AddFileRepositoryData _addRepoData;
        private GetFileRepositoryData _getRepoData;
        private ModifyFileRepositoryData _modifyRepoData;
        private TiffMetadataExtractor _tiffMetadataExtractor;
        private AddTiffFileData _addTiffFileData;
        private GetTiffFileData _getTiffFileData;
        private TiffLabeler _tiffLabeler;

        public FileRepositoryController()
        {
            _addRepoData = new AddFileRepositoryData();
            _getRepoData = new GetFileRepositoryData();
            _tiffMetadataExtractor = new TiffMetadataExtractor();
            _addTiffFileData = new AddTiffFileData();
            _getTiffFileData = new GetTiffFileData();
            _tiffLabeler = new TiffLabeler();
            _modifyRepoData = new ModifyFileRepositoryData();
        }
        public List<FileRepository> GetRepositories()
        {
            return _getRepoData.GetRepositories(new TextDataManagerContext());
        }

        public void LoadDataToRepository(string zipFilePath, int repositoryId)
        {
            using (var archive = ZipFile.OpenRead(zipFilePath))
            {
                if (!ZipHelpers.ZipContainsGivenExtensionsOnly(archive, Constants.SupportedExtensions))
                {
                    throw new ArgumentException("Provided zip does not only contain supported image formats.");
                }

                var repo = _getRepoData.GetRepository(new TextDataManagerContext(), repositoryId);
                HandleDataLoading(zipFilePath, archive, repo);
            }
        }

        private void HandleDataLoading(string zipFilePath, ZipArchive archive, FileRepository repository)
        {
            //var repository = _getRepoData.GetRepository(new TextDataManagerContext(), repositoryViewModel.Id);
            ValidateInputData(zipFilePath, archive, repository);


            var destPath = FolderStructuresCreator.CreateDefinedFolderStructure(repository.Path, Constants.ZipFolderPrefix, Constants.BegZipFolderSuffix);
            var destZipPath = FolderStructuresCreator.GetZipPathForExistingFolderStructure(destPath);
            ZipHelpers.CopyExtractRemoveZipFile(zipFilePath, destZipPath);
            var tiffsWithMetadata = _tiffMetadataExtractor.ExtractTiffMetadataForAllTiffsInDirectory(destPath.FullName);
            var lastTiffInRepo = _getTiffFileData.GetLastTiffInRepository(new TextDataManagerContext(), repository);
            var labeledTiffs = _tiffLabeler.LabelTiffs(tiffsWithMetadata, lastTiffInRepo);
            var refolderedTiffs = FolderStructuresCreator.RefolderTiffsToRootRepoPath(repository, labeledTiffs);
            _addTiffFileData.AddTiffFiles(new TextDataManagerContext(),refolderedTiffs);
            _modifyRepoData.SubtractSpaceLeft(new TextDataManagerContext(), repository, refolderedTiffs.Sum(x => x.Size));
            FileHelpers.RemoveDirectory(destPath);
        }

        private void ValidateInputData(string zipFilePath, ZipArchive archive, FileRepository repository)
        {
            var extractedZipSize = ZipHelpers.EstimateZipSize(archive, new FileInfo(zipFilePath));
            if (extractedZipSize > repository.SpaceLeft)
            {
                throw new ArgumentException("Provided repository does not contain enough space to load this package.");
            }
        }
    }
}
