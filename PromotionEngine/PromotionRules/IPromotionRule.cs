using PromotionEngine.Models;

namespace PromotionEngine.PromotionRules
{
    public interface IPromotionRule
    {
        /// <summary>
        /// Calculate applicable discount after applying a promotion
        /// </summary>
        /// <param name="cart"></param>
        /// <returns>Discounted price after applying this promotion rule</returns>
        public decimal CalculateDiscount(Cart cart);

        /// <summary>
        /// Check if this promotion is applicable for given cart
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public bool IsApplicable(Cart cart);
    }
}
