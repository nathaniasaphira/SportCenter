using Dapper;
using MySql.Data.MySqlClient;
using SportCenter.Models.Entities;
using System.Configuration;
using System.Data;

namespace SportCenter.DataAccess;

public class DatabaseConnection
{
    private string connectionString;

    public DatabaseConnection()
    {
        this.connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new MySqlConnection(connectionString);
    }

    public List<Member> GetAllData()
    {
        using IDbConnection dbConnection = CreateConnection();

        string query = "SELECT * FROM members";
        return dbConnection.Query<Member>(query).AsList();
    }
}
