namespace PromotionEngine.Models
{
    public class Product
    {
        public Product(string sku, decimal price)
        {
            this.SKU = sku;
            this.Price = price;
        }

        public string SKU { get; private set; }

        public decimal Price { get; private set; }
    }
}
