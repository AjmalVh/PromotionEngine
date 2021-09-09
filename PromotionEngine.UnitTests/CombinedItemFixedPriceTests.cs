﻿using FluentAssertions;
using NUnit.Framework;
using PromotionEngine.Models;
using PromotionEngine.PromotionRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromotionEngine.UnitTests
{
    public class CombinedItemFixedPriceTests
    {
        [Test]
        public void PromotionApplicable_If_Already_Applied_ForThis_SKU_ShouldBe_False()
        {
            Cart cart = new();

            Product productC = new("C", 20);
            Product productD = new("D", 15);

            cart.AddToCart(productC, 1);
            cart.AddToCart(productD, 1);

            cart.PromotionAppliedSKUs = new List<string> { "A", "C" };

            List<string> combinedItemsPromoSkus = new() { "C", "D" };
            decimal fixedPrice = 30;

            CombinedItemFixedPrice combinedItemFixedPricePromo = new(combinedItemsPromoSkus, fixedPrice);

            combinedItemFixedPricePromo.IsApplicable(cart).Should().BeFalse();
        }

        [Test]
        public void PromotionApplicable_If_If_RequiredSKU_NotInCart_ShouldBe_False()
        {
            Cart cart = new();

            Product productB = new("B", 30);
            Product productD = new("D", 15);

            cart.AddToCart(productB, 1);
            cart.AddToCart(productD, 1);

            List<string> combinedItemsPromoSkus = new() { "C", "D" };
            decimal fixedPrice = 30;

            CombinedItemFixedPrice combinedItemFixedPricePromo = new(combinedItemsPromoSkus, fixedPrice);

            combinedItemFixedPricePromo.IsApplicable(cart).Should().BeFalse();
        }
    }
}