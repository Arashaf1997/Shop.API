using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dependencies.Models
{
    public class Product : BaseModel
    {
        public Product()
        {
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public int VisitCount { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public ProductStatuses Status { get; set; }
    }

    public enum ProductStatuses
    {
        BrandNew = 1,
        Used = 2 ,
        Referwish = 3 
    }
}
