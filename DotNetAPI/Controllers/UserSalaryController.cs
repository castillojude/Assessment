using DotNetApi.Data;
using DotNetApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserSalaryController : ControllerBase
{
    DataContextDapper _dapper;

    public UserSalaryController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetUserSalary")]
    public IEnumerable<UserSalary> GetUserSalary()
    {
        string sqlQuery = "select * from UserSalary";

        var results = _dapper.LoadData<UserSalary>(sqlQuery);
        
        return results;
    }

    [HttpGet("GetSingleUserSalary/{userId}")]
    public UserSalary GetSingleUserSalary(int userId)
    {
        string userSqlQuery = $"select userId from Users where userId = {userId}";

        var userResult = _dapper.LoadDataSingle<User>(userSqlQuery);

        if(userResult != null)
        {
            string sqlQuery = $"select * from UserSalary where userId = {userId}";

            var result = _dapper.LoadDataSingle<UserSalary>(sqlQuery);

            if(result != null) return result;

            throw new Exception("Cannot find User Salary");
        }

        throw new Exception("User doesn't exist");
    }

    [HttpPost("CreateUserSalary")]
    public IActionResult CreateUserSalary(UserSalary data)
    {
        string userSqlQuery = $"select userId from Users where userId = {data.UserId}";

        var userResult = _dapper.LoadDataSingle<User>(userSqlQuery);

        if(userResult != null)
        {
            string sqlQuery = @$"insert into UserSalary values ({data.UserId}, {data.Salary})";

            var result = _dapper.ExecuteSql(sqlQuery);

            if(result)
            {
                return Ok("User Salary Updated Successfully");
            }

            throw new Exception("Failed to update user salary");
        }

        throw new Exception("UserId doesn't exist"); 
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary data)
    {
        string userSqlQuery = $"select userId from Users where userId = {data.UserId}";

        var userResult = _dapper.LoadDataSingle<User>(userSqlQuery);

        if(userResult != null)
        {
            string sqlQuery = @$"update UserSalary set 
            Salary = '{data.Salary}'
            where userId = {data.UserId}
            ";

            var result = _dapper.ExecuteSql(sqlQuery);

            if(result)
            {
                return Ok("User Salary Updated Successfully");
            }

            throw new Exception("Failed to update user salary");
        }

         throw new Exception("UserId doesn't exist"); 
    }

    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        string userSqlQuery = $"select userId from Users where userId = {userId}";

        var userResult = _dapper.LoadDataSingle<User>(userSqlQuery);

        if(userResult != null)
        {
            string deleteSqlQuery = $"delete UserSalary where userId = {userId}";
            
            var result = _dapper.ExecuteSql(deleteSqlQuery);

            if(result)
            {
                return Ok("User Salary successfully deleted");
            }

            throw new Exception("Failed to delete user salary");
        }

        throw new Exception("User doesn't exist");

    }
}