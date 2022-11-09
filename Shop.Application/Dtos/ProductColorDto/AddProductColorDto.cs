namespace Shop.Application.Dtos.ProductColorDto
{
    public class AddProductColorDto
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int Price { get; set; }
        public int DiscountPercent { get; set; }
        public int DiscountPrice { get; set; }
    }
}
