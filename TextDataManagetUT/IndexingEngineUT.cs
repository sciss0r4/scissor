using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IndexingEngine;
using Lucene.Net.Store;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using System.Linq;

namespace TextDataManagetUT
{
    [TestClass]
    public class IndexingEngineUT
    {
        private IndexManager _im = new IndexManager(FSDirectory.Open(@"C:\Lucene"), new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30), IndexWriter.MaxFieldLength.LIMITED);
        [TestMethod]
        public void DoNothingVer2()
        {
            Document doc = new Document();
            doc.Add(new Field("myName", new System.IO.StreamReader(@"C:\Repositories\A\TIFF_0000002.txt")));
            _im.InsertSingleDocToIndex(doc);
            AskNothingVer2();
        }

        private void AskNothingVer2()
        {
            QueryParser parser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "myName", _im.IndexAnalyzer);
            Query query = parser.Parse("OO1D0c");
            var collect = TopScoreDocCollector.Create(40, true);
            _im.AskIndexWithQuery(query, collect);
            var d = collect.TopDocs().ScoreDocs.Select(x => x.Doc).ToList();
        }
    }
}
