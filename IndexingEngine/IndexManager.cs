using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexingEngine
{
    public class IndexManager
    {
        public Directory IndexDirectory { get; set; }
        public Analyzer IndexAnalyzer { get; set; }
        public IndexWriter.MaxFieldLength MaxFieldLength { get; set; }

        public IndexWriter IndexWriter
        {
            get
            {
                return new IndexWriter(IndexDirectory, IndexAnalyzer, MaxFieldLength);
            }
        }

        public IndexSearcher IndexSearcher
        {
            get
            {
                return new IndexSearcher(IndexDirectory);
            }
        }

        public IndexManager(Directory dir, Analyzer analyzer, IndexWriter.MaxFieldLength maxIndexFieldLength)
        {
            IndexDirectory = dir;
            IndexAnalyzer = analyzer;
            MaxFieldLength = maxIndexFieldLength;
        }

        public void InsertSingleDocToIndex(Document doc)
        {
            using (var indx = IndexWriter)
            {
                indx.AddDocument(doc);
                IndexFlushHelper(indx);
            }
        }

        public void InsertDocsToIndex(IEnumerable<Document> doc)
        {
            using (var indx = IndexWriter)
            {
                foreach (var d in doc)
                {
                    indx.AddDocument(d);
                }

                IndexFlushHelper(indx);
            }
        }

        public void AskIndexWithQuery (Query q, Collector c)
        {
            using (var searcher = IndexSearcher)
            {
                searcher.Search(q, c);
            }
        }

        private void IndexFlushHelper(IndexWriter writer)
        {
            writer.Optimize();
            writer.Flush(true, true, true);
        }
    }
}
