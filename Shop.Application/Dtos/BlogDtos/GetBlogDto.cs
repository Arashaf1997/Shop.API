using Dependencies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Application.Dtos.BlogDtos
{
    public class GetBlogDto
    {
        public int Id { get; }
        public string Subject { get; }
        public string Text { get; }
        public int ImageFileContentId { get; }

    }
}
