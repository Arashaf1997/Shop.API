using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class ProductImage : BaseModel
    {
        public ProductImage()
        {
        }
        public int ProductId { get; set; }
        public bool IsMainImage { get; set; }
        public string ImageUrl { get; set; }
    }
}
