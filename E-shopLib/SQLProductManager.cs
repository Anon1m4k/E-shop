using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_shop;
using MySql.Data.MySqlClient;

namespace E_shopLib
{
    public class SQLProductManager
    {
        MySqlConnection conn;
        string MyConnectionString = "server=127.0.0.1; uid=root;pwd=vertrigo; database=users;";
        public List<Product> ReadUsers()
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
                //MessageBox.Show(ex.Message);
                return result;
            }
            return result;
        }
    }
}