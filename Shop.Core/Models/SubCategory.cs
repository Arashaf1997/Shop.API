using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class SubCategory : BaseModel
    {
        public SubCategory()
        { }
        public string Title { get; set; }
        public int CategoryId { get; set; }
    }
}
