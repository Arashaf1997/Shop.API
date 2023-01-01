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
        public string ImageUrl { get; }
        public int UserId { get; }
        public DateTime InsertTime { get; }
        public DateTime EditTime { get; }
        public int BlogCategoryId { get; }
        public string BlogCategoryTitle { get; }
    }
}
