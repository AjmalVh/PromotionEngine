namespace PromotionEngine.Models
{
    public class Product
    {
        public Product(string sku, decimal price)
        {
            this.SKU = sku;
            this.Price = price;
        }

        /// <summary>
        /// Unique ID for a product
        /// </summary>
        public string SKU { get; private set; }

        /// <summary>
        /// Unit price of product
        /// </summary>
        public decimal Price { get; private set; }
    }
}
