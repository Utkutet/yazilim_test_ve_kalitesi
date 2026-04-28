namespace ECommerceApp.Core
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; } // BUG 1: Fiyatın negatif olmasını engelleyen bir kontrol yok.
        public int Stock { get; set; }
    }
}