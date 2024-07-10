using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeThongDangKyDuHoc.Models
{
    public class TinNhan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string NoiDung { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime NgayGui { get; set; } = DateTime.Now;
        public string Subject { get; set; }
        public bool DaDoc { get; set; } = false;
    }
}
