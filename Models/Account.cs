using System.ComponentModel.DataAnnotations;

namespace HeThongDangKyDuHoc.Models
{
    public class Account
    {
        [Key]
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public bool isAdmin { get; set; } = false;
        public string Email { get; set; } = "";
    }
}
