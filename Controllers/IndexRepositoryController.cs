using DatabaseModels;
using IndexingEngine;
using IndexRepositoryOperations;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using OcrFileOperations;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TiffFileOperations;

namespace Controllers
{
    public class IndexRepositoryController
    {
        private GetIndexRepositoryData _getIndexRepoData;
        private GetTiffFileData _getTiffData;
        private GetOcrFileData _getOcrData;
        private AddIndexData _addIndxData;
        private ModifyIndexData _modIndxData;
        private GetIndexData _getIndxData;

        public IndexRepositoryController()
        {
            _getIndexRepoData = new GetIndexRepositoryData();
            _getTiffData = new GetTiffFileData();
            _getOcrData = new GetOcrFileData();
            _addIndxData = new AddIndexData();
            _modIndxData = new ModifyIndexData();
            _getIndxData = new GetIndexData();
        }

        public List<Index> GetIndexes()
        {
            return _getIndxData.GetAllIndexes(new TextDataManagerContext());
        }

        public void CreateIndex(int[] tiffFilesIds, string indexLabel)
        {
            VerifyIndexLabel(indexLabel);

            int begLastLuceneIndexId = 0;
            var tiffs = _getTiffData.GetTiffFiles(new TextDataManagerContext(), tiffFilesIds);
            var ocrs = _getOcrData.GetOcrFiles(new TextDataManagerContext(), tiffs.Select(t => t.OcrId.Value).ToArray());

            var indexPair = HandleIndexCreation(begLastLuceneIndexId, indexLabel);
            var indexMngr = indexPair.Item1;
            var indexObject = indexPair.Item2;
            var docs = PrepareDocsToIndex(ocrs, begLastLuceneIndexId, indexObject.Id);

            indexMngr.InsertDocsToIndex(docs.Select(d => d.Doc));
        }

        private void VerifyIndexLabel(string indexLabel)
        {
            Regex r = new Regex("^[a-zA-Z0-9]*$");
            if(!r.IsMatch(indexLabel))
            {
                throw new ArgumentException("Index Label should contain only alphanumeric characters","indexLabel");
            }
        }

        private Tuple<IndexManager,Index> HandleIndexCreation(int begLastLuceneIndexId, string indexLabel)
        {
            var indexRepo = _getIndexRepoData.GetTopIndexRepository(new TextDataManagerContext());
            var indexStructure = FolderStructuresCreator.CreateDefinedFolderStructure(indexRepo.Path, Constants.IndexFolderPrefix, Constants.BegIndexFolderSuffix);
            var indexMngr = new IndexManager(FSDirectory.Open(indexStructure), new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30), IndexWriter.MaxFieldLength.LIMITED);
            var indexObject = new Index()
            {
                IndexRepositoryId = indexRepo.Id,
                LastLuceneIndexId = begLastLuceneIndexId,
                Path = indexStructure.FullName,
                Label = indexLabel
            };

            _addIndxData.AddIndex(new TextDataManagerContext(), indexObject);

            return new Tuple<IndexManager,Index>(indexMngr, indexObject);
        }

        private IEnumerable<FullDocument> PrepareDocsToIndex(IEnumerable<OcrFile> ocrs, int lastIndexId, int indexId)
        {
            List<FullDocument> docs = new List<FullDocument>();

            foreach(var o in ocrs)
            {
                Document doc = new Document();
                doc.Add(new Field("BUMP", new System.IO.StreamReader(o.Path)));
                docs.Add(new FullDocument() { Doc = doc, IdLucene = lastIndexId, Ocr = o });
                ++lastIndexId;
            }

            UpdateIndexes(indexId, lastIndexId, docs); 

            return docs;
        }

        private void UpdateIndexes(int indexId, int lastIndexId, List<FullDocument> docs)
        {
            _modIndxData.UpdateLastIndexId(new TextDataManagerContext(), indexId, lastIndexId);

            foreach(var d in docs)
            {
                var indxDoc = new IndexDocument()
                {
                    IndexId = indexId
                    ,OcrId = d.Ocr.Id
                    ,LuceneIndexId = d.IdLucene
                };

                _addIndxData.AddIndexDocument(new TextDataManagerContext(), indxDoc);
            }
        }
    }
}
