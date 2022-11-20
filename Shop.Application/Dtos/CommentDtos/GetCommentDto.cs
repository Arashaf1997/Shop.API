using Dependencies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.CommentDtos
{
    public class GetCommentDto
    {
        public int Id { get; }
        public string Title { get; }
        public string LogoPath { get; }

    }
}
