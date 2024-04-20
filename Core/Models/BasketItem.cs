namespace Core.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public Decimal? Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;


    }
}