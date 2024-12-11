using DotNetApi.Models;

namespace DotNetApi.Data;

public class UserRepository : IUserRepository
{
    DataContextEF _ef;


    public UserRepository(IConfiguration config)
    {
        _ef = new DataContextEF(config);
    } 

    public bool SaveChanges()
    {
        return _ef.SaveChanges() > 0;
    }

    public void AddEntity<T>(T data)
    {
        if(data != null)
        {
            _ef.Add(data);
        }
    }

    public void DeleteEntity<T>(T data)
    {
        if(data != null)
        {
            _ef.Remove(data);
        }
    }

    public IEnumerable<User> GetUsers()
    {
        var userList = _ef.Users.ToList();

        return userList;
    }

    public User GetSingleUser(int userId)
    {

        User? user = _ef.Users.Where(x => x.UserId == userId).SingleOrDefault();
        
        if(user != null)
        {
            return user;
        }
        
        throw new Exception("Failed to get User");
    }

    public UserSalary GetSingleUserSalary(int userId)
    {

        UserSalary? userSalary = _ef.UserSalary.Where(x => x.UserId == userId).SingleOrDefault();
        
        if(userSalary != null)
        {
            return userSalary;
        }
        
        throw new Exception("Failed to get User Salary / No user found");
    }

    public UserJobInfo GetSingleUserJobInfo(int userId)
    {

        UserJobInfo? userJobInfo = _ef.UserJobInfo.Where(x => x.UserId == userId).SingleOrDefault();
        
        if(userJobInfo != null)
        {
            return userJobInfo;
        }
        
        throw new Exception("Failed to get User Job Info / No user found");
    }
}