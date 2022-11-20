using Dependencies.Models;
using Shop.Application.Dtos.ProductColorDto;
using Shop.Application.Dtos.PropertyValueDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.CommentDtos
{
    public class AddCommentDto
    {
        public string Text { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int ReplyTo { get; set; }
    }

}
