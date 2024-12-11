using DotNetApi.Models;
using DotNetApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserJobInfoController : ControllerBase
{
    DataContextDapper _dapper;

    public UserJobInfoController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetUserJobInfos")]
    public IEnumerable<UserJobInfo> GetUserJobInfos()
    {
        string sqlQuery = "select * from UserJobInfo";

        var results = _dapper.LoadData<UserJobInfo>(sqlQuery);
        
        return results;
    }

    [HttpGet("GetSingleUserJobInfo/{userId}")]
    public UserJobInfo GetSingleUserJobInfo(int userId)
    {
        string userSqlQuery = $"select userId from Users where userId = {userId}";

        var userResult = _dapper.LoadDataSingle<User>(userSqlQuery);

        if(userResult != null)
        {
            string sqlQuery = $"select * from UserJobInfo where userId = {userId}";

            var result = _dapper.LoadDataSingle<UserJobInfo>(sqlQuery);

            if(result != null) return result;

            throw new Exception("Cannot find User Job Info");
        }

        throw new Exception("User doesn't exist");
    }

    [HttpPost("CreateUserJobInfo")]
    public IActionResult CreateUserJobInfo(UserJobInfo data)
    {
        string userSqlQuery = $"select userId from Users where userId = {data.UserId}";

        var userResult = _dapper.LoadDataSingle<User>(userSqlQuery);

        if(userResult != null)
        {
            string sqlQuery = @$"insert into UserJobInfo 
            values({data.UserId},'{escapeCharacter(data.JobTitle)}',
            '{escapeCharacter(data.Department)}')";

            var result = _dapper.ExecuteSql(sqlQuery);

            if(result)
            {
                return Ok("New Job Info Created Successfully");
            }

            throw new Exception("Failed to create new job info");
        }

         throw new Exception("UserId doesn't exist");    
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo data)
    {
        string userSqlQuery = $"select userId from Users where userId = {data.UserId}";

        var userResult = _dapper.LoadDataSingle<User>(userSqlQuery);

        if(userResult != null)
        {
            string sqlQuery = @$"update UserJobInfo set 
            JobTitle = '{data.JobTitle}',
            Department = '{data.Department}'
            where userId = {data.UserId}
            ";

            var result = _dapper.ExecuteSql(sqlQuery);

            if(result)
            {
                return Ok("User Job Info Updated Successfully");
            }

            throw new Exception("Failed to update job info");
        }

         throw new Exception("UserId doesn't exist");    
    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        string userSqlQuery = $"select userId from Users where userId = {userId}";

        var userResult = _dapper.LoadDataSingle<User>(userSqlQuery);

        if(userResult != null)
        {
            string deleteSqlQuery = $"delete UserJobInfo where userId = {userId}";
            
            var result = _dapper.ExecuteSql(deleteSqlQuery);

            if(result)
            {
                return Ok("User Job Info successfully deleted");
            }

            throw new Exception("Failed to delete job info");
        }

        throw new Exception("User doesn't exist");

    }

    string escapeCharacter(string text)
    {
        string escapedText = text.Replace("'", "''");

        return escapedText;
    }

}