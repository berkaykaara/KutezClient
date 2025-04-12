namespace KutezClient.Models
{
    //Verilen json ürünlerine göre model ve özellikler
    public class Product
    {
        public string Name { get; set; }
        public double PopularityScore { get; set; }

        //fiyat hesabı yapabilmek için sonradan eklendi
        public double Price { get; set; }
        public double Weight { get; set; }
        public ProductImage Images { get; set; }
    }
}
