using DecisionTech.Cart.Abstractions;
using DecisionTech.Cart.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace DecisionTech.Cart
{
    public class CartService : ICartService
    {
        private readonly IList<Models.Product> _products = new List<Models.Product>
        {
            new Models.Product
                {
                    Id = 1,
                    Name = "Butter",
                    Cost = .8M
                },
                new Models.Product
                {
                    Id = 2,
                    Name = "Milk",
                    Cost = 1.15M
                },
                new Models.Product
                {
                    Id = 3,
                    Name = "Bread",
                    Cost = 1M
                },
        };

        private readonly Models.Cart _cart = new Models.Cart();
        private readonly List<IDiscountService> _discounts;

        public CartService(List<IDiscountService> discounts)
        {
            _discounts = discounts ?? throw new System.ArgumentNullException(nameof(discounts));
        }

        public CartDto Get()
        {
            var cart = GetModel();
            return ConvertToDto(cart);
        }

        public Models.Cart GetModel()
        {
            return _cart;
        }

        public CommandResult<CartDto> AddItem(Models.Cart cart, CartRequest request)
        {
            var result = new CommandResult<CartDto>();

            if (request == null || !request.ProductId.HasValue)
            {
                result.Errors.Add("The ProductId field is required.");
                return result;
            }

            var product = _products.FirstOrDefault(x => x.Id == request.ProductId);
            if (product == null)
            {
                result.Errors.Add("The ProductId field is invalid.");
            }

            if (!result.Success) return result;

            var item = cart.Items.FirstOrDefault(x => x.ProductId == request.ProductId);
            if (item != null)
            {
                item.Quantity += request.Quantity;
            }
            else
            {
                cart.Items.Add(new Models.CartItem { Product = product, Quantity = request.Quantity });
            }

            _discounts.ForEach(x => x.Execute(cart));

            result.Model = ConvertToDto(cart);
            return result;
        }

        private static CartDto ConvertToDto(Models.Cart cart)
        {
            if (cart == null) return null;

            return new CartDto
            {
                Id = cart.Id,
                Items = cart.Items.Select(x => ConvertItemToDto(x)).ToList()
            };
        }

        private static CartItemDto ConvertItemToDto(Models.CartItem item)
        {
            if (item == null) return null;

            return new CartItemDto
            {
                Cost = item.Product != null ? item.Product.Cost : 0,
                ProductId = item.Product != null ? item.Product.Id : 0,
                ProductName = item.Product?.Name,
                Quantity = item.Quantity,
                Discount = item.Discount
            };
        }
    }
}