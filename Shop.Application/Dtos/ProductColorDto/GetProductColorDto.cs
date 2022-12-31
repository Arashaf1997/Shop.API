namespace Shop.Application.Dtos.ProductColorDto
{
    public class GetProductColorDto
    {
        public int ProductColorId { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int RealPrice { get; set; }
        public int DiscountedPrice { get; set; }
        public int DiscountPercent { get; set; }
        public DateTime DiscountEndDate { get; set; }
        public int ColleaguePrice { get; set; }
        public bool IsExists { get; set; }
    }
}
