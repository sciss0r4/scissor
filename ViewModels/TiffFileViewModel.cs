using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class TiffFileViewModel
    {
        public int Id { get; set; }
        public string OriginalFileName { get; set; }
        public string Path { get; set; }
        public bool HasOcr { get; set; }
        public bool IsSelected { get; set; }
    }
}
