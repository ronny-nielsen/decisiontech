using DecisionTech.Cart.Abstractions;
using DecisionTech.Cart.Dtos;

namespace DecisionTect.Cart
{
    public class CartService : ICartService
    {
        public CartDto Get()
        {
            throw new System.NotImplementedException();
        }

        public DecisionTech.Cart.Models.Cart GetModel()
        {
            throw new System.NotImplementedException();
        }

        public CommandResult<CartDto> AddItem(DecisionTech.Cart.Models.Cart cart, CartRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}