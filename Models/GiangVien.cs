using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeThongDangKyDuHoc.Models
{
    public class GiangVien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaGiangVien { get; set; }
        public string HoTen { get; set; } = "";
        public string Email { get; set; } = "";
        public string SoDienThoai { get; set; } = "";
        public string DiaChi { get; set; } = "";
        public string ChucVu { get; set; } = "";
        public string HocVi { get; set; } = "";
        public string SoCMND { get; set; } = "";
        public string AnhDaiDien { get; set; } = "";
        public string GioiTinh { get; set; } = "";
        public DateTime NgaySinh { get; set; }
        public string Username { get; set; } = "";
    }
}
