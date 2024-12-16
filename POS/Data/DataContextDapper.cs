using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;

namespace POS.Data
{
    public class DataContextDapper : IDataContextDapper
    {
        private readonly IConfiguration _config;

        public DataContextDapper(IConfiguration cfg)
        {
            _config = cfg;
        }

        public IEnumerable<T> LoadData<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Query<T>(sql);
        }

        public T LoadDataSingle<T>(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.QuerySingle<T>(sql);
        }

        public bool ExecuteSql(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            return dbConnection.Execute(sql) > 0;
        }

        public int ExecuteSqlWithRowCount(string sql)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            int rowCount = dbConnection.Execute(sql);

            return rowCount;
        }

        public bool ExecuteSqlWithParameters(string sql, DynamicParameters sqlParams)
        {
            IDbConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            // foreach (var sqlParam in sqlParams)
            // {
            //     commandWithParams.Parameters.Add(sqlParam);
            // }

            // SqlConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            // dbConnection.Open();

            // commandWithParams.Connection = dbConnection;

            // int rowsAffected = commandWithParams.ExecuteNonQuery();

            // dbConnection.Close();

            return dbConnection.Execute(sql, sqlParams) > 0;
        }
    }
}
