namespace ECommerceApp.Core
{
    public class Cart
    {
        private List<Product> _products = new List<Product>();

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public int GetItemCount()
        {
            return _products.Count;
        }

        public decimal GetTotalPrice()
        {
            // BUG 2: Sepet toplamını hesaplarken bilerek her zaman 10 TL eksik hesaplıyor.
            decimal total = _products.Sum(p => p.Price);
            return total > 10 ? total - 10 : total;
        }

        public void ClearCart()
        {
            _products.Clear();
        }

        public List<Product> GetProducts() => _products;
    }
}