using System;
using System.Collections.Generic;
using E_shopLib;
using MySql.Data.MySqlClient;

namespace E_shopLib1
{
    public class SQLInvoiceRepository : IInvoiceRepository
    {
        public string AddInvoice(Invoice invoice)
        {
            using (MySqlConnection conn = new MySqlConnection(E_shopLib.AppSettings.ConnectionString))
            {
                try
                {
                    conn.Open();

                    using (var transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            string invoiceQuery = @"INSERT INTO Invoice (Date) VALUES (@Date)";
                            int invoiceId;

                            using (MySqlCommand invoiceCommand = new MySqlCommand(invoiceQuery, conn, transaction))
                            {
                                invoiceCommand.Parameters.AddWithValue("@Date", invoice.Date);
                                invoiceCommand.ExecuteNonQuery();
                                invoiceId = (int)invoiceCommand.LastInsertedId;
                            }

                            foreach (Product product in invoice.Items)
                            {
                                if (!ProductExists(conn, transaction, product.Article))
                                {
                                    CreateProductFromInvoice(conn, transaction, product);
                                }

                                string itemQuery = @"INSERT INTO InvoiceItems (ID_Invoice, Article, Name, Quantity, Price) 
                                                   VALUES (@ID_Invoice, @Article, @Name, @Quantity, @Price)";

                                using (MySqlCommand itemCommand = new MySqlCommand(itemQuery, conn, transaction))
                                {
                                    itemCommand.Parameters.AddWithValue("@ID_Invoice", invoiceId);
                                    itemCommand.Parameters.AddWithValue("@Article", product.Article);
                                    itemCommand.Parameters.AddWithValue("@Name", product.Name);
                                    itemCommand.Parameters.AddWithValue("@Quantity", product.Stock);
                                    itemCommand.Parameters.AddWithValue("@Price", product.Price);
                                    itemCommand.ExecuteNonQuery();
                                }
                                string updateQuery = @"UPDATE Product SET Stock = Stock + @Quantity 
                                                    WHERE Article = @Article";

                                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, conn, transaction))
                                {
                                    updateCommand.Parameters.AddWithValue("@Quantity", product.Stock);
                                    updateCommand.Parameters.AddWithValue("@Article", product.Article);
                                    updateCommand.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            return string.Empty; 
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            return $"Ошибка при сохранении накладной: {ex.Message}";
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    return $"Ошибка подключения к БД: {ex.Message}";
                }
            }
        }
        private bool ProductExists(MySqlConnection conn, MySqlTransaction transaction, string article)
        {
            string query = "SELECT COUNT(*) FROM Product WHERE Article = @Article";
            using (MySqlCommand command = new MySqlCommand(query, conn, transaction))
            {
                command.Parameters.AddWithValue("@Article", article);
                long count = (long)command.ExecuteScalar();
                return count > 0;
            }
        }
        private void CreateProductFromInvoice(MySqlConnection conn, MySqlTransaction transaction, Product product)
        {
            string createProductQuery = @"INSERT INTO Product 
                                        (Article, Name, Category, Price, Stock, Unit) 
                                        VALUES (@Article, @Name, @Category, @Price, @Stock, @Unit)";

            using (MySqlCommand createCommand = new MySqlCommand(createProductQuery, conn, transaction))
            {
                createCommand.Parameters.AddWithValue("@Article", product.Article);
                createCommand.Parameters.AddWithValue("@Name", product.Name);

                createCommand.Parameters.AddWithValue("@Category", product.Category); 
                createCommand.Parameters.AddWithValue("@Price", product.Price); 
                createCommand.Parameters.AddWithValue("@Stock", 0); 
                createCommand.Parameters.AddWithValue("@Unit", product.Unit); 

                createCommand.ExecuteNonQuery();
            }
        }
        public int GetNextInvoiceId()
        {
            using (MySqlConnection conn = new MySqlConnection(E_shopLib.AppSettings.ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COALESCE(MAX(ID_Invoice), 0) + 1 FROM Invoice";
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception($"Ошибка при получении следующего ID накладной: {ex.Message}");
                }
            }
        }
        public Invoice GetInvoiceById(int id)
        {
            return null;
        }
    }
}
