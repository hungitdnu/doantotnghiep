using System.ComponentModel.DataAnnotations;

namespace HeThongDangKyDuHoc.Models
{
    public class GiaDinhSinhVien
    {
        [Key]
        public int IdSinhVien { get; set; }
        public string HoTenBo { get; set; } = "";
        public string NgheNghiepBo { get; set; } = "";
        public string SoDienThoaiBo { get; set; } = "";
        public string HoTenMe { get; set; } = "";
        public string NgheNghiepMe { get; set; } = "";
        public string SoDienThoaiMe { get; set; } = "";

    }
}
