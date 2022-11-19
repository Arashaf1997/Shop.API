using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class FileContent : BaseModel
    {
        public FileContent()
        {
        }
        public string GuidName { get; set; }
        public string RealFileName { get; set; }
        public int Length { get; set; }
        public string MimeType { get; set; }
        public string FileExtension { get; set; }
        public string FilePath { get; set; }
    }
}
