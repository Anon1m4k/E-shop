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
        MySqlConnection conn;
        public List<Product> GetAllProducts()
        {
            List<Product> result = new List<Product>();

            try
            {
                conn = new MySqlConnection(AppSettings.ConnectionString);
                conn.Open();
                const string query = "SELECT Article, Name, Category, Price, Stock, Unit from Product;";
                MySqlCommand command = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string Article = reader.GetString("Article");

                        Product product = new Product(Article);
                        product.Name = reader.GetString("Name");
                        product.Category = reader.GetString("Category");
                        product.Price = reader.GetDecimal("Price");
                        product.Stock = reader.GetInt32("Stock");
                        product.Unit = reader.GetString("Unit");
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

        public string AddProduct(Product product)
        {
            using (MySqlConnection conn = new MySqlConnection(AppSettings.ConnectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"INSERT INTO Product 
                                    (Article, Name, Category, Price, Stock, Unit) 
                                    VALUES (@Article, @Name, @Category, @Price, @Stock, @Unit)";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Article", product.Article);
                        command.Parameters.AddWithValue("@Name", product.Name);
                        command.Parameters.AddWithValue("@Category", product.Category);
                        command.Parameters.AddWithValue("@Price", product.Price);
                        command.Parameters.AddWithValue("@Stock", product.Stock);
                        command.Parameters.AddWithValue("@Unit", product.Unit);

                        command.ExecuteNonQuery();
                    }
                    return $"Товар успешно добавлен.";
                }
                catch (MySqlException ex)
                {
                    return "Ошибка при добавлении товара: " + ex.Message;
                }
            }

        public string DeleteProduct(string article)
        {
            using (MySqlConnection conn = new MySqlConnection(AppSettings.ConnectionString))
            {
                try
                {
                    conn.Open();

                    // Сначала проверяем существование товара
                    if (!ArticleExists(article))
                    {
                        return "Товар с указанным артикулом не найден";
                    }

                    string query = "DELETE FROM Product WHERE Article = @Article";
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Article", article);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0 ? string.Empty : "Не удалось удалить товар";
                    }
                }
                catch (MySqlException ex)
                {
                    return "Ошибка при удалении товара: " + ex.Message;
                }
            }
        }

        public Product GetProductByArticle(string article)
        {
            using (MySqlConnection conn = new MySqlConnection(AppSettings.ConnectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT Article,Name, Category, Price, Stock, Unit FROM Product WHERE Article = @Article";
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Article", article);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Product(reader.GetString("Article"))
                                {
                                    Name = reader.GetString("Name"),
                                    Category = reader.GetString("Category"),
                                    Price = reader.GetDecimal("Price"),
                                    Stock = reader.GetInt32("Stock"),
                                    Unit = reader.GetString("Unit")
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
            using (MySqlConnection conn = new MySqlConnection(AppSettings.ConnectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM Product WHERE Article = @Article";
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