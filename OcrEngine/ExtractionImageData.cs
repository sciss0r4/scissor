using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace OcrEngine
{
    public class ExtractionImageData
    {
        public Pix ImageData { get; set; }
        public string DestFilePath { get; set; }
    }
}
