using Dependencies.Models;
using Shop.Application.Dtos.ProductColorDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.ProductPropertyDtos
{
    public class UpdateProductPropertyDto
    {
        public int Id{ get; set; }
        public string Title { get; set; }
    }

}
