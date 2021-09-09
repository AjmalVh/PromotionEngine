using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine.PromotionRules
{
    public class CombinedItemFixedPrice : IPromotionRule
    {
        public CombinedItemFixedPrice(IEnumerable<string> skus, decimal fixedPrice)
        {
            this.CombinedDiscountSKUs = skus;
            this.FixedPrice = fixedPrice;
        }

        /// <summary>
        /// List of SKUs for combined offer
        /// </summary>
        public IEnumerable<string> CombinedDiscountSKUs { get; private set; }

        /// <summary>
        /// Fixed price for this SKU combination
        /// </summary>
        public decimal FixedPrice { get; private set; }


        public decimal CalculateDiscount(Cart cart)
        {
            throw new NotImplementedException();
        }

        public bool IsApplicable(Cart cart)
        {
            return cart.CartItems.Where(x => !cart.PromotionAppliedSKUs.Contains(x.Product.SKU))
                .Select(item => item.Product.SKU).Intersect(CombinedDiscountSKUs).Count() > 1;
        }
    }
}
