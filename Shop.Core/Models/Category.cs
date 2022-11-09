using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class Category : BaseModel
    {
        public Category()
        {
        }
        public string Title { get; set; }
    }
}
