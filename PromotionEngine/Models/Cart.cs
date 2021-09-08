using System.Collections.Generic;

namespace PromotionEngine.Models
{
    public class Cart
    {
        public Cart()
        {
            this.CartItems = new List<CartItem>();
        }

        /// <summary>
        /// Items in Cart
        /// </summary>
        public IList<CartItem> CartItems { get; set; }

        /// <summary>
        /// List of SKUs where promotion is already applied
        /// </summary>
        public List<string> PromotionAppliedSKUs { get; set; }

        public Cart AddToCart(Product product, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                CartItem item = new() { Product = product, };
                CartItems.Add(item);
            }

            return this;
        }
    }
}
