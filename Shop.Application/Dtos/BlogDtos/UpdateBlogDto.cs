using Dependencies.Models;

namespace Shop.Application.Dtos.BlogDtos
{
    public class UpdateBlogDto
    {
        public int Id{ get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public int ImageFileContentId { get; set; }
    }

}
