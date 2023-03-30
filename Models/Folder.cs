using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace WebAPI_SQL.Models
{
    public class Folder
    {
        public string? FolderName { get; set; }
    
    }
    public class FolderVM : Folder 
    {
        [Key]
        public int FolderId { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? DateEdit { get; set; }
    }
    public class UploadFile 
    {
        [Key]
        public int FileId { get; set; }
        [Required]
        [ForeignKey("Folder")]
        public int FolderId { get; set; }
        [Required]
        public string FileName { get; set; }
        public string? FilePath { get; set; } = null;
        public string? FileType { get; set; } = null;

        public DateTime? DateUpload { get; set; }

    }
}
