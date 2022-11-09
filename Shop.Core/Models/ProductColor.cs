using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class ProductColor : BaseModel
    {
        public ProductColor()
        {
        }
        public int ProductId { get; set; }
       
        public int ColorId { get; set; }
        public int Price { get; set; }
        public int ColleaguePrice { get; set; }
        public bool IsExists { get; set; }
    }
}
