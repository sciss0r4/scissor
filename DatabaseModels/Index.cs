using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModels
{
    public class Index
    {
        [Key]
        public int Id { get; set; }
        [Index(IsUnique = true)]
        [Required, MaxLength(255, ErrorMessage = "File path cannot exceed 255 characters")]
        public string Path { get; set; }
        [Index(IsUnique = true)]
        [Required, MaxLength(50, ErrorMessage = "Label must be 50 characters or less")]
        public string Label { get; set; }
        [ForeignKey("IndexRepository")]
        public int? IndexRepositoryId { get; set; }
        [DefaultValue(0)]
        public int LastLuceneIndexId { get; set; }

        public IndexRepository IndexRepository { get; set; }
    }
}
