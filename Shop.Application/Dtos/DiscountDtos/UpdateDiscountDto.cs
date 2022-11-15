using Dependencies.Models;

namespace Shop.Application.Dtos.DiscountDtos
{
    public class UpdateDiscountDto
    {
        public int Id{ get; set; }
        public int DiscountPercent { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
