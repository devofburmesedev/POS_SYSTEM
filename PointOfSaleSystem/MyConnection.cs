using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
namespace PointOfSaleSystem
{
    class MyConnection
    {
        public SqlConnection GetConnection()
        {
            return new SqlConnection { ConnectionString = @"Data Source=DESKTOP-1P3FEHV\MSSQLSERVER02;Initial Catalog=pointofsale;Integrated Security=True" };
        }
    }
}
