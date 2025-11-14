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
                            string invoiceQuery = @"INSERT INTO Invoice (SerialNumber,Date) VALUES (@SerialNumber,@Date)";
                            int invoiceId;

                            using (MySqlCommand invoiceCommand = new MySqlCommand(invoiceQuery, conn, transaction))
                            {
                                invoiceCommand.Parameters.AddWithValue("@SerialNumber", invoice.SerialNumber);
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

                                string itemQuery = @"INSERT INTO InvoiceItems (ID_Invoice, Article, Name,Category,Unit, Quantity, Price) 
                                                   VALUES (@ID_Invoice, @Article, @Name,@Category,@Unit, @Quantity, @Price)";

                                using (MySqlCommand itemCommand = new MySqlCommand(itemQuery, conn, transaction))
                                {
                                    itemCommand.Parameters.AddWithValue("@ID_Invoice", invoiceId);
                                    itemCommand.Parameters.AddWithValue("@Article", product.Article);
                                    itemCommand.Parameters.AddWithValue("@Name", product.Name);
                                    itemCommand.Parameters.AddWithValue("@Category", product.Category);
                                    itemCommand.Parameters.AddWithValue("@Unit", product.Unit);
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
            using (MySqlConnection conn = new MySqlConnection(E_shopLib.AppSettings.ConnectionString))
            {
                try
                {
                    conn.Open();

                    string invoiceQuery = @"SELECT ID_Invoice, SerialNumber, Date 
                                  FROM Invoice 
                                  WHERE ID_Invoice = @ID_Invoice";

                    Invoice invoice = null;

                    using (MySqlCommand command = new MySqlCommand(invoiceQuery, conn))
                    {
                        command.Parameters.AddWithValue("@ID_Invoice", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                invoice = new Invoice();
                                invoice.SetId(reader.GetInt32("ID_Invoice"));
                                invoice.SerialNumber = reader.GetString("SerialNumber");
                                invoice.Date = reader.GetDateTime("Date");
                                invoice.Items = new List<Product>();
                            }
                        }
                    }

                    if (invoice == null) return null;

                    string itemsQuery = @"SELECT Article, Name, Category, Unit, Quantity, Price 
                                FROM InvoiceItems 
                                WHERE ID_Invoice = @ID_Invoice";

                    using (MySqlCommand command = new MySqlCommand(itemsQuery, conn))
                    {
                        command.Parameters.AddWithValue("@ID_Invoice", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Product product = new Product
                                {
                                    Article = reader.GetString("Article"),
                                    Name = reader.GetString("Name"),
                                    Category = reader.GetString("Category"),
                                    Unit = reader.GetString("Unit"),
                                    Stock = reader.GetInt32("Quantity"),
                                    Price = reader.GetDecimal("Price")
                                };

                                invoice.Items.Add(product);
                            }
                        }
                    }

                    return invoice;
                }
                catch (MySqlException ex)
                {
                    throw new Exception($"Ошибка при загрузке накладной: {ex.Message}");
                }
            }

        }
        public List<Invoice> GetAllInvoices()
        {
            List<Invoice> invoices = new List<Invoice>();

            using (MySqlConnection conn = new MySqlConnection(E_shopLib.AppSettings.ConnectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"SELECT i.ID_Invoice, i.SerialNumber, i.Date 
                           FROM Invoice i 
                           ORDER BY i.Date DESC, i.ID_Invoice DESC";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Invoice invoice = new Invoice();
                            invoice.SetId(reader.GetInt32("ID_Invoice"));
                            invoice.SerialNumber = reader.GetString("SerialNumber");
                            invoice.Date = reader.GetDateTime("Date");

                            invoices.Add(invoice);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception($"Ошибка при загрузке накладных: {ex.Message}");
                }
            }

            return invoices;
        }
    }
}
