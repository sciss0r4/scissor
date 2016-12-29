using DatabaseModels;
using Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiffFileOperations
{
    public class TiffMetadataExtractor
    {
        public TiffFile ExtractTiffMetadata(string tiffPath)
        {
            var info = new FileInfo(tiffPath);

            var tiffInfo = new TiffFile()
            {
                Path = tiffPath,
                Size = info.Length,
                OriginalFileName = info.Name
            };

            return tiffInfo;
        }

        public List<TiffFile> ExtractTiffMetadataForAllTiffsInDirectory(string dirPath)
        {
            var imgFiles = FileHelpers.GetFiles(Constants.SupportedExtensions, dirPath);
            var imagesMetadata = new List<TiffFile>();

            foreach(var tiff in imgFiles)
            {
                imagesMetadata.Add(ExtractTiffMetadata(tiff));
            }

            return imagesMetadata;
        }
    }
}
