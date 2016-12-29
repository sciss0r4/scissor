using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModels
{
    public class TiffFile
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("FileRepository")]
        [Index("IX_RepositoryLabel", 1, IsUnique = true)]
        public int FileRepositoryId { get; set; }
        [Index(IsUnique = true)]
        [Required, MaxLength(255, ErrorMessage = "File path cannot exceed 255 characters")]
        public string Path { get; set; }
        [Required]
        public string OriginalFileName { get; set; }
        [Index("IX_RepositoryLabel", 2, IsUnique = true)]
        [Required, MaxLength(50, ErrorMessage = "Label cannot exceed 50 characters")]
        public string Label { get; set; }
        [Required]
        public long Size { get; set; }
        [ForeignKey("OcrFile")]
        public int? OcrId{ get; set; }

        
        public FileRepository FileRepository { get; set; }
        public OcrFile OcrFile { get; set; }
    }
}