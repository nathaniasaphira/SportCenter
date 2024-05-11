using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCenter.DataAccess;

public class DatabaseService
{
    public DatabaseConnection _databaseConnection;

    public DatabaseService()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        _databaseConnection = new DatabaseConnection(connectionString);
    }

    public void GetAll(string column1, string column2)
    {
        using (IDbConnection connection = _databaseConnection.CreateConnection())
        {
            connection.Open();

            string query = "SELECT * FROM members";
            
            connection.Execute(query, new { column1, column2 });
        }
    }


}
