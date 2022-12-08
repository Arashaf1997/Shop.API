using Dependencies.Models;
using Shop.Application.Dtos.ProductColorDto;
using Shop.Application.Dtos.PropertyValueDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.BlogDtos
{
    public class AddBlogDto
    {
        public string Subject { get; set; }
        public string Text { get; set; }
        public int ImageFileContentId { get; set; }
    }

}
