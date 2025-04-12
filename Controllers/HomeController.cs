using KutezClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace KutezClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _productService;

        // ProductService dependency injection ile alýnýr
        public HomeController(ProductService productService)
        {
            _productService = productService;
        }

        // Index aksiyonu: filtreleme ve sýralama yapýlmýþ ürün listesini döndürür
        // Parametreler opsiyonel (nullable) olarak alýnýr
        public async Task<IActionResult> Index(double? minPrice, double? maxPrice, double? minRating, string? sortBy)
        {
            // API'den ürün verileri çekilir
            var products = await _productService.GetAllAsync();

            // Fiyat filtresi (minimum)
            if (minPrice.HasValue)
                products = products.Where(p => p.Price >= minPrice.Value).ToList();

            // Fiyat filtresi (maksimum)
            if (maxPrice.HasValue)
                products = products.Where(p => p.Price <= maxPrice.Value).ToList();

            // Puan filtresi (popülerlik skoru * 5 = yýldýzlý sistem gibi düþünülmüþ)
            if (minRating.HasValue)
                products = products.Where(p => p.PopularityScore * 5 >= minRating.Value).ToList();

            // Sýralama (sortBy parametresine göre)
            products = sortBy switch
            {
                "priceAsc" => products.OrderBy(p => p.Price).ToList(),               // Fiyata göre artan
                "priceDesc" => products.OrderByDescending(p => p.Price).ToList(),    // Fiyata göre azalan
                "ratingDesc" => products.OrderByDescending(p => p.PopularityScore).ToList(), // Puana göre azalan
                "ratingAsc" => products.OrderBy(p => p.PopularityScore).ToList(),    // Puana göre artan
                _ => products // sýralama yapýlmaz
            };

            // Sonuç View'e gönderilir
            return View(products);
        }
    }
}
