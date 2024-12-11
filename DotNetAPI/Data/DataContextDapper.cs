using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DotNetApi.Data
{
    class DataContextDapper
    {
        private readonly IConfiguration _config;

        public DataContextDapper(IConfiguration config)
        {
            _config = config;
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

        public bool ExecuteSqlWithParameters(string sql, List<SqlParameter> sqlParams)
        {
            SqlCommand commandWithParams = new SqlCommand(sql);
            
            Console.WriteLine(sql);
            
            foreach(var sqlParam in sqlParams)
            {
                commandWithParams.Parameters.Add(sqlParam);
            }    

            SqlConnection dbConnection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

            dbConnection.Open();

            commandWithParams.Connection = dbConnection;

            int rowsAffected = commandWithParams.ExecuteNonQuery();

            dbConnection.Close();

            return rowsAffected > 0;
        }
    }
}