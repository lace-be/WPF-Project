using System;
using System.Collections.Generic;
using System.Text;

namespace dal
{
    public class BaseRepository
    {
        protected string ConnectionString { get; }
        public BaseRepository()
        {
            ConnectionString = DatabaseConnection.Connectionstring("DierenartsDB");
        }
    }
}
