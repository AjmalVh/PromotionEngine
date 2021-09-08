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

    }
}