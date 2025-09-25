namespace E_shop
{
    public class Product
    {
        public string Article { get; set; } // Уникальный артикул
        public string Name { get; set; } // Наименование
        public string Category { get; set; } // Категория (строка)
        public decimal Price { get; set; } // Цена продажи
        public int Stock { get; set; } // Текущий остаток
        public string Unit { get; set; } //Ед. измерения
    }
}