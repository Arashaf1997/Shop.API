using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class Order : BaseModel
    {
        public Order()
        {
        }

        public int UserId { get; set; }
        public int TotalPrice { get; set; }
        public bool IsPaid { get; set; }
    }
}
