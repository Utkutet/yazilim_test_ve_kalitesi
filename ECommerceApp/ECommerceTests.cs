using NUnit.Framework;
using ECommerceApp.Core;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceApp.Tests
{
    [TestFixture]
    public class ECommerceTests
    {
        private Cart _cart;
        private OrderService _orderService;
        private Product _product;

        [SetUp]
        public void Setup()
        {
            _cart = new Cart();
            _orderService = new OrderService();
            _product = new Product { Id = 1, Name = "Laptop", Price = 15000, Stock = 5 };
        }

        // --- 1. UNIT TEST (WHITE BOX) ---
        [Test]
        public void UnitTest_ProductPrice_ShouldNotBeNegative_Fails()
        {
            var p = new Product { Id = 2, Name = "Mouse", Price = -100 };
            // YENİ NUNIT 4 KULLANIMI:
            Assert.That(p.Price > 0, Is.True, "Ürün fiyatı 0'dan büyük olmalıdır!");
        }

        [Test]
        public void UnitTest_CartClear_ShouldRemoveAllItems_Passes()
        {
            _cart.AddProduct(_product);
            _cart.ClearCart();
            Assert.That(_cart.GetItemCount(), Is.EqualTo(0));
        }

        [Test]
        public void UnitTest_AddProduct_ShouldIncreaseItemCount_Passes()
        {
            _cart.AddProduct(_product);
            Assert.That(_cart.GetItemCount(), Is.EqualTo(1));
        }

        // --- 2. BLACK BOX TEST ---
        [Test]
        public void BlackBox_CartTotalPrice_ShouldBeCorrect_Fails()
        {
            _cart.AddProduct(new Product { Price = 100 });
            _cart.AddProduct(new Product { Price = 200 });
            Assert.That(_cart.GetTotalPrice(), Is.EqualTo(300), "Sepet toplamı yanlış hesaplandı!");
        }

        [Test]
        public void BlackBox_EmptyCart_ShouldReturnZeroTotal_Passes()
        {
            Assert.That(_cart.GetTotalPrice(), Is.EqualTo(0));
        }

        // --- 3. GRAY BOX TEST ---
        [Test]
        public void GrayBox_OrderWithEmptyCart_ShouldNotBePlaced_Fails()
        {
            bool result = _orderService.PlaceOrder(_cart);
            Assert.That(result, Is.False, "Boş sepet ile sipariş verilememeli!");
        }

        [Test]
        public void GrayBox_SuccessfulOrder_ShouldClearCart_Passes()
        {
            _cart.AddProduct(_product);
            _orderService.PlaceOrder(_cart);
            Assert.That(_cart.GetItemCount(), Is.EqualTo(0));
        }

        [Test]
        public void GrayBox_ProcessPayment_WithNegativeAmount_ShouldFail_Fails()
        {
            bool paymentResult = _orderService.ProcessPayment(-50);
            Assert.That(paymentResult, Is.False, "Negatif tutar ile ödeme alınmamalıdır!");
        }

        // --- 4. INTEGRATION TEST ---
        [Test]
        public void Integration_FullCheckoutFlow_ShouldCompleteSuccessfully_Passes()
        {
            _cart.AddProduct(_product);
            bool isOrderPlaced = _orderService.PlaceOrder(_cart);

            Assert.That(isOrderPlaced, Is.True);
            Assert.That(_cart.GetItemCount(), Is.EqualTo(0));
        }

        [Test]
        public void Integration_AddMultipleProducts_AndCheckTotal_Fails()
        {
            _cart.AddProduct(new Product { Price = 50 });
            _cart.AddProduct(new Product { Price = 50 });

            Assert.That(_cart.GetTotalPrice(), Is.EqualTo(100), "Integration: Sepet ve ödeme tutarları eşleşmiyor.");
        }
    }
}