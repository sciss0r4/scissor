using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseModels
{
    public class FileRepository
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Path { get; set; }
        [Index(IsUnique = true)]
        [Required, MaxLength(50,ErrorMessage = "Label must be 50 characters or less")]
        public string Label { get; set; }
        [Required]
        public long SpaceInKbytes { get; set; }
        [Required]
        public long SpaceLeft { get; set; }
    }
}