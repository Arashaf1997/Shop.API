using Dependencies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.ProductDtos
{
    public class GetProductDto
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int VisitCount { get; set; }
        public ProductStatuses ProductStatus  { get; set; }
        public string CategoryName { get; set; }
        public int SellCount { get; set; }
        public string Colors { get; set; }
        public int RealPrice { get; set; }
        public int DiscountedPrice { get; set; }
        public int DiscountPercent { get; set; }
        public DateTime DiscountEndDate { get; set; }
        public int Bonus { get; set; }
        public string UsageIds { get; set; }
        public int MainImageId { get; set; }
        public string ImageUrl { get; set; }
        public Object Props { get; set; }
    }
}
