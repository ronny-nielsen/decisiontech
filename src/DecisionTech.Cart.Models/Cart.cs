using System;
using System.Collections.Generic;

namespace DecisionTech.Cart.Models
{
    public class Cart
    {
        public string Id { get; } = Guid.NewGuid().ToString();

        public IList<CartItem> Items { get; set; } = new List<CartItem>();
    }
}