using FluentAssertions;
using NUnit.Framework;
using PromotionEngine.Models;
using System.Linq;

namespace PromotionEngine.UnitTests
{
    public class CartTests
    {
        [SetUp]
        public void Setup()
        {
        }

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
    }
}