using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeThongDangKyDuHoc.Models
{
    public class DuHoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaKhoaDuHoc { get; set; }
        public string TenTruong { get; set; } = "";
        public string QuocGia { get; set; } = "";
        public string Website { get; set; } = "";
        public string Email { get; set; } = "";
        public string SoDienThoai { get; set; } = "";
        public string Image { get; set; } = "";
        public string Ten { get; set; } = "";
        public int HocPhi { get; set; } 
        public string ThoiGian { get; set; } = "";
        public string MoTa { get; set; } = "";
    }
}
