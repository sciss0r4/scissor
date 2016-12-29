using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class Constants
    {
        public static readonly string TesseractLanguageDataPath = GetTessLangDataPath();  // @"C:\Users\kryst\Documents\ProjektInżynierski\TextDataManager\OcrEngine\TesseractLanguageData\tessdata";
        public static readonly string[] SupportedExtensions = {".tif", ".tiff"};
        public const string StandardTiffPrefix = "TIFF_";
        public const int TiffSuffixPadding = 7;
        public const string ZipFolderPrefix = "ZIP_EXTRACTION_";
        public const int BegZipFolderSuffix = 1;
        public const string IndexFolderPrefix = "LUCENE_";
        public const int BegIndexFolderSuffix = 1;
        public const string ExtractionFileName = "TO_EXTRACT.zip";

        private static string GetTessLangDataPath()
        {
            var assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var tessDataPath = Path.GetFullPath(Path.Combine(assemblyDir, @"..\..\..\"));
            tessDataPath = Path.Combine(tessDataPath, @"OcrEngine\TesseractLanguageData\tessdata");

            return tessDataPath;
        }
    }
}
