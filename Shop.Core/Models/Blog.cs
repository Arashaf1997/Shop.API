using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class Blog : BaseModel
    {
        public Blog()
        {
        }
        public string Subject { get; set; }
        public string Text { get; set; }
        public int ImageFileContentId { get; set; }
    }
}
