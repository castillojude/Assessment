using Microsoft.AspNetCore.Mvc;
using DotNetApi.Data;
using DotNetApi.Dtos;
using DotNetApi.Models;
using AutoMapper;

namespace DotNetApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserControllerEF : ControllerBase
{
    //DataContextEF _ef;
    IMapper _mapper;
    IUserRepository _userRepo;

    public UserControllerEF(IConfiguration config, IUserRepository userRepo)
    {
        //_ef = new DataContextEF(config);

        _userRepo = userRepo;

        _mapper = new Mapper(new MapperConfiguration(x =>
        {
            x.CreateMap<UserDto, User>();
        }));


    }

    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        // var Users = _ef.Users.ToList();
        var Users = _userRepo.GetUsers().ToList();

        return Users;
    }

    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        //var user = _ef.Users.Where(x => x.UserId == userId).SingleOrDefault();
        var user = _userRepo.GetSingleUser(userId);

        return user;

        //throw new Exception("User Not Found!");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserDto data)
    {
        try
        {
            var newUser = _mapper.Map<User>(data);

            // var newUser = new User()
            // {
            //     FirstName = data.FirstName,
            //     LastName = data.LastName,
            //     Email = data.Email,
            //     Gender = data.Gender,
            //     Active = true
            // };

            _userRepo.AddEntity(newUser);

            if (_userRepo.SaveChanges()) return Ok("New User Added");

            return Ok("No changes done");
        }

        catch (Exception ex)
        {
            string err = "Failed to add new user";

            throw new Exception($"error: {err} | {ex.Message}");
        }


    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User data)
    {

        // var currentUser = _ef.Users.Where(x => x.UserId == data.UserId).FirstOrDefault();

        var currentUser = _userRepo.GetSingleUser(data.UserId);

        if (currentUser != null)
        {
            currentUser.FirstName = data.FirstName;
            currentUser.LastName = data.LastName;
            currentUser.Email = data.Email;
            currentUser.Gender = data.Gender;
            currentUser.Active = data.Active;
        }

        if (_userRepo.SaveChanges())
        {
            return Ok("User editted successfully");
        }

        return Ok("No changes done");

    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        //var userToDelete = _ef.Users.Where(x => x.UserId == userId).FirstOrDefault();
        var userToDelete = _userRepo.GetSingleUser(userId);

        if (userToDelete != null)
        {
            _userRepo.DeleteEntity(userToDelete);

            if (_userRepo.SaveChanges())
            {
                return Ok("User deleted successfully");
            }

        }

        throw new Exception($"User failed to delete");


    }

    [HttpGet("GetUserJobInfo/{userId}")]
    public UserJobInfo GetUserJobInfo(int userId)
    {
        // var userJobInfo = _ef.UserJobInfo.Where(x => x.UserId == userId).SingleOrDefault();
        var userJobInfo = _userRepo.GetSingleUserJobInfo(userId);

        if (userJobInfo != null) return userJobInfo;

        throw new Exception("User Not Found!");
    }

    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUserJobInfo(UserJobInfo data)
    {

        //User? user = _ef.Users.Where(x => x.UserId == data.UserId).FirstOrDefault();
        var user = _userRepo.GetSingleUser(data.UserId);

        if (user != null)
        {
            _userRepo.AddEntity(data);

            if (_userRepo.SaveChanges())
            {
                return Ok("New User Job Info Added");
            }

        }

        throw new Exception($"User doesn't exist");


    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo data)
    {

        //var currentUser = _ef.UserJobInfo.Where(x => x.UserId == data.UserId).FirstOrDefault();
        var currentUser = _userRepo.GetSingleUserJobInfo(data.UserId);

        if (currentUser != null)
        {
            currentUser.JobTitle = data.JobTitle;
            currentUser.Department = data.Department;

            if (_userRepo.SaveChanges())
            {
                return Ok("User Job Info editted successfully");
            }
        }

        throw new Exception($"User doesn't exist");

    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        //var jobInfoToDelete = _ef.UserJobInfo.Where(x => x.UserId == userId).FirstOrDefault();
        var jobInfoToDelete = _userRepo.GetSingleUserJobInfo(userId);

        if (jobInfoToDelete != null)
        {
            _userRepo.DeleteEntity(jobInfoToDelete);

            if (_userRepo.SaveChanges())
            {
                return Ok("User job info deleted successfully");
            }
        }

        throw new Exception($"User job info failed to delete");
    }

    [HttpGet("GetUserSalary/{userId}")]
    public UserSalary GetUserSalary(int userId)
    {
        //var userSalary = _ef.UserSalary.Where(x => x.UserId == userId).SingleOrDefault();
        var userSalary = _userRepo.GetSingleUserSalary(userId);

        if (userSalary != null) return userSalary;

        throw new Exception("User Not Found!");
    }

    [HttpPost("AddUserSalary")]
    public IActionResult AddUserSalary(UserSalary data)
    {

        //User? user = _ef.Users.Where(x => x.UserId == data.UserId).FirstOrDefault();
        User? user = _userRepo.GetSingleUser(data.UserId);

        if (user != null)
        {
            _userRepo.AddEntity(data);

            if (_userRepo.SaveChanges())
            {
                return Ok("New User Salary Added");
            }
        }

        throw new Exception($"User doesn't exist");


    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary data)
    {

        //var currentUser = _ef.UserSalary.Where(x => x.UserId == data.UserId).FirstOrDefault();
        var currentUser = _userRepo.GetSingleUserSalary(data.UserId);
        if (currentUser != null)
        {
            currentUser.Salary = data.Salary;

            if (_userRepo.SaveChanges())
            {
                return Ok("User salary editted successfully");
            }


        }

        throw new Exception($"User doesn't exist");

    }

    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        //var jobSalaryToDelete = _ef.UserSalary.Where(x => x.UserId == userId).FirstOrDefault();

        var jobSalaryToDelete = _userRepo.GetSingleUserSalary(userId);

        if (jobSalaryToDelete != null)
        {
            _userRepo.DeleteEntity(jobSalaryToDelete);

            if (_userRepo.SaveChanges())
            {
                return Ok("User job salary deleted successfully");
            }

        }

        throw new Exception($"User job salary failed to delete");
    }

    string escapeCharacter(string text)
    {
        string escapedText = text.Replace("'", "''");

        return escapedText;
    }

}