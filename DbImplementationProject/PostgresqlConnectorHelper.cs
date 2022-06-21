using System;
using Npgsql;

namespace DbImplementationProject
{
    public class PostgresqlConnectorHelper : IDisposable
    {
        private const string ConnectionString = "Host=localhost;Username=postgres;Password=123456;Database=project2";
        private static NpgsqlConnection _connection;
        
        public NpgsqlConnection GetConnection()
        {
            _connection = new NpgsqlConnection(ConnectionString);
            _connection.Open();
            return _connection;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}