namespace ECommerceApp.Core
{
    public class OrderService
    {
        public bool ProcessPayment(decimal amount)
        {
            // BUG 3: Tutar 0 veya negatif olsa bile ödeme başarılı (true) dönüyor.
            return true;
        }

        public bool PlaceOrder(Cart cart)
        {
            // BUG 4: Sepet BOŞ olsa bile siparişi onaylıyor.
            bool paymentSuccess = ProcessPayment(cart.GetTotalPrice());

            if (paymentSuccess)
            {
                cart.ClearCart();
                return true;
            }
            return false;
        }
    }
}