using System.ComponentModel.DataAnnotations;

namespace Karne.API.Entities
{
    public class RawDataUpload
    {
        [Key]
        public int Id { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.Now;

        [StringLength(255)]
        public string? FileName { get; set; }

        public string? RawContent { get; set; }
    }
}
