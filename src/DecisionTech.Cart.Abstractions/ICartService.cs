using DecisionTech.Cart.Dtos;

namespace DecisionTech.Cart.Abstractions
{
    public interface ICartService
    {
        CartDto Get();

        Models.Cart GetModel();

        CommandResult<CartDto> AddItem(Models.Cart cart, CartRequest request);
    }
}