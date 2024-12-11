using System.ComponentModel.DataAnnotations;

namespace DotNetApi.Models
{
    public partial class UserSalary
    {
        [Key]
        public int UserId { get; set; }
        public decimal Salary { get; set; }

    }
}