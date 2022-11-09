using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class Tag : BaseModel
    {
        public Tag()
        {
        }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
