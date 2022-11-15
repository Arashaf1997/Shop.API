using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    [Table("ProductDiscounts", Schema = "dbo")]
    public class Discount : BaseModel
    {
        public Discount()
        {
        }
        public int DiscountPercent { get; set; }
        public int Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int ProductColorId { get; set; }

    }
}
