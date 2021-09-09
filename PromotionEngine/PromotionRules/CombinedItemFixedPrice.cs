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
        public decimal FixedPrice { get; set; }

        /// <summary>
        /// SKU of product 1 combo offer
        /// </summary>
        public string SKU1 { get; set; }

        /// <summary>
        /// SKU of product 2 combo offer
        /// </summary>
        public string SKU2 { get; set; }

        public decimal CalculateDiscount(Cart cart)
        {
            throw new NotImplementedException();
        }

        public bool IsApplicable(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}
