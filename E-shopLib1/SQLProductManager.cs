using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace E_shopLib
{
    public class SQLProductManager : IProductRepository
    {
        public List<Product> GetAllProducts()
        {
            List<Product> result = new List<Product>();

            try
            {
                conn = new MySqlConnection(MyConnectionString);
                conn.Open();
                const string quary = "SELECT Артикул_Товара, Наименование, Категория, Цена, Остаток, Ед_измерения from товар;";
                MySqlCommand command = new MySqlCommand(quary, conn);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string Article = reader.GetString("Артикул_Товара");

                        Product product = new Product(Article);
                        product.Name = reader.GetString("Наименование");
                        product.Category = reader.GetString("Категория");
                        product.Price = reader.GetDecimal("Цена");
                        product.Stock = reader.GetInt32("Остаток");
                        product.Unit = reader.GetString("Ед_измерения");
                        result.Add(product);
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Ошибка при загрузке товаров: " + ex.Message);
            }
            return result;
        }
        public Product GetProductByArticle(string article)
        {
            using (MySqlConnection conn = new MySqlConnection(MyConnectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT Артикул_Товара, Наименование, Категория, Цена, Остаток, Ед_измерения FROM товар WHERE Артикул_Товара = @Article";
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Article", article);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Product(reader.GetString("Артикул_Товара"))
                                {
                                    Name = reader.GetString("Наименование"),
                                    Category = reader.GetString("Категория"),
                                    Price = reader.GetDecimal("Цена"),
                                    Stock = reader.GetInt32("Остаток"),
                                    Unit = reader.GetString("Ед_измерения")
                                };
                            }
                        }
                        return null;
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Ошибка при поиске товара: " + ex.Message);
                }
            }
        }
        public bool ArticleExists(string article)
        {
            using (MySqlConnection conn = new MySqlConnection(MyConnectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM товар WHERE Артикул_Товара = @Article";
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Article", article);

                        long count = (long)command.ExecuteScalar();
                        return count > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Ошибка при проверке артикула: " + ex.Message);
                }
            }
        }

    }
}