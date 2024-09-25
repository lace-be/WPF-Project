using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace dal
{
    public class DatabaseConnection
    {
        public static string Connectionstring(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
