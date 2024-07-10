using Microsoft.EntityFrameworkCore;
using HeThongDangKyDuHoc.Models;

namespace HeThongDangKyDuHoc.Context
{
    public class MyContext: DbContext
    {
        public DbSet<SinhVien> SinhViens { get; set; }
        public DbSet<GiaDinhSinhVien> GiaDinhSinhViens { get; set; }
        public DbSet<GiangVien> GiangViens { get; set; }
        public DbSet<TaiLieu> TaiLieus { get; set; }
        public DbSet<DuHoc> DuHocs { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<TinNhan> TinNhans { get; set; }
        public DbSet<TypeDocument> TypeDocuments { get; set; }
        public DbSet<Register> DonDangKys { get; set; }
        public DbSet<YeuCauDuHoc> YeuCauDuHocs { get; set; }
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SinhVien>().ToTable("SinhVien").HasKey(sv => sv.MaSinhVien);
            modelBuilder.Entity<GiaDinhSinhVien>().ToTable("GiaDinhSinhVien").HasKey(gd => gd.IdSinhVien);
            modelBuilder.Entity<GiangVien>().ToTable("GiangVien").HasKey(gv => gv.MaGiangVien);
            modelBuilder.Entity<TaiLieu>().ToTable("TaiLieu").HasKey(tl => tl.MaTaiLieu);
            modelBuilder.Entity<DuHoc>().ToTable("DuHoc").HasKey(dh => dh.MaKhoaDuHoc);
            modelBuilder.Entity<Account>().ToTable("Account").HasKey(acc => acc.Username);
            modelBuilder.Entity<TinNhan>().ToTable("TinNhan").HasKey(tn => tn.Id);
            modelBuilder.Entity<TypeDocument>().ToTable("LoaiTaiLieu").HasKey(td => td.LoaiTaiLieu);
            modelBuilder.Entity<Register>().ToTable("DonDangKy").HasKey(ddk => ddk.MaDangKy);
            modelBuilder.Entity<YeuCauDuHoc>().ToTable("YeuCauDuHoc").HasKey(y => y.MaYeuCau);
        }
    }
}
