using Dependencies.Models;
using Shop.Application.Dtos.ProductColorDto;
using Shop.Application.Dtos.PropertyValueDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.BonusDtos
{
    public class AddBonusDto
    {
        public int Stars { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }

}
