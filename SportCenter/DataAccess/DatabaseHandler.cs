using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCenter.DataAccess;

public class DatabaseHandler
{
    private readonly DatabaseConnection _databaseConnection;

    public DatabaseHandler()
    {
        string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
        _databaseConnection = new DatabaseConnection(connectionString);
    }

    public void InsertNewRow(string column1, string column2)
    {
        using (IDbConnection connection = _databaseConnection.CreateConnection())
        {
            connection.Open();

            string query = "";

            connection.Execute(query, new { column1, column2 });
        }
    }


}
