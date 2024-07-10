using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeThongDangKyDuHoc.Models
{
    public class Register
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaDangKy { get; set; }
        public int MaSinhVien { get; set; }
        public string GhiChu { get; set; } = "";
        public int TrangThai { get; set; } = 0;
        public string GhiChuAdmin { get; set; } = "";
        public string NguoiDuyet { get; set; } = "";
        public DateTime NgayTao { get; set; } = DateTime.Now;
        public DateTime NgayCapNhat { get; set; } = DateTime.Now;
        public int MaKhoaDuHoc { get; set; }
        [NotMapped]
        public DuHoc KhoaDuHoc = new DuHoc();

    }
}
