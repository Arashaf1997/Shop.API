using Dependencies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.ColorDtos
{
    public class GetColorDto
    {
        public int Id{ get; set; }
        public string ColorName { get; set; }
    }
}
