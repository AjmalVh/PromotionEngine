using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Models
{
    public class Cart
    {
        public Cart()
        {
            this.CartItems = new List<CartItem>();
            this.PromotionAppliedSKUs = new HashSet<string>();
        }

        /// <summary>
        /// Items in Cart
        /// </summary>
        public IList<CartItem> CartItems { get; set; }

        /// <summary>
        /// List of SKUs where promotion is already applied
        /// </summary>
        public HashSet<string> PromotionAppliedSKUs { get; set; }

        /// <summary>
        /// Add products to cart
        /// </summary>
        /// <param name="product"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public Cart AddToCart(Product product, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                CartItem item = new() { Product = product, };
                CartItems.Add(item);
            }

            return this;
        }

        /// <summary>
        /// Get products in cart with given SKU
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        public IEnumerable<CartItem> GetProduct(string sku)
        {
            return this.CartItems.Where(x => x.Product.SKU == sku);
        }
    }
}
