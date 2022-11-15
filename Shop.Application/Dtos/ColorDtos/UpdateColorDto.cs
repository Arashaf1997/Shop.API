using Dependencies.Models;

namespace Shop.Application.Dtos.ProductDtos
{
    public class UpdateColorDto
    {
        public int Id{ get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public ProductStatuses ProductStatus { get; set; }
    }

}
