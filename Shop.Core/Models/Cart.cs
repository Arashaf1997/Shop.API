using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class Cart : BaseModel
    {
        public Cart()
        {
        }
        public int ProductColorId { get; set; }
        public int Count { get; set; }
        public int UserId { get; set; }
    }
}
