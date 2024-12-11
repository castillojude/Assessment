using Microsoft.AspNetCore.Mvc;
using DotNetApi.Data;
using DotNetApi.Dtos;
using DotNetApi.Models;

namespace DotNetApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    DataContextDapper _dapper;
    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetConnection")]
    public DateTime GetDate()
    {
        return _dapper.LoadDataSingle<DateTime>("Select getdate()");
    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        string sqlQuery = "select * from Users";
        IEnumerable<User> users = _dapper.LoadData<User>(sqlQuery);

        return users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        string sqlQuery = $"select * from Users where userId = {userId}";

        User user = _dapper.LoadDataSingle<User>(sqlQuery);

        return user;
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserDto data)
    {
        string sqlQuery = @$"insert into Users values('{escapeCharacter(data.FirstName)}',
        '{escapeCharacter(data.LastName)}',
        '{escapeCharacter(data.Email)}',
        '{data.Gender}',
        1)";

        var result = _dapper.ExecuteSql(sqlQuery);

        if(result)
        {
            return Ok("New User Added");
        }

        throw new Exception("Failed to updated user");
        
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User data)
    {
        string sqlQuery = @$"update Users set FirstName = '{escapeCharacter(data.FirstName)}',
         LastName = '{escapeCharacter(data.LastName)}', 
         Email = '{escapeCharacter(data.Email)}', 
         Gender = '{data.Gender}', 
         Active = '{data.Active}'
         where userId = {data.UserId}";

        Console.WriteLine(sqlQuery);

        var result = _dapper.ExecuteSql(sqlQuery);
        if(result)
        {
            return Ok("User Edited Successfully");
        }

        throw new Exception("Failed to updated user");
        
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sqlQuery = $"delete Users where userId = {userId}";

        var result = _dapper.ExecuteSql(sqlQuery);
        
        if(result)
        {
            return Ok("User Deleted Successfully");
        }

        throw new Exception("Failed to delete user");
        
    }

    string escapeCharacter(string text)
    {
        string escapedText = text.Replace("'", "''");

        return escapedText;
    }

}