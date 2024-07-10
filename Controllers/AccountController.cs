using Microsoft.AspNetCore.Mvc;
using HeThongDangKyDuHoc;
using HeThongDangKyDuHoc.Context;
using HeThongDangKyDuHoc.Utils;
using HeThongDangKyDuHoc.Models;

namespace HeThongDangKyDuHoc.Controllers
{
    public class AccountController : Controller
    {
        private readonly MyContext _context;
        public AccountController(MyContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Login()
        {
            var user = SessionUtil.getAccountFromUsr(_context, HttpContext.Session);
            if(user != null)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password, IFormFile fileImage)
        {
            var user = _context.Accounts.FirstOrDefault(x => x.Username == username && x.Password == password);
            if(user == null)
            {
                ViewBag.ErrorMessage = "Sai tên đăng nhập hoặc mật khẩu";
                return View();
            }
            HttpContext.Session.SetString(Program.session_username, username);
            HttpContext.Session.SetString(Program.session_admin, user.isAdmin ? "1" : "0");
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var user = SessionUtil.getAccountFromUsr(_context, HttpContext.Session);
            if(user == null)
            {
                return RedirectToAction("Login");
            }
            if(user.isAdmin)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                var sinhvien = SessionUtil.getSinhVienFromUsr(_context, HttpContext.Session);
                if(sinhvien == null)
                {
                    return RedirectToAction("Login");
                }
                sinhvien.giaDinhSinhVien = _context.GiaDinhSinhViens.FirstOrDefault(m => m.IdSinhVien == sinhvien.MaSinhVien);
                ViewBag.SinhVien = sinhvien;
                return View(user);
            }
        }
        public IActionResult Edit()
        {
            var user = SessionUtil.getAccountFromUsr(_context, HttpContext.Session);
            if (user == null)
            {
                return RedirectToAction("Login");
            }
            if (user.isAdmin)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                var sinhvien = SessionUtil.getSinhVienFromUsr(_context, HttpContext.Session);
                sinhvien.giaDinhSinhVien = _context.GiaDinhSinhViens.FirstOrDefault(m => m.IdSinhVien == sinhvien.MaSinhVien);
                if (sinhvien == null)
                {
                    return RedirectToAction("Login");
                }
                ViewBag.SinhVien = sinhvien;
                ViewBag.Notis = new List<CustomNotification>();
                return View(user);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string Email, string Phone, string DiaChi, IFormFile Image, GiaDinhSinhVien ?gd)
        {
            var user = SessionUtil.getAccountFromUsr(_context, HttpContext.Session);
            var sinhvien = SessionUtil.getSinhVienFromUsr(_context, HttpContext.Session);
            if (user == null || sinhvien == null)
            {
                return RedirectToAction("Login");
            }
            sinhvien.Email = Email;
            sinhvien.SoDienThoai = Phone;
            sinhvien.DiaChi = DiaChi;
            if (Image != null)
            {
                bool isSuccess = await FileUtil.SaveFile("avatars", sinhvien.MaSinhVien + "_" + Image.FileName, Image);
                if(isSuccess)
                    sinhvien.HinhAnh = sinhvien.MaSinhVien + "_" + Image.FileName;
            }
            var old_gd = _context.GiaDinhSinhViens.FirstOrDefault(m => m.IdSinhVien == sinhvien.MaSinhVien);
            if(gd != null)
            {
                if (old_gd != null)
                {
                    old_gd.HoTenBo = gd.HoTenBo;
                    old_gd.NgheNghiepBo = gd.NgheNghiepBo;
                    old_gd.SoDienThoaiBo = gd.SoDienThoaiBo;
                    old_gd.HoTenMe = gd.HoTenMe;
                    old_gd.NgheNghiepMe = gd.NgheNghiepMe;
                    old_gd.SoDienThoaiMe = gd.SoDienThoaiMe;
                }
                else
                {
                    _context.GiaDinhSinhViens.Add(gd);
                }
            }
            _context.SaveChanges();
            ViewBag.SinhVien = sinhvien;
            List<CustomNotification> notis = [CustomNotification.Success("Cập nhật thông tin thành công")];
            ViewBag.Notis = notis;
            return View(user);
        }
        public IActionResult Documents()
        {
            var sinhvien = SessionUtil.getSinhVienFromUsr(_context, HttpContext.Session);
            var user = SessionUtil.getAccountFromUsr(_context, HttpContext.Session);
            if(user == null)
            {
                return RedirectToAction("Login");
            }
            if (user.isAdmin)
            {
                return RedirectToAction("Index", "Admin");
            }
            else if (sinhvien == null)
            {
                return RedirectToAction("Login");
            }
            List<TaiLieu> tailieus = _context.TaiLieus.Where(m => m.MaSinhVien == sinhvien.MaSinhVien).ToList();
            return View(tailieus);
        }
        public IActionResult AddDocument()
        {
            var sinhvien = SessionUtil.getSinhVienFromUsr(_context, HttpContext.Session);
            if (sinhvien == null)
            {
                return RedirectToAction("Login");
            }
            var loaitailieus = _context.TypeDocuments.ToList();
            List<CustomNotification> customNotification = new List<CustomNotification>();
            ViewBag.Notis = customNotification;
            return View(loaitailieus);
        }
        [HttpPost]
        public async Task<IActionResult> AddDocument(TaiLieu? tailieu, IFormFile Image, int? BanGoc)
        {
            var sinhvien = SessionUtil.getSinhVienFromUsr(_context, HttpContext.Session);
            if (sinhvien == null || tailieu == null)
            {
                return BadRequest();
            }
            List<CustomNotification> customNotification = new List<CustomNotification>();
            var old_tailieu = _context.TaiLieus.FirstOrDefault(m => m.MaSinhVien == sinhvien.MaSinhVien && m.LoaiTaiLieu == tailieu.LoaiTaiLieu);
            if (old_tailieu != null)
            {
                customNotification.Add(CustomNotification.Error("Tài liệu đã tồn tại"));
                ViewBag.Notis = customNotification;
                return View(_context.TypeDocuments.ToList());
            }
            string uuid = Guid.NewGuid().ToString();
            bool isSuccess = await FileUtil.SaveFile("Documents", uuid + "_" + Image.FileName, Image);
            if (isSuccess)
            {
                tailieu.MaSinhVien = sinhvien.MaSinhVien;
                tailieu.TenTaiLieu = _context.TypeDocuments.FirstOrDefault(m => m.LoaiTaiLieu == tailieu.LoaiTaiLieu).TenTaiLieu ?? "Không xác định";
                tailieu.FileName = uuid + "_" + Image.FileName;
                if(BanGoc != null && BanGoc == 1)
                {
                    tailieu.isBanGoc = true;
                }
                else
                {
                    tailieu.isBanGoc = false;
                }
                _context.TaiLieus.Add(tailieu);
                _context.SaveChanges();
                customNotification.Add(CustomNotification.Success("Tải lên tài liệu thành công"));
            }
            else
            {
                customNotification.Add(CustomNotification.Error("Lỗi khi tải lên hình ảnh"));
            }
            ViewBag.Notis = customNotification;
            return View(_context.TypeDocuments.ToList());
        }
        public IActionResult EditDocuments(int ? document_id)
        {
            var sinhvien = SessionUtil.getSinhVienFromUsr(_context, HttpContext.Session);
            if (document_id == null) document_id = -1;
            var document = _context.TaiLieus.FirstOrDefault(m => m.MaTaiLieu == document_id);
            if (sinhvien == null)
            {
                return RedirectToAction("Login");
            }
            else if(document == null)
            {
                return NotFound();
            }
            else if(document.MaSinhVien != sinhvien.MaSinhVien)
            {
                return BadRequest();
            }
            ViewBag.TypeDocuments = _context.TypeDocuments.ToList();
            ViewBag.Notis = new List<CustomNotification>();
            return View(document);
        }

