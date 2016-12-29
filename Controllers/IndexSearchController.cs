using DatabaseModels;
using IndexingEngine;
using IndexRepositoryOperations;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using OcrFileOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiffFileOperations;

namespace Controllers
{
    public class IndexSearchController
    {
        private GetIndexData _getIndxData;
        private GetTiffFileData _getTiffFileData;
        private GetOcrFileData _getOcrData;
        public IndexSearchController()
        {
            _getIndxData = new GetIndexData();
            _getTiffFileData = new GetTiffFileData();
            _getOcrData = new GetOcrFileData();
        }

        public Dictionary<string,int> RunQuery(int IndexId, string queryString)
        {
            Index indx = _getIndxData.GetIndex(new TextDataManagerContext(),IndexId);
            IndexManager im = new IndexManager(FSDirectory.Open(indx.Path), new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30), IndexWriter.MaxFieldLength.LIMITED);
            QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "BUMP", im.IndexAnalyzer);
            Query query = parser.Parse(queryString);
            var collect = TopScoreDocCollector.Create(40, true);
            im.AskIndexWithQuery(query, collect);
            var d = collect.TopDocs().ScoreDocs.Select(x => x.Doc).ToList();
            return GetVmDictionary(IndexId, d);
        }

        public string GetOcrPath(int ocrId)
        {
            return _getOcrData.GetOcrPath(new TextDataManagerContext(), ocrId);
        }

        private Dictionary<string, int> GetVmDictionary(int IndexId, List<int> resultDocs)
        {
            var resultCollection = new Dictionary<string, int>();

            foreach(var r in resultDocs)
            {
                IndexDocument id = _getIndxData.GetIndexDocument(new TextDataManagerContext(), IndexId, r);
                TiffFile tiff = _getTiffFileData.GetTiffFile(new TextDataManagerContext(), id.OcrId);
                resultCollection.Add(tiff.OriginalFileName, id.OcrId);
            }

            return resultCollection;
        }
    }
}
