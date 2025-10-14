using System.ComponentModel;

namespace E_shopLib
{
    public class Product
    {
        [DisplayName("Артикул")]
        public string Article { get; set; }

        [DisplayName("Наименование")]
        public string Name { get; set; }

        [DisplayName("Категория")]
        public string Category { get; set; }

        [DisplayName("Цена")]
        public decimal Price { get; set; }

        [DisplayName("Количество на складе")]
        public int Stock { get; set; }

        [DisplayName("Единица измерения")]
        public string Unit { get; set; }

        public Product() { }

        public Product(string article)
        {
            Article = article;
        }
    }
}