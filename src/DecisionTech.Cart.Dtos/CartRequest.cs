﻿namespace DecisionTech.Cart.Dtos
{
    public class CartRequest
    {
        public int? ProductId { get; set; }

        public int Quantity { get; set; } = 1;
    }
}