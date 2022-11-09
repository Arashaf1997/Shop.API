using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class Comment : BaseModel
    {
        public Comment()
        {
        }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Text { get; set; }
    }
}
