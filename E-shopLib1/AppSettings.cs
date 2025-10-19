using System;
using System.IO;
using System.Runtime.InteropServices;
using IniParser;
using IniParser.Model;


namespace E_shopLib
{
    public static class AppSettings
    {
        public static string Server { get; set; }
        public static string Database { get; set; }
        public static string UserId { get; set; }
        public static string Password { get; set; }
        public static string Port { get; set; }
        public static string Charset { get; set; }


        public static string ConnectionString
        {
            get
            {
                return $"Server={Server};Port={Port};Database={Database};Uid={UserId};Pwd={Password};Charset={Charset}";
            }
        }

        static AppSettings()
        {
            LoadSettings();
        }

        public static void LoadSettings()
        {
            string iniPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\config.ini"));

            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(iniPath);

            Server = data["Database"]["Server"];
            Database = data["Database"]["Database"];
            UserId = data["Database"]["UserId"];
            Password = data["Database"]["Password"];
            Port = data["Database"]["Port"];
            Charset = data["Database"]["Charset"];
        }

        public static (bool isValid, string errorMessage) AreSettingsValidWithDetails()
        {
            // Проверка заполненности полей
            if (string.IsNullOrWhiteSpace(Server))
                return (false, "Server не указан");
            if (string.IsNullOrWhiteSpace(Database))
                return (false, "Database не указан");
            if (string.IsNullOrWhiteSpace(UserId))
                return (false, "UserId не указан");
            if (string.IsNullOrWhiteSpace(Port))
                return (false, "Port не указан");

            // Проверка подключения к БД
            try
            {
                using (var connection = new MySql.Data.MySqlClient.MySqlConnection(ConnectionString))
                {
                    connection.Open();
                    return (true, "Настройки корректны");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Ошибка подключения к БД: {ex.Message}");
            }
        }
    }
}