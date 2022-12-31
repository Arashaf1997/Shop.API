using Dependencies.Models;
using Shop.Application.Dtos.BonusDtos;
using Shop.Application.Dtos.CommentDtos;
using Shop.Application.Dtos.ProductColorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.ProductDtos
{
    public class GetSingleProductDto
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Object Props { get; set; }
        public ProductStatuses ProductStatus  { get; set; }
        public Object ColorPrices { get; set; }
        public Object Bonuses { get; set; }
        public string UsageIds { get; set; }
        public string Usages { get; set; }
        public string MainImageUrl { get; set; }
        public Object ImageUrls { get; set; }
        public Object LastComments { get; set; }
    }
}
