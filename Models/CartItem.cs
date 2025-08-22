namespace POSSystem.Models
{
    public class CartItem
    {
        public Product Product { get; set; } = new();
        public int Quantity { get; set; } = 1;
        public double Total => Product.Price * Quantity;
    }
}