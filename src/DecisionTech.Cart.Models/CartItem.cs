﻿namespace DecisionTech.Cart.Models
{
    public class CartItem
    {
        public int CartId { get; set; }

        public Cart Cart { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public decimal Discount { get; set; }

        public int Quantity { get; set; }
    }
}