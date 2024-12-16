using Dapper;
using Microsoft.AspNetCore.Authorization;
using POS.Models;

namespace POS.Data;

public interface IDataContextDapper
{
    public IEnumerable<T> LoadData<T>(string sql);

    public T LoadDataSingle<T>(string sql);


    public bool ExecuteSql(string sql);

    public int ExecuteSqlWithRowCount(string sql);

    public bool ExecuteSqlWithParameters(string sql, DynamicParameters sqlParams);

}