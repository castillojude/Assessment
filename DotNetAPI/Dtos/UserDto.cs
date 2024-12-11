using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DotNetApi.Dtos
{
    public partial class UserDto
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Gender { get; set; } = "";
        public bool Active { get; set; }

        public UserDto()
        {
            FirstName = FirstName == null ? "" : FirstName;
            LastName = LastName == null ? "" : LastName;
            Email = Email == null ? "" : Email;
            Gender = Gender == null ? "" : Gender;
        }
    }
}