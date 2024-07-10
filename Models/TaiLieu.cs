using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HeThongDangKyDuHoc.Models
{
    public class TaiLieu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaTaiLieu { get; set; } 
        public string TenTaiLieu { get; set; } = "";
        public string FileName { get; set; } = "";
        public string LoaiTaiLieu { get; set; } = "";
        public bool isBanGoc { get; set; }
        public string GhiChu { get; set; } = "";
        public int MaSinhVien { get; set; }
    }
}
