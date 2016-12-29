using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseModels
{
    public class OcrFile
    {
        [Key]
        public int Id { get; set; }
        [Index(IsUnique = true)]
        [Required, MaxLength(255, ErrorMessage = "File path cannot exceed 255 characters")]
        public string Path { get; set; }
        [Required]
        public long Size { get; set; }
    }
}
