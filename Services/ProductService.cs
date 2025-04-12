using System.Text.Json;
using KutezClient.Models;

namespace KutezClient.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            // BaseAddress tanımlandığı için sadece relative path kullanıyoruz
            var response = await _httpClient.GetAsync("api/products");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var products = JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return products ?? new List<Product>();
        }
    }
}
