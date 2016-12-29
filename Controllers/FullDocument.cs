using DatabaseModels;
using Lucene.Net.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    internal class FullDocument
    {
        public int IdLucene { get; set; }
        public Document Doc { get; set; }
        public OcrFile Ocr { get; set; }
    }
}