        [HttpPost]
        public async Task<IActionResult> EditDocuments(TaiLieu? tailieu, IFormFile Image, int? BanGoc)
        {
            var sinhvien = SessionUtil.getSinhVienFromUsr(_context, HttpContext.Session);
            if (sinhvien == null || tailieu == null)
            {
                return BadRequest();
            }
            List<CustomNotification> customNotification = new List<CustomNotification>();
            var document = _context.TaiLieus.FirstOrDefault(m => m.MaTaiLieu == tailieu.MaTaiLieu);
            if (document == null)
            {
                return NotFound();
            }
            if (Image != null)
            {
                string uuid = Guid.NewGuid().ToString();
                bool isSuccess = await FileUtil.SaveFile("Documents", uuid + "_" + Image.FileName, Image);
                if (isSuccess)
                {
                    document.FileName = uuid + "_" + Image.FileName;
                }
                else
                {
                    customNotification.Add(CustomNotification.Error("Lỗi khi tải lên hình ảnh mới"));
                }
            }
            document.LoaiTaiLieu = tailieu.LoaiTaiLieu;
            document.TenTaiLieu = _context.TypeDocuments.FirstOrDefault(m => m.LoaiTaiLieu == tailieu.LoaiTaiLieu).TenTaiLieu ?? "Không xác định";
            document.GhiChu = tailieu.GhiChu;
            if (BanGoc != null && BanGoc == 1)
            {
                document.isBanGoc = true;
            }
            else
            {
                document.isBanGoc = false;
            }
            customNotification.Add(CustomNotification.Success("Cập nhật tài liệu thành công"));
            _context.SaveChanges();
            ViewBag.TypeDocuments = _context.TypeDocuments.ToList();
            ViewBag.Notis = customNotification;
            return View(document);
        }

