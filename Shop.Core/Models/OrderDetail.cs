using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class OrderDetail : BaseModel
    {
        public OrderDetail()
        {
        }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public int ProductColorId { get; set; }

    }
}
