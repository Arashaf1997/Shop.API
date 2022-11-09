using Dependencies.Models;
using Shop.Application.Dtos.CategoryPropertyDtos;
using Shop.Application.Dtos.PorpertyValueDto;
using Shop.Application.Dtos.ProductColorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.ProductDtos
{
    public class UpdateColorDto
    {
        public int Id{ get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public ProductStatuses ProductStatus { get; set; }
    }

}
