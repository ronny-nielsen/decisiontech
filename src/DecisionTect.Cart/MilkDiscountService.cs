using DecisionTech.Cart.Abstractions;
using System.Linq;

namespace DecisionTech.Cart
{
    public class MilkDiscountService : IMilkDiscountService
    {
        public void Execute(Models.Cart cart)
        {
            var milk = cart.Items.FirstOrDefault(x => x.Product?.Name == "Milk");
            if (milk == null) return;

            var milkCount = milk.Quantity;

            var occurences = milkCount / 4;
            decimal discount = occurences * milk.Product.Cost;

            milk.Discount = discount;
        }
    }
}