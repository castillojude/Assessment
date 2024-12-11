namespace DotNetApi.Dtos
{
    public partial class UserForRegistrationDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Gender { get; set; } = "";

        public UserForRegistrationDto()
        {
            FirstName = FirstName == null ? "" : FirstName;
            LastName = LastName == null ? "" : LastName;
            Gender = Gender == null ? "" : Gender;
            Email = Email == null ? "" : Email;
            Password = Password == null ? "" : Password;
            PasswordConfirm = PasswordConfirm == null ? "" : PasswordConfirm;
        }
    }
}