using KutezApi.Model; // Sunucudan gelecek Product modelini kullanmak için
using System.Text.Json;

namespace KutezClient.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        // HttpClient, DI (Dependency Injection) yoluyla enjekte edilir
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // API'den tüm ürünleri asenkron olarak çeker
        public async Task<List<Product>> GetAllAsync()
        {
            // API'ye GET isteği gönderiliyor (gerekirse URL ortam değişkeninden alınabilir)
            var response = await _httpClient.GetAsync("https://localhost:7039/api/Products"); // API adresini kendi adresinle değiştir

            // Eğer hata varsa exception fırlatır (hataları otomatik yakalayabiliriz)
            response.EnsureSuccessStatusCode();

            // JSON yanıtını string olarak al
            var json = await response.Content.ReadAsStringAsync();

            // JSON'u List<Product> nesnesine dönüştür (property isimleri büyük/küçük harfe duyarsız)
            var products = JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Eğer deserialization başarısızsa boş liste döndür
            return products ?? new List<Product>();
        }
    }
}
