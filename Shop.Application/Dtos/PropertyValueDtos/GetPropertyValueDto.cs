using Dependencies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.PropertyValueDtos
{
    public class GetPropertyValueDto
    {
        public int CategoryPropertyId { get; set; }
        public string Value { get; set; }
    }
}
