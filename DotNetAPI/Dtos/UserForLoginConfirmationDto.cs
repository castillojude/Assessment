namespace DotNetApi.Dtos
{
    public partial class UserForLoginConfirmationDto
    {
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

        public UserForLoginConfirmationDto()
        {
            PasswordSalt = PasswordSalt == null ? new byte[0] : PasswordSalt; 
            PasswordHash = PasswordHash == null ? new byte[0] : PasswordHash; 
        }
    }
}