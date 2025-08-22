namespace POSSystem.Models
{
    public class Product
    {
        public string Name     { get; set; } = string.Empty;
        public double  Price   { get; set; }
        public string  Category{ get; set; } = string.Empty;
        public string  CategoryType { get; set; } = string.Empty; // "Foods" or "Beverages"
        public string  ImagePath { get; set; } = "pack://application:,,,/Images/placeholder.png";
    }
}