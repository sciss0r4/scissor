using System;
using Tesseract;
using System.Drawing;
using System.IO;

namespace OcrEngine
{
    public class ImageToOcrExtractor : IDisposable
    {
        public TesseractEngine TesseractEngine { get; private set; }

        public ImageToOcrExtractor(TesseractEngine tesseractEngine)
        {
            TesseractEngine = tesseractEngine;
        }

        public string ExtractTextFromImage(Pix image)
        {
            using (var page = TesseractEngine.Process(image))
            {
                return page.GetText();
            }
        }

        public void GetTextFileFromImage(ExtractionImageData data)
        {
            var text = ExtractTextFromImage(data.ImageData);

            using (var sw = new StreamWriter(data.DestFilePath))
            {
                sw.WriteLine(text);
            }
        }

        public void Dispose()
        {
            TesseractEngine.Dispose();
        }
    }
}
