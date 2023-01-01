using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class BlogCategory : BaseModel
    {
        public BlogCategory()
        {
        }
        public string Title { get; set; }
    }
}
