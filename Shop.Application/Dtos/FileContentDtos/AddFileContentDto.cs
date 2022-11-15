using Dependencies.Models;
using Microsoft.AspNetCore.Http;
using Shop.Application.Dtos.ProductColorDto;
using Shop.Application.Dtos.PropertyValueDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.FileContentDtos
{
    public class AddFileContentDto
    {
       public IFormFile form { get; set; }
    }

}
