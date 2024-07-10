using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace HeThongDangKyDuHoc.Models
{
    public class YeuCauDuHoc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaYeuCau { get; set; }
        public int MaKhoaDuHoc { get; set; }
        public string LoaiTaiLieu { get; set; } = "";
        public string TenTaiLieu { get; set; } = "";
        public string MoTa { get; set; } = "";
        [NotMapped]
        public TaiLieu taiLieuSV = new TaiLieu();
    }
}
