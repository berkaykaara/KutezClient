/**
 * Ürün rengini değiştirmek için kullanılır.
 * Görseli fade-in efektiyle günceller ve renk adını gösterir.
 * 
 * @param {string} imgId - Görselin ID'si
 * @param {string} colorNameId - Renk adının gösterileceği elementin ID'si
 * @param {string} newSrc - Yeni görselin kaynak yolu
 * @param {string} colorName - Yeni seçilen renk adı
 */
function selectColor(imgId, colorNameId, newSrc, colorName) {
    const img = document.getElementById(imgId);
    if (img) {
        img.classList.remove('fade-in'); // Animasyonu sıfırla
        void img.offsetWidth; // Reflow tetikleyerek animasyonu yeniden başlat
        img.src = newSrc; // Yeni görseli ayarla
        img.classList.add('fade-in'); // Fade-in animasyonu ekle
    }

    const label = document.getElementById(colorNameId);
    if (label) label.textContent = colorName; // Renk adını güncelle
}

/**
 * Carousel’i sağa veya sola kaydırır.
 * Eğer sonuna gelinirse başa döner, başındaysa sona gider.
 * 
 * @param {number} direction - Kaydırma yönü (1: sağ, -1: sol)
 */
function scrollCarousel(direction) {
    const container = document.getElementById("carousel");
    const track = container.querySelector(".carousel-track");
    const cards = track.querySelectorAll(".product-card");

    const cardWidth = cards[0].offsetWidth + 20; // Kart genişliği + gap
    const maxScroll = track.scrollWidth - container.clientWidth; // Scroll limit
    const currentScroll = container.scrollLeft;

    let newScroll = currentScroll + direction * cardWidth; // Yeni scroll pozisyonu

    // Scroll aralığını aşarsa başa veya sona sar
    if (newScroll > maxScroll) {
        newScroll = 0;
    }
    if (newScroll < 0) {
        newScroll = maxScroll;
    }

    // Scroll animasyonu ile uygula
    container.scrollTo({ left: newScroll, behavior: "smooth" });
}

/**
 * Filtre bölümünü aç/kapat yapar.
 */
function toggleFilter() {
    const section = document.getElementById("filter-section");
    section.style.display = section.style.display === "none" ? "block" : "none";
}

/**
 * Sayfa yüklendiğinde URL parametrelerine göre filtre bölümünü göster/gizle.
 * Filtre parametreleri yoksa filtreyi gizler.
 */
window.addEventListener("DOMContentLoaded", () => {
    const urlParams = new URLSearchParams(window.location.search);

    // Filtre parametrelerinden herhangi biri varsa filtre açık kalsın
    const hasFilters = urlParams.has("minPrice") || urlParams.has("maxPrice") || urlParams.has("minRating") || urlParams.has("sortBy");

    // Filtre yoksa bölümü gizle
    if (!hasFilters) {
        document.getElementById("filter-section").style.display = "none";
    }
});
