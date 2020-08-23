using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
namespace PointOfSaleSystem
{
    class MyConnection
    {
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection {ConnectionString=@"Server=localhost;Database=pointofsale;User=root;Password=" };
        }
    }
}
