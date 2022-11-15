using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class Brand : BaseModel
    {
        public Brand()
        {
        }
        public string Title { get; set; }
        public int LogoFileContentId { get; set; }
    }
}
