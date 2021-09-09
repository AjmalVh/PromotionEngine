using FluentAssertions;
using NUnit.Framework;
using PromotionEngine.Models;
using PromotionEngine.PromotionRules;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.UnitTests
{
    public class CartTests
    {

        [Test]
        public void CartCanAddItems()
        {
            Cart cart = new();

            Product productA = new("A", 50);
            Product productB = new("B", 30);

            cart.AddToCart(productA, 5);
            cart.AddToCart(productB, 5);

            cart.CartItems.Count.Should().Be(10);
            cart.CartItems.Where(item => item.Product.SKU == "B").Count().Should().Be(5);
        }

        [Test]
        public void Calculate_Total_Price_Scenario_A()
        {
            //Scenario A
            // 1 * A 50
            // 1 * B 30
            // 1 * C 20
            //Total = 100


            IPromotionRule nItemsForFixedPriceA = new NItemsForFixedPrice("A", 3, 130);

            IPromotionRule nItemsForFixedPriceB = new NItemsForFixedPrice("B", 2, 45);

            List<string> comboPromoSkus = new() { "C", "D" };
            IPromotionRule combinedItemFixedPrice = new CombinedItemFixedPrice(comboPromoSkus, 30);

            List<IPromotionRule> activePromotions = new()
            {
                nItemsForFixedPriceA,
                nItemsForFixedPriceB,
                combinedItemFixedPrice
            };

            Product productA = new("A", 50);
            Product productB = new("B", 30);
            Product productC = new("C", 20);

            Cart cart = new();
            cart.AddToCart(productA, 1);
            cart.AddToCart(productB, 1);
            cart.AddToCart(productC, 1);

            var totalPrice = cart.CalculateTotalPrice(activePromotions);

            totalPrice.Should().Be(100);
        }


        private IEnumerable<IPromotionRule> GetAllActivePromotions()
        {
            /*
            * Active Promotions
            * 3 of A's for 130
            * 2 of B's for 45 
            * C & D for 30
           */

            IPromotionRule nItemsForFixedPriceA = new NItemsForFixedPrice("A", 3, 130);

            IPromotionRule nItemsForFixedPriceB = new NItemsForFixedPrice("B", 2, 45);

            List<string> comboPromoSkus = new() { "C", "D" };
            IPromotionRule combinedItemFixedPrice = new CombinedItemFixedPrice(comboPromoSkus, 30);

            List<IPromotionRule> activePromotions = new()
            {
                nItemsForFixedPriceA,
                nItemsForFixedPriceB,
                combinedItemFixedPrice
            };

            return activePromotions;
        }
    }
}