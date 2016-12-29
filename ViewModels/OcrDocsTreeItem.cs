using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class OcrDocsTreeItem
    {
        public OcrDocsTreeItem()
        {
            Tiffs = new ObservableCollection<TiffFileViewModel>();
        }

        public FileRepositoryViewModel Repository { get; set; }
        public ObservableCollection<TiffFileViewModel> Tiffs { get; set; }
    }
}
