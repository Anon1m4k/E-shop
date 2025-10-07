namespace E_shopLib
{
    public class Product
    {
        public string Article { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Unit { get; set; }

        public Product() { }

        public Product(string article)
        {
            Article = article;
        }
    }
}