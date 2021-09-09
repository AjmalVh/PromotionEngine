using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine.PromotionRules
{
    public class NItemsForFixedPrice : IPromotionRule
    {
        /// <param name="sku">SKU of the product which this offer applies</param>
        /// <param name="quantityRequired"> Quantity for items required for this promotion</param>
        /// <param name="fixedPrice">Fixed discounted price</param>
        public NItemsForFixedPrice(string sku, int quantityRequired, decimal fixedPrice)
        {
            this.SKU = sku;
            this.FixedPrice = fixedPrice;
            this.QuantityRequired = quantityRequired;
        }

        /// <summary>
        /// SKU of the product which this offer applies
        /// </summary>
        public string SKU { get; private set; }

        /// <summary>
        /// Fixed discounted price
        /// </summary>
        public decimal FixedPrice { get; private set; }

        /// <summary>
        /// Quantity for items required for this promotion 
        /// </summary>
        public int QuantityRequired { get; private set; }

        public decimal CalculateDiscount(Cart cart)
        {
            if (this.IsApplicable(cart))
            {
                var thisProductInCart = cart.CartItems.Where(item => item.Product.SKU == this.SKU);

                var unitPrice = thisProductInCart.FirstOrDefault().Product.Price;
                var quantityInCart = thisProductInCart.Count();
                var originalPrice = unitPrice * quantityInCart;

                var discountFromOriginalPrice = (unitPrice * QuantityRequired) - FixedPrice;

                var numberOfDiscountedBundles = quantityInCart / QuantityRequired;

                var totalDiscount = discountFromOriginalPrice * numberOfDiscountedBundles;
                var discountAppliedProductCount = numberOfDiscountedBundles * QuantityRequired;

                return totalDiscount;
            }

            return default;
        }

        
        public bool IsApplicable(Cart cart)
        {
            var thisProductInCart = cart.CartItems.Where(item => item.Product.SKU == this.SKU);

            if (thisProductInCart.Any()
                && (thisProductInCart.Count() >= this.QuantityRequired)
                && !cart.PromotionAppliedSKUs.Contains(this.SKU))
            {
                return true;
            }

            return false;
        }
    }
}
