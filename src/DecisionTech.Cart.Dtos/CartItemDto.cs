namespace DecisionTech.Cart.Dtos
{
    public class CartItemDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Discount { get; set; }

        public decimal Cost { get; set; }

        public decimal Total => Quantity * Cost;
    }
}