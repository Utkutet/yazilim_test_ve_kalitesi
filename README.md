#E-Ticaret Sistemi - Yazılım Test ve Kalite Projesi

Bu proje, yazılım test süreçlerini ve **NUnit** framework'ünün kullanımını uygulamalı olarak göstermek amacıyla geliştirilmiş bir e-ticaret simülasyonudur. Sistemde **kasıtlı olarak bırakılmış mantık hataları (bug'lar)** bulunmakta olup, yazılan Unit, Black Box, Gray Box ve Integration testleri ile bu hataların nasıl tespit edildiği raporlanmıştır.
------------------------------------------------------------
Sisteme Kasıtlı Eklenen Zafiyetler (Bugs)
Sistemin test edilebilirliğini ölçmek için Core sınıflarına aşağıdaki mantıksal hatalar enjekte edilmiştir:

Negatif Fiyat Açığı (Product.cs): Ürün fiyatına 0'dan küçük değerler girilmesi engellenmemiştir.

Sepet Hesaplama Hatası (Cart.cs): Sepet toplamı hesaplanırken tutardan her zaman 10 TL eksiltilmektedir.

Boş Sipariş Onayı (OrderService.cs): Sepet boş olsa dahi sipariş oluşturma sürecine onay verilmektedir.

Ödeme Doğrulama Zafiyeti (OrderService.cs): Ödeme sistemi, tutar negatif olsa bile her zaman true (başarılı) dönmektedir.

-----------------------------------------------------------------------------------------------------------
Test Raporu ve Analizi
Toplamda 10 adet test senaryosu yazılmıştır. Sağlıklı çalışan metotlar testi başarıyla geçerken (🟢 Pass), kasıtlı bırakılan hatalar NUnit tarafından başarıyla yakalanmış ve testlerin başarısız olmasına (🔴 Fail) neden olmuştur.

🔴 BAŞARISIZ (FAIL) OLAN TESTLER - [Kasıtlı Hataların Yakalandığı Senaryolar]
UnitTest_ProductPrice_ShouldNotBeNegative_Fails [White Box]

Yakalanan Bug 1: Ürün fiyatına negatif değer girişi engellenmemiş. Test bu açığı başarıyla tespit etti.

BlackBox_CartTotalPrice_ShouldBeCorrect_Fails [Black Box]

Yakalanan Bug 2: Sepet toplamı hesaplanırken tutardan kasıtlı olarak 10 TL eksiltildiği NUnit tarafından yakalandı.

GrayBox_OrderWithEmptyCart_ShouldNotBePlaced_Fails [Gray Box]

Yakalanan Bug 3: Sepet tamamen boş olmasına rağmen sistemin siparişi onaylama zafiyeti tespit edildi.

GrayBox_ProcessPayment_WithNegativeAmount_ShouldFail_Fails [Gray Box]

Yakalanan Bug 4: Ödeme sistemi, gelen tutarı doğrulamadan (negatif bile olsa) her zaman başarılı true döndürüyor. Test bu işlemi reddetti.

Integration_AddMultipleProducts_AndCheckTotal_Fails [Integration]

Yakalanan Entegrasyon Hatası: Sınıflar birleştiğinde sepetin yanlış hesaplama yapması, ödeme sistemine de hatalı veri gitmesine sebep oldu ve entegrasyon testte patladı.

.

🟢 BAŞARILI (PASS) OLAN TESTLER
✔️ UnitTest_CartClear_ShouldRemoveAllItems_Passes [White Box] ➔ Sepeti temizleme işlemi kusursuz çalışıyor.

✔️ UnitTest_AddProduct_ShouldIncreaseItemCount_Passes [White Box] ➔ Ürün eklendiğinde sepet içerisindeki eleman sayısı doğru artıyor.

✔️ BlackBox_EmptyCart_ShouldReturnZeroTotal_Passes [Black Box] ➔ Boş sepetin toplam değeri beklendiği gibi 0 dönüyor.

✔️ GrayBox_SuccessfulOrder_ShouldClearCart_Passes [Gray Box] ➔ Sipariş başarıyla tamamlandığında sepet otomatik olarak boşaltılıyor.

✔️ Integration_FullCheckoutFlow_ShouldCompleteSuccessfully_Passes [Integration] ➔ Ürün ekleme, hesaplama ve sipariş onayının uçtan uca akışı senkronize çalışıyor.

------------------------------
Not: Testlerin 5 tanesinin "Failed" olarak sonuçlanması beklenen bir durumdur ve test kodlarının kasıtlı hataları (bug) başarıyla yakaladığını kanıtlar.