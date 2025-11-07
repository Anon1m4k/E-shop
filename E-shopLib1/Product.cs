using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace E_shopLib
{
    public class Product : INotifyPropertyChanged
    {
        private string article;
        private string name;
        private string category;
        private decimal price;
        private int stock;
        private string unit;

        [DisplayName("Артикул")]
        public string Article
        {
            get { return article; }
            set
            {
                article = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Наименование")]
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Категория")]
        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Цена")]
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Количество на складе")]
        public int Stock
        {
            get { return stock; }
            set
            {
                stock = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Единица измерения")]
        public string Unit
        {
            get { return unit; }
            set
            {
                unit = value;
                OnPropertyChanged();
            }
        }

        public Product() { }

        public Product(string article)
        {
            Article = article;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Метод для копирования товара
        public Product Clone()
        {
            return new Product
            {
                Article = this.Article,
                Name = this.Name,
                Category = this.Category,
                Price = this.Price,
                Stock = this.Stock,
                Unit = this.Unit
            };
        }
    }
}