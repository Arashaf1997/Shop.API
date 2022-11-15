using Dependencies.Models;

namespace Shop.Application.Dtos.BrandDtos
{
    public class UpdateBrandDto
    {
        public int Id{ get; set; }
        public int BrandPercent { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
