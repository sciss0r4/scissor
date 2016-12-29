using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModels
{
    public class IndexDocument
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Index")]
        public int IndexId { get; set; }
        [ForeignKey("OcrFile")]
        public int OcrId { get; set; }
        public int LuceneIndexId { get; set; }

        public Index Index { get; set; }
        public OcrFile OcrFile { get; set; }
    }
}
