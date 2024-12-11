using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DotNetApi.Data;
using DotNetApi.Dtos;
using DotNetApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace DotNetApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly DataContextDapper _dapper;
    //private readonly IConfiguration _config;
    private readonly AuthHelper _authHelper;

    public AuthController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
        //_config = config;
        _authHelper = new AuthHelper(config);
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public IActionResult Register(UserForRegistrationDto userRegInfo)
    {
        if (userRegInfo.Password == userRegInfo.PasswordConfirm)
        {
            string sqlCheckUserExists = $"select Email from Auth where email = '{userRegInfo.Email}'";

            var users = _dapper.LoadData<string>(sqlCheckUserExists);

            if (users.Count() == 0)
            {
                byte[] pwSalt = new byte[128 / 8];

                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    rng.GetNonZeroBytes(pwSalt);
                }

                byte[] pwHash = _authHelper.GetPasswordHash(userRegInfo.Password, pwSalt);

                string sqlAddAuth = $@"insert into Auth
                     values ('{userRegInfo.Email}', @PasswordHash, @PasswordSalt) ";

                var sqlParameters = new List<SqlParameter>();

                var pwHashParam = new SqlParameter("@PasswordHash", SqlDbType.VarBinary);
                pwHashParam.Value = pwHash;

                var pwSaltParam = new SqlParameter("@PasswordSalt", SqlDbType.VarBinary);
                pwSaltParam.Value = pwSalt;

                sqlParameters.Add(pwHashParam);
                sqlParameters.Add(pwSaltParam);

                if (_dapper.ExecuteSqlWithParameters(sqlAddAuth, sqlParameters))
                {
                    string sqlAddUser = @$"insert into Users values('{escapeCharacter(userRegInfo.FirstName)}',
                                    '{escapeCharacter(userRegInfo.LastName)}',
                                    '{escapeCharacter(userRegInfo.Email)}',
                                    '{userRegInfo.Gender}',
                                    1)";

                    if (_dapper.ExecuteSql(sqlAddUser))
                    {
                        return Ok();
                    }

                    throw new Exception("Failed to register user");

                }

                throw new Exception("Failed to register user");


            }

            throw new Exception("User already exists");

        }

        throw new Exception("Passwords does not match!");
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public IActionResult Login(UserForLoginDto userLogin)
    {
        string sqlForHashAndSalt = $"select PasswordHash, PasswordSalt from Auth where Email = '{userLogin.Email}'";

        var userForConfirmation = _dapper.LoadDataSingle<UserForLoginConfirmationDto>(sqlForHashAndSalt);

        Console.WriteLine(userForConfirmation);

        if (userForConfirmation != null)
        {
            byte[] pwHash = _authHelper.GetPasswordHash(userLogin.Password, userForConfirmation.PasswordSalt);

            Console.WriteLine(pwHash);
            //if(pwHash == userForLoginConfirmation.PasswordHash) // wont work

            // will check if every byte is the same
            for (int i = 0; i < pwHash.Length; i++)
            {
                if (pwHash[i] != userForConfirmation.PasswordHash[i])
                {
                    return StatusCode(401, "Incorrect Password");
                }
            }

            string sqlGetUserId = $"select userId from Users where email = '{userLogin.Email}'";

            int userId = _dapper.LoadDataSingle<int>(sqlGetUserId);
            return Ok(new Dictionary<string, string> {
                {"token", _authHelper.CreateToken(userId)}
            });
        }

        throw new Exception("User doesn't exist");
    }

    [HttpGet("RefreshToken")]
    public IActionResult RefreshToken()
    {
        // Will look on Claims for userId
        string userId = User.FindFirst("userId")?.Value + "";

        string userIdSql = $"select userId from Users where userId = {userId}";

        int userIdFromDB = _dapper.LoadDataSingle<int>(userIdSql);

        return Ok(new Dictionary<string, string> {
                {"token", _authHelper.CreateToken(userIdFromDB)}
            });
    }

    string escapeCharacter(string text)
    {
        string escapedText = text.Replace("'", "''");

        return escapedText;
    }
}