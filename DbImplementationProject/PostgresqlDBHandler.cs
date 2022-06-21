using System;
using Npgsql;

namespace DbImplementationProject
{
    public class PostgresqlDBHandler
    {
        private NpgsqlConnection _connection;
        private readonly string _tableName = "testtable";
        private string col1name = "key";
        private string col2name = "value";
        public PostgresqlDBHandler()
        {
            _connection = new PostgresqlConnectorHelper().GetConnection();
        }

        public bool SetData(string key, string value)
        {
            try
            {
                using var cmd = new NpgsqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = $"INSERT INTO {_tableName} VALUES('{key}' ,'{value}')";
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public string? GetData(string key)
        {
            try
            {
                using var cmd = new NpgsqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = $"SELECT {col2name} FROM {_tableName} WHERE {col1name} = '{key}'";
                var reader = cmd.ExecuteReader();
                reader.Read();
                var res = reader[0].ToString();
                reader.Close();
                cmd.Cancel();
                return res;

            }
            catch (Exception e)
            {
                return null;
            }
        }
        
        public bool CreateTable()
        {
            try
            {
                using var cmd = new NpgsqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = $"CREATE TABLE {_tableName}({col1name} VARCHAR(50), {col2name} VARCHAR(50))";
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}