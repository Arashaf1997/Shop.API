using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class CategoryProperty : BaseModel
    {
        public CategoryProperty()
        {
        }
        public int CategoryId { get; set; }
        public string Title { get; set; }
    }
}
