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
        Cart cart;

        [SetUp]
        public void Setup()
        {
            cart = new Cart();
        }

        [Test]
        public void CartCanAddItems()
        {
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
            // 1 * A   50
            // 1 * B   30
            // 1 * C   20
            // Total = 100

            var allActivePromos = GetAllActivePromotions();

            Product productA = new("A", 50);
            Product productB = new("B", 30);
            Product productC = new("C", 20);

            cart.AddToCart(productA, 1);
            cart.AddToCart(productB, 1);
            cart.AddToCart(productC, 1);

            var totalPrice = cart.CalculateTotalPrice(allActivePromos);

            totalPrice.Should().Be(100);
        }

        [Test]
        public void Calculate_Total_Price_Scenario_B()
        {
            // Scenario	B	
            // 5 * A   130 + 2*50
            // 5 * B   45 + 45 + 30
            // 1 * C   20
            // Total   370

            var allActivePromos = GetAllActivePromotions();

            Product productA = new("A", 50);
            Product productB = new("B", 30);
            Product productC = new("C", 20);

            cart.AddToCart(productA, 5);
            cart.AddToCart(productB, 5);
            cart.AddToCart(productC, 1);

            var totalPrice = cart.CalculateTotalPrice(allActivePromos);

            totalPrice.Should().Be(370);
        }

        [Test]
        public void Calculate_Total_Price_Scenario_C()
        {
            // Scenario C
            // 3 * A    130
            // 5 * B    45 + 45 + 1 * 30
            // 1 * C	-
            // 1 * D    30
            // Total    280

            var allActivePromos = GetAllActivePromotions();

            Product productA = new("A", 50);
            Product productB = new("B", 30);
            Product productC = new("C", 20);
            Product productD = new("D", 15);

            cart.AddToCart(productA, 3);
            cart.AddToCart(productB, 5);
            cart.AddToCart(productC, 1);
            cart.AddToCart(productD, 1);

            var totalPrice = cart.CalculateTotalPrice(allActivePromos);

            totalPrice.Should().Be(280);
        }

        [Test]
        public void Calculate_Total_Price_Promotion_Mutually_Exclusive_ForSKU()
        {
            // The promotion rules are mutually exclusive, that implies only one promotion (individual SKU or combined) is applicable to a particular SKU

            // 4 * A   130 + 50
            // 1 * B   30
            // Total = 210

            // 3 of A's for 130
            IPromotionRule nItemsForFixedPriceA = new NItemsForFixedPrice("A", 3, 130);

            // A & B for 70
            // (This promotion should not get applied, because the previous promotion for A already got applied for A.)
            List<string> comboPromoSkus = new() { "A", "B" };
            IPromotionRule combinedItemFixedPrice = new CombinedItemFixedPrice(comboPromoSkus, 70);

            List<IPromotionRule> activePromotions = new()
            {
                nItemsForFixedPriceA,
                combinedItemFixedPrice
            };

            Product productA = new("A", 50);
            Product productB = new("B", 30);

            cart.AddToCart(productA, 4);
            cart.AddToCart(productB, 1);

            var totalPrice = cart.CalculateTotalPrice(activePromotions);

            totalPrice.Should().Be(210);
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