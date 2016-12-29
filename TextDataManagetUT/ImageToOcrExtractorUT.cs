using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OcrEngine;
using Shared;
using Tesseract;

namespace TextDataManagetUT
{
    [TestClass]
    public class ImageToOcrExtractorUT
    {
        [TestMethod]
        public void GetTextFileForSpecificImage()
        {
            using (var tes = new TesseractEngine(Constants.TesseractLanguageDataPath,"eng"))
            using (var imgEx = new ImageToOcrExtractor(tes))
            {
                imgEx.GetTextFileFromImage(new ExtractionImageData
                {
                    ImageData = Pix.LoadFromFile(@"C:\Users\kryst\Documents\AC Szkoda\Polisa.jpg"),
                    DestFilePath = @"C:\Users\kryst\Documents\AC Szkoda\Polisa.txt"
                });

                imgEx.GetTextFileFromImage(new ExtractionImageData
                {
                    ImageData = Pix.LoadFromFile(@"C:\Users\kryst\Documents\AC Szkoda\Podatek od nieruchomości.jpg"),
                    DestFilePath = @"C:\Users\kryst\Documents\AC Szkoda\Podatek od nieruchomości.txt"

                });
            }
        }
    }
}
