using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace _22806012222_PhamNgocHuy_S3_B3.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? Age { get; set; }
    }
}
