using System.Collections.ObjectModel;
using System.Linq;

namespace POSSystem.Models
{
    public class Bill
    {
        public int Id   { get; set; }
        public string Table { get; set; } = string.Empty;

        public ObservableCollection<CartItem> Items { get; } = new();
        public double Total => Items.Sum(i => i.Total);
    }
}