using Dependencies.Models;
using Shop.Application.Dtos.ProductColorDto;
using Shop.Application.Dtos.PropertyValueDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.ProductDtos
{
    public class AddCategoryDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ProductStatuses ProductStatus  { get; set; }
        public int CategoryId { get; set; }
        //public List<GetCategoryPropertyDto> Colors { get; set; }
        public DateTime DiscountEndDate { get; set; }
        public List<int> UsageIds { get; set; } 
        public List<AddPropertyValueDto> PropertyValues { get; set; }
    }

}
