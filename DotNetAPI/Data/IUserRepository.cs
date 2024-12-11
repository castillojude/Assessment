using DotNetApi.Models;

namespace DotNetApi.Data;

public interface IUserRepository
{
    public bool SaveChanges();
    public void AddEntity<T>(T data);
    public void DeleteEntity<T>(T data);
    public IEnumerable<User> GetUsers();
    public User GetSingleUser(int userId);
    public UserSalary GetSingleUserSalary(int userId);
    public UserJobInfo GetSingleUserJobInfo(int userId);

}