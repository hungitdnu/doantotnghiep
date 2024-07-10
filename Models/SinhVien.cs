using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeThongDangKyDuHoc.Models
{
    public class SinhVien
    {
        [Key]
        public int MaSinhVien { get; set; } 
        public string HoTen { get; set; } = "";
        public string Email { get; set; } = "";
        public string SoDienThoai { get; set; } = "";
        public string DiaChi { get; set; } = "";
        public string GioiTinh { get; set; } = "";
        public DateTime NgaySinh { get; set; } = DateTime.Now.Date;
        public string CMND { get; set; } = "";
        public string HinhAnh { get; set; } = "avatar.webp";
        public string MaNganh { get; set; } = "";
        public int MaLop { get; set; }
        public string MaHeDaoTao { get; set; } = "";
        public string MaChuongTrinhHoc { get; set; } = "";
        public int NienKhoa { get; set; }
        public string Username { get; set; } = "";
        [NotMapped]
        public GiaDinhSinhVien giaDinhSinhVien = new GiaDinhSinhVien();
        [NotMapped]
        public List<TaiLieu> tailieus = new List<TaiLieu>();
    }
}
