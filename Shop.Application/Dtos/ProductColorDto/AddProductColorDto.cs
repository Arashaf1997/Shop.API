namespace Shop.Application.Dtos.ProductColorDto
{
    public class AddProductColorDto
    {
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int Price { get; set; }
        public int ColleaguePrice { get; set; }
        public bool IsExists { get; set; }
    }
}
