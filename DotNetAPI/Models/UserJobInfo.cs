using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DotNetApi.Models
{
    public partial class UserJobInfo
    {
        [Key]
        public int UserId { get; set; }
        public string JobTitle { get; set; } = "";
        public string Department { get; set; } = "";

        public UserJobInfo()
        {
            JobTitle = JobTitle == null ? "" : JobTitle;
            Department = Department == null ? "" : Department;
        }
    }
}