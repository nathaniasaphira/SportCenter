using Dapper;
using MySql.Data.MySqlClient;
using SportCenter.Models.Entities;
using System.Configuration;
using System.Data;

namespace SportCenter.DataAccess;

public class DatabaseConnection
{
    private readonly string _connectionString;

    public DatabaseConnection()
    {
        _connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}
