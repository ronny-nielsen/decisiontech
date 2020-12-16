using System.Collections.Generic;
using System.Linq;

namespace DecisionTech.Cart.Dtos
{
    public class CartDto
    {
        public string Id { get; set; }

        public IList<CartItemDto> Items { get; set; }

        public decimal SubTotal => Items.Sum(x => x.Total);

        public decimal Discount => Items.Sum(x => x.Discount);

        public decimal Total => SubTotal - Discount;
    }
}