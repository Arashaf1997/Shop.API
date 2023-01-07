using Dependencies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.CartDtos
{
    public class GetCartDto
    {

        public string ProductTitle { get; }
        public int ProductColorId { get; }
        public int Count { get; }
        public int UserId { get; }
        public int ProductId { get; }
        public int ColorId { get; }
        public string ColorName { get; }
        public int ItemRealPrice { get; }
        public int ItemDiscountedPrice { get; }
        public int DiscountPercent { get; }
        public int TotalItemPrice { get; }
        public int TotalCartPrice { get; }
        public string ProductImage { get; }
    }
}