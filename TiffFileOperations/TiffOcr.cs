using DatabaseModels;
using OcrEngine;
using Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace TiffFileOperations
{
    public class TiffOcr
    {
        public Dictionary<TiffFile,OcrFile> OcrTiffs(List<TiffFile> tiffs)
        {
            Dictionary<TiffFile, OcrFile> ocrs = new Dictionary<TiffFile, OcrFile>();

            using (var tes = new TesseractEngine(Constants.TesseractLanguageDataPath, "eng"))
            using (var imgEx = new ImageToOcrExtractor(tes))
            {
                foreach (var t in tiffs)
                {
                    var ocr = new OcrFile
                    {
                        Path = Path.ChangeExtension(t.Path, ".txt")
                    };

                    imgEx.GetTextFileFromImage(new ExtractionImageData
                    {
                        ImageData = Pix.LoadFromFile(t.Path),
                        DestFilePath = ocr.Path
                    });

                    ocr.Size = new FileInfo(ocr.Path).Length;

                    ocrs.Add(t,ocr);
                }
            }

            return ocrs;
        }
    }
}
