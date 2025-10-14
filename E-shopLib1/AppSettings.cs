using System;
using System.IO;
using System.Runtime.InteropServices;

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

                string[] lines = File.ReadAllLines(iniPath);

                foreach (string line in lines)
                {
                    if (line.Contains("="))
                    {
                        string[] parts = line.Split('=');
                        if (parts.Length == 2)
                        {
                            string key = parts[0].Trim();
                            string value = parts[1].Trim();

                            switch (key)
                            {
                                case "Server": Server = value; break;
                                case "Database": Database = value; break;
                                case "UserId": UserId = value; break;
                                case "Password": Password = value; break;
                                case "Port": Port = value; break;
                                case "Charset": Charset = value; break;
                        }
                        }
                    }
                }         
        }  
    }
}