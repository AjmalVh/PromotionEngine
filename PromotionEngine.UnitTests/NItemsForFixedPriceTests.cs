using FluentAssertions;
using NUnit.Framework;
using PromotionEngine.Models;
using PromotionEngine.PromotionRules;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.UnitTests
{
    public class NItemsForFixedPriceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PromotionApplicable_If_Already_Applied_ForThis_SKU_ShouldBe_False()
        {
            Cart cart = new();

            Product productA = new("A", 50);
            cart.AddToCart(productA, 5);

            cart.PromotionAppliedSKUs = new List<string>
            {
                "A", "C"
            };

            IPromotionRule nItemsForFixedPricePromo = new NItemsForFixedPrice("A", 120, 3);

            nItemsForFixedPricePromo.IsApplicable(cart).Should().BeFalse();
        }

        [Test]
        public void PromotionApplicable_If_Cart_Has_LessThan_QuantityRequiredForPromo_ShouldBe_False()
        {
            Cart cart = new();

            Product productA = new("A", 50);
            cart.AddToCart(productA, 2);

            IPromotionRule nItemsForFixedPricePromo = new NItemsForFixedPrice("A", 120, 3);

            nItemsForFixedPricePromo.IsApplicable(cart).Should().BeFalse();
        }

        [Test]
        public void PromotionApplicable_If_Cart_Has_QuantityRequiredForPromo_ShouldBe_True()
        {
            Cart cart = new();

            Product productA = new("A", 50);
            cart.AddToCart(productA, 3);

            cart.PromotionAppliedSKUs = new List<string>
            {
                "B", "C"
            };

            IPromotionRule nItemsForFixedPricePromo = new NItemsForFixedPrice("A", 3, 130);

            nItemsForFixedPricePromo.IsApplicable(cart).Should().BeTrue();
        }

        [Test]
        public void CalculateDiscount()
        {
            // Unit price for A = 50
            // 3 of A's for 130

            IPromotionRule nItemsForFixedPrice = new NItemsForFixedPrice("A", 3, 130);

            Cart cart = new();

            Product productA = new("A", 50);
            cart.AddToCart(productA, 3);

            var discount = nItemsForFixedPrice.CalculateDiscount(cart);
            discount.Should().Be(20);
        }

        [Test]
        public void CalculateDiscount_Extra_Items_Involved()
        {
            // Unit price for A = 50
            // Offer: 3 of A's for 130
            // Cart: 8A

            IPromotionRule nItemsForFixedPrice = new NItemsForFixedPrice("A", 3, 130);

            Cart cart = new();

            Product productA = new("A", 50);
            cart.AddToCart(productA, 8);

            var discount = nItemsForFixedPrice.CalculateDiscount(cart);
            discount.Should().Be(40);
        }

    }
}