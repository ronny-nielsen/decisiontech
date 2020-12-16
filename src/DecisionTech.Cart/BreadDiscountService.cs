using DecisionTech.Cart.Abstractions;
using System.Linq;

namespace DecisionTech.Cart
{
    public class BreadDiscountService : IBreadDiscountService
    {
        public void Execute(Models.Cart cart)
        {
            var bread = cart.Items.FirstOrDefault(x => x.Product.Name == "Bread");
            var butter = cart.Items.FirstOrDefault(x => x.Product.Name == "Butter");

            if (bread == null || butter == null) return;

            var breadCount = bread.Quantity;
            var butterCount = butter.Quantity;

            decimal discount = 0;
            for (var i = 0; butterCount > 1; i++)
            {
                if (butterCount >= 2 && breadCount >= 1)
                {
                    discount += discount + bread.Product.Cost * .5M;
                    butterCount -= 2;
                    breadCount -= 1;
                }
            }

            bread.Discount = discount;
        }
    }
}