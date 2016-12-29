using DatabaseModels;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TiffFileOperations
{
    public class TiffLabeler
    {
        public List<TiffFile> LabelTiffs(List<TiffFile> inputTiffs, TiffFile lastLabeledTiff)
        {
            var tiffPrefix = Constants.StandardTiffPrefix;
            var padding = Constants.TiffSuffixPadding;
            int tiffNumber = 1;

            if(lastLabeledTiff != null)
            {
                var lastTiffSuffix = Regex.Match(lastLabeledTiff.Label, @"\d+").Value;
                tiffNumber = Int32.Parse(lastTiffSuffix) + 1;
            }

            foreach(var tiff in inputTiffs)
            {
                tiff.Label = tiffPrefix + tiffNumber.ToString().PadLeft(padding, '0');
                ++tiffNumber;
            }

            return inputTiffs;
        }
    }
}
