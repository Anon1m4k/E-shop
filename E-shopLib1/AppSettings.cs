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
            string iniPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.ini");

            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(iniPath);

            Server = data["Database"]["Server"];
            Database = data["Database"]["Database"];
            UserId = data["Database"]["UserId"];
            Password = data["Database"]["Password"];
            Port = data["Database"]["Port"];
            Charset = data["Database"]["Charset"];
        }

        public static bool AreSettingsValid()
        {
            return !string.IsNullOrWhiteSpace(Server) &&
                   !string.IsNullOrWhiteSpace(Database) &&
                   !string.IsNullOrWhiteSpace(UserId) &&
                   !string.IsNullOrWhiteSpace(Port);
        }
    }

}