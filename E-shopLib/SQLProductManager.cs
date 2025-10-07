using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace E_shopLib
{
    public class SQLProductManager : IProductRepository
    {
        private MySqlConnection conn;
        private string MyConnectionString = "server=127.0.0.1;uid=root;pwd=vertrigo;database=users;";

        public void AddProduct(Product product)
        {
            try
            {
                conn = new MySqlConnection(MyConnectionString);
                conn.Open();

                string query = @"INSERT INTO товар 
                                (Артикул_Товара, Наименование, Категория, Цена, Остаток, Ед_измерения) 
                                VALUES (@Article, @Name, @Category, @Price, @Stock, @Unit)";

                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@Article", product.Article);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Category", product.Category);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Stock", product.Stock);
                command.Parameters.AddWithValue("@Unit", product.Unit);

                command.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                throw new Exception("Ошибка при добавлении товара: " + ex.Message);
            }
            finally
            {
                conn?.Close();
            }
        }

        public string DeleteProduct(string article)
        {
            try
            {
                conn = new MySqlConnection(MyConnectionString);
                conn.Open();

                // Сначала проверяем существование товара
                if (!ArticleExists(article))
                {
                    return "Товар с указанным артикулом не найден";
                }

                string query = "DELETE FROM товар WHERE Артикул_Товара = @Article";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@Article", article);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0 ? string.Empty : "Не удалось удалить товар";
            }
            catch (MySqlException ex)
            {
                return "Ошибка при удалении товара: " + ex.Message;
            }
            finally
            {
                conn?.Close();
            }
        }

        public Product GetProductByArticle(string article)
        {
            try
            {
                conn = new MySqlConnection(MyConnectionString);
                conn.Open();

                string query = "SELECT Артикул_Товара, Наименование, Категория, Цена, Остаток, Ед_измерения FROM товар WHERE Артикул_Товара = @Article";
                MySqlCommand command = new MySqlCommand(query, conn);
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
            catch (MySqlException ex)
            {
                throw new Exception("Ошибка при поиске товара: " + ex.Message);
            }
            finally
            {
                conn?.Close();
            }
        }

        public bool ArticleExists(string article)
        {
            try
            {
                conn = new MySqlConnection(MyConnectionString);
                conn.Open();

                string query = "SELECT COUNT(*) FROM товар WHERE Артикул_Товара = @Article";
                MySqlCommand command = new MySqlCommand(query, conn);
                command.Parameters.AddWithValue("@Article", article);

                long count = (long)command.ExecuteScalar();
                return count > 0;
            }
            catch (MySqlException ex)
            {
                throw new Exception("Ошибка при проверке артикула: " + ex.Message);
            }
            finally
            {
                conn?.Close();
            }
        }

        public List<Product> GetAllProducts()
        {
            List<Product> result = new List<Product>();

            try
            {
                conn = new MySqlConnection(MyConnectionString);
                conn.Open();

                const string query = "SELECT Артикул_Товара, Наименование, Категория, Цена, Остаток, Ед_измерения FROM товар";
                MySqlCommand command = new MySqlCommand(query, conn);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product(reader.GetString("Артикул_Товара"))
                        {
                            Name = reader.GetString("Наименование"),
                            Category = reader.GetString("Категория"),
                            Price = reader.GetDecimal("Цена"),
                            Stock = reader.GetInt32("Остаток"),
                            Unit = reader.GetString("Ед_измерения")
                        };
                        result.Add(product);
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Ошибка при загрузке товаров: " + ex.Message);
            }
            finally
            {
                conn?.Close();
            }
            return result;
        }
    }
}