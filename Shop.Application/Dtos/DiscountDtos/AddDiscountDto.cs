using Dependencies.Models;
using Shop.Application.Dtos.ProductColorDto;
using Shop.Application.Dtos.PropertyValueDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.DiscountDtos
{
    public class AddDiscountDto
    {
        public float DiscountPercent { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Price { get; set; }
        public int ProductColorId { get; set; }
    }

}
