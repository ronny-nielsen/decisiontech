namespace DecisionTech.Cart.Abstractions
{
    public interface IDiscountService
    {
        void Execute(Models.Cart cart);
    }
}