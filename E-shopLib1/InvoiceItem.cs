
using System.ComponentModel;


namespace E_shopLib1
{
    public class InvoiceItem : INotifyPropertyChanged
    {
        private string _article;
        private string _name;
        private string _category;
        private int _quantity;
        private string _unit;
        private decimal _price;

        [DisplayName("Артикул")]
        public string Article
        {
            get { return _article; }
            set
            {
                _article = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Наименование")]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Категория")]
        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Количество")]
        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Total)); 
            }
        }

        [DisplayName("Единица измерения")]
        public string Unit
        {
            get { return _unit; }
            set
            {
                _unit = value;
                OnPropertyChanged();
            }
        }

        [DisplayName("Цена")]
        public decimal Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Total)); 
            }
        }

        [DisplayName("Сумма")]
        public decimal Total => Price * Quantity;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}