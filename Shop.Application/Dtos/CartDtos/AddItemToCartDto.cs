using Dependencies.Models;
using Shop.Application.Dtos.ProductColorDto;
using Shop.Application.Dtos.PropertyValueDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.CartDtos
{
    public class AddItemToCartDto
    {
        public int ProductColorId { get; set; }
        public int UserId { get; set; }
        public int Count { get; set; }
    }

}
