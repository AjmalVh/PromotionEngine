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
        /// <param name="fixedPrice">Fixed discounted price</param>
        /// <param name="quantityRequired"> Quantity for items required for this promotion</param>
        public NItemsForFixedPrice(string sku, decimal fixedPrice, int quantityRequired)
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
            throw new NotImplementedException();
        }

        
        public bool IsApplicable(Cart cart)
        {
            if (cart.CartItems.Any() && !cart.PromotionAppliedSKUs.Contains(this.SKU))
            {
                return true;
            }

            return false;
        }
    }
}
