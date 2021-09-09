using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.PromotionRules
{
    public class CombinedItemFixedPrice : IPromotionRule
    {
        public CombinedItemFixedPrice(IEnumerable<string> skus, decimal fixedPrice)
        {
            if(skus?.Count() < 2) throw new ArgumentException("Atleast 2 SKUs must be provided for this promotion");

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
            if (IsApplicable(cart))
            {
                var applicableProductsInCart = cart.CartItems
                    .Where(item => this.CombinedDiscountSKUs.Contains(item.Product.SKU));

                var numberOfBundles = applicableProductsInCart
                    .GroupBy(item => item.Product.SKU).Min(x => x.Count());

                var originalPricePerBundle = this.CombinedDiscountSKUs
                    .Sum(sku => cart.CartItems.First(cartItem => cartItem.Product.SKU == sku).Product.Price);

                var totalDiscount = (originalPricePerBundle - FixedPrice) * numberOfBundles;

                cart.PromotionAppliedSKUs.UnionWith(CombinedDiscountSKUs);

                return totalDiscount;
            }

            return default;
        }

        public bool IsApplicable(Cart cart)
        {
            return cart.CartItems.Where(x => !cart.PromotionAppliedSKUs.Contains(x.Product.SKU))
                .Select(item => item.Product.SKU).Intersect(CombinedDiscountSKUs).Count() > 1;
        }
    }
}
