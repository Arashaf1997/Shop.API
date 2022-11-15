using Dependencies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.ProductPropertyDtos
{
    public class GetProductPropertyDto
    {
        public int PropertyValueId { get; set; }
        public string Value { get; set; }
        public int CategoryPropertyId { get; set; }
        public string CategoryPropertyTitle { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
    }
}
