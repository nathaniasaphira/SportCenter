using Dapper;
using SportCenter.DataAccess;
using SportCenter.Models.Entities;
using System.Data;

namespace SportCenter.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly DatabaseConnection _databaseConnection;

        public UserRepository(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public IEnumerable<User> GetAll()
        {
            using IDbConnection dbConnection = _databaseConnection.CreateConnection();

            string query = "SELECT * FROM " + User.Table;
            return dbConnection.Query<User>(query).AsList();
        }

        public User GetById(int id)
        {
            using IDbConnection connection = _databaseConnection.CreateConnection();

            string query = "SELECT * FROM " + User.Table + " WHERE Id = @Id";
            return connection.QuerySingleOrDefault<User>(query, new { Id = id });
        }

        public void Add(User user)
        {
            using IDbConnection connection = _databaseConnection.CreateConnection();

            string query = "INSERT INTO " + User.Table + " (Username, Password, Role) VALUES (@Username, @Password, @Role)";
            connection.Execute(query, user);
        }

        public void Update(User user)
        {
            using IDbConnection connection = _databaseConnection.CreateConnection();

            string query = "UPDATE " + User.Table + " SET Username = @Username, Password = @Password, Role = @Role WHERE Id = @Id";
            connection.Execute(query, user);
        }
    }
}