        public IActionResult DeleteDocument(int ? document_id)
        {
            var sinhvien = SessionUtil.getSinhVienFromUsr(_context, HttpContext.Session);
            if (document_id == null) document_id = -1;
            var document = _context.TaiLieus.FirstOrDefault(m => m.MaTaiLieu == document_id);
            if (sinhvien == null)
            {
                return RedirectToAction("Login");
            }
            else if (document == null)
            {
                return NotFound();
            }
            else if (document.MaSinhVien != sinhvien.MaSinhVien)
            {
                return BadRequest();
            }
            _context.TaiLieus.Remove(document);
            _context.SaveChanges();
            return RedirectToAction("Documents");
        }

        public IActionResult ViewRegisterStatus(int page = 1)
        {
            var sinhvien = SessionUtil.getSinhVienFromUsr(_context, HttpContext.Session);
            if (sinhvien == null)
            {
                return RedirectToAction("Login");
            }
            var totalRegisters = _context.DonDangKys.Where(m => m.MaSinhVien == sinhvien.MaSinhVien).ToList();
            foreach(var item in totalRegisters)
            {
                item.KhoaDuHoc = _context.DuHocs.FirstOrDefault(m => m.MaKhoaDuHoc == item.MaKhoaDuHoc);
                if(item.KhoaDuHoc == null)
                {
                    totalRegisters.Remove(item);
                }
            }
            var lstRegister = new List<Register>();
            int totalPage = totalRegisters.Count / 3 + totalRegisters.Count % 3 != 0 ? 1 : 0;
            if (totalPage >= 1){
                if(page > totalPage)
                {
                    page = totalPage;
                }
                else if(page < 1)
                {
                    page = 1;
                }
                if (totalRegisters.Count > 0)
                {
                    lstRegister = totalRegisters.Skip((page - 1) * 3).Take(3).ToList();
                }
            }
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = totalPage;
            return View(lstRegister);
        }

        public IActionResult ChangePassword()
        {
            var sinhvien = SessionUtil.getSinhVienFromUsr(_context, HttpContext.Session);
            var user = SessionUtil.getAccountFromUsr(_context, HttpContext.Session);
            if (sinhvien == null || user == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.Notis = new List<CustomNotification>();
            ViewBag.SinhVien = sinhvien;
            return View(user);
        }
        [HttpPost]
        public IActionResult ChangePassword(string old_password, string new_password, string repeat_new_password)
        {
            var sinhvien = SessionUtil.getSinhVienFromUsr(_context, HttpContext.Session);
            var user = SessionUtil.getAccountFromUsr(_context, HttpContext.Session);
            if (user == null || sinhvien == null)
            {
                return RedirectToAction("Login");
            }
            List<CustomNotification> customNotifications = new List<CustomNotification>();
            if (user.Password != old_password)
            {
                customNotifications.Add(CustomNotification.Error("Mật khẩu cũ không đúng"));
            }
            else if (new_password != repeat_new_password)
            {
                customNotifications.Add(CustomNotification.Error("Mật khẩu mới không khớp"));
            }
            else
            {
                user.Password = new_password;
                _context.SaveChanges();
                customNotifications.Add(CustomNotification.Success("Đổi mật khẩu thành công"));
            }
            ViewBag.Notis = customNotifications;
            ViewBag.SinhVien = sinhvien;
            return View(user);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(Program.session_username);
            HttpContext.Session.Remove(Program.session_admin);
            return RedirectToAction("Login");
        }

    }
}
