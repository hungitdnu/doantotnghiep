using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HeThongDangKyDuHoc;
using HeThongDangKyDuHoc.Context;
using HeThongDangKyDuHoc.Models;
using Azure;
using static System.Net.Mime.MediaTypeNames;
using HeThongDangKyDuHoc.Utils;

namespace HeThongDangKyDuHoc.Controllers
{
    public class AdminController : Controller
    {
        private readonly MyContext _context;
        public AdminController(MyContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString(Program.session_admin) == null || HttpContext.Session.GetString(Program.session_admin) != "1")
            {
                return NotFound();
            }
            return RedirectToAction("Message");
        }
        //Message
        public IActionResult Message(int? page)
        {
            List<TinNhan> messages = new List<TinNhan>();
            if (page == null)
            {
                page = 1;
            }
            if (HttpContext.Session.GetString("isAdmin") == null || HttpContext.Session.GetString("isAdmin") != "1")
            {
                return NotFound();
            }
            int total = _context.TinNhans.Count();
            int perPage = 10;
            int totalPages = (int)Math.Ceiling((double)total / perPage);
            if (page > totalPages)
            {
                page = totalPages;
            }
            else if (page < 1)
            {
                page = 1;
            }
            if (totalPages != 0)
            {
                messages = _context.TinNhans.Skip((int)(perPage * (page - 1))).Take(perPage).ToList();
                messages.Reverse();
            }
            ViewBag.Current = "Message";
            ViewBag.page = page;
            ViewBag.total = total;
            return View(messages);
        }

        public IActionResult ViewMessage(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (HttpContext.Session.GetString("isAdmin") == null || HttpContext.Session.GetString("isAdmin") != "1")
            {
                return NotFound();
            }
            var message = _context.TinNhans.FirstOrDefault(x => x.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            message.DaDoc = true;
            _context.SaveChanges();
            return View(message);
        }
        // Student
        public IActionResult Student(int? page)
        {
            if (page == null || page < 1)
            {
                page = 1;
            }
            if (HttpContext.Session.GetString("isAdmin") == null || HttpContext.Session.GetString("isAdmin") != "1")
            {
                return NotFound();
            }
            List<SinhVien> sinhviens = new List<SinhVien>();
            int total = _context.SinhViens.Count();
            int perPage = 10;
            int totalPages = (int)Math.Ceiling((double)total / perPage);
            if (page > totalPages)
            {
                page = totalPages;
            }
            else if (page < 1)
            {
                page = 1;
            }
            if (totalPages != 0)
            {
                sinhviens = _context.SinhViens.Skip((int)(perPage * (page - 1))).Take(perPage).ToList();
            }
            ViewBag.page = page;
            ViewBag.total = total;
            ViewBag.Current = "Student";
            ViewBag.isSearch = false;

            ViewBag.SearchBy = "";
            ViewBag.SortOrder = "";
            ViewBag.OrderBy = "";
            ViewBag.Keyword = "";
            return View(sinhviens);
        }
        public IActionResult SearchStudent(String SearchBy, String SortOrder, String OrderBy, String Keyword, int page = 1)
        {
            List<SinhVien> students = new List<SinhVien>();
            string searchBy = SearchBy;
            string sortOrder = SortOrder;
            string orderBy = OrderBy;
            String keyWord = Keyword;
            switch (SearchBy.Trim())
            {
                case "name":
                    {
                        students = _context.SinhViens.Where(x => x.HoTen.Trim().ToLower().Contains(Keyword.ToLower().Trim())).ToList();
                        break;
                    }
                case "email":
                    {
                        students = _context.SinhViens.Where(x => x.Email.Trim().ToLower().Contains(Keyword.ToLower().Trim())).ToList();
                        break;
                    }
                case "phone":
                    {
                        students = _context.SinhViens.Where(x => x.SoDienThoai.Trim().ToLower().Contains(Keyword.ToLower().Trim())).ToList();
                        break;
                    }
            }
            bool isAsc = SortOrder == "asc";
            switch (OrderBy)
            {
                case "id":
                    {
                        students = isAsc ? students.OrderBy(x => x.MaSinhVien).ToList() : students.OrderByDescending(x => x.MaSinhVien).ToList();
                        break;
                    }
                case "birth":
                    {
                        students = isAsc ? students.OrderBy(x => x.NgaySinh).ToList() : students.OrderByDescending(x => x.NgaySinh).ToList();
                        break;
                    }
                case "year":
                    {
                        students = isAsc ? students.OrderBy(x => x.NienKhoa).ToList() : students.OrderByDescending(x => x.NienKhoa).ToList();
                        break;
                    }
                case "name":
                    {
                        students = isAsc ? students.OrderBy(x => x.HoTen).ToList() : students.OrderByDescending(x => x.HoTen).ToList();
                        break;
                    }
            }
            int total = students.Count();
            int perPage = 10;
            int totalPages = (int)Math.Ceiling((double)total / perPage);
            if (page > totalPages)
            {
                page = totalPages;
            }
            else if (page < 1)
            {
                page = 1;
            }
            if (totalPages != 0)
            {
                students = students.Skip((int)(perPage * (page - 1))).Take(perPage).ToList();
            }
            ViewBag.Current = "Student";
            ViewBag.page = page;
            ViewBag.total = total;
            ViewBag.isSearch = true;

            ViewBag.SearchBy = searchBy;
            ViewBag.SortOrder = sortOrder;
            ViewBag.OrderBy = orderBy;
            ViewBag.Keyword = keyWord;
            return View("Student", students);
        }

        public IActionResult ViewStudent(int? msv)
        {
            if (HttpContext.Session.GetString("isAdmin") == null || HttpContext.Session.GetString("isAdmin") != "1")
            {
                return NotFound();
            }
            if (msv == null)
            {
                return NotFound();
            }
            SinhVien sinhVien = _context.SinhViens.FirstOrDefault(x => x.MaSinhVien == msv);
            if (sinhVien == null)
            {
                return NotFound();
            }
            sinhVien.tailieus = _context.TaiLieus.Where(x => x.MaSinhVien == sinhVien.MaSinhVien).ToList();
            sinhVien.giaDinhSinhVien = _context.GiaDinhSinhViens.FirstOrDefault(x => x.IdSinhVien == sinhVien.MaSinhVien);
            ViewBag.ThongBao = new List<string>();
            ViewBag.TypeTB = "";
            ViewBag.Current = "Student";
            ViewBag.isEdit = false;
            return View(sinhVien);
        }
        public IActionResult AddStudent()
        {
            if (HttpContext.Session.GetString("isAdmin") == null || HttpContext.Session.GetString("isAdmin") != "1")
            {
                return NotFound();
            }
            ViewBag.Current = "Student";
            ViewBag.TypeTB = "";
            ViewBag.ThongBao = new List<string>();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddStudent(SinhVien? sinhvien, string BirthDay, string tk, string mk, IFormFile Image)
        {
            ViewBag.TypeTB = "";
            List<string> notis = new List<string>();
            if (HttpContext.Session.GetString("isAdmin") == null || HttpContext.Session.GetString("isAdmin") != "1" || sinhvien == null)
            {
                return NotFound();
            }
            try
            {
                sinhvien.NgaySinh = DateTime.Parse(BirthDay);
                try
                {
                    sinhvien.Username = tk;
                    if (Image != null)
                    {
                        string fileName = sinhvien.MaSinhVien.ToString() + ".jpg";
                        bool uploadSuccess = await Utils.FileUtil.SaveFile("avatars", fileName, Image);
                        if (uploadSuccess)
                        {
                            sinhvien.HinhAnh = fileName;
                        }
                    }
                    _context.SinhViens.Add(sinhvien);
                    Account account = new Account();
                    account.Email = sinhvien.Email;
                    account.Username = tk;
                    account.Password = mk;
                    account.isAdmin = false;
                    _context.Accounts.Add(account);
                    _context.SaveChanges();

                    notis.Add("Thêm sinh viên thành công !");
                    ViewBag.TypeTB = "success";
                }
                catch (Exception e)
                {
                    notis.Add("Có lỗi xảy ra! " + e.Message);
                    ViewBag.TypeTB = "danger";
                }

            }
            catch
            {
                notis.Add("Ngày sinh không hợp lệ !");
                ViewBag.TypeTB = "danger";
            }
            ViewBag.ThongBao = notis;
            ViewBag.Current = "Student";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditStudent(SinhVien? sinhvien, string BirthDay, IFormFile Image, string new_password)
        {
            ViewBag.TypeTB = "";
            List<string> notis = new List<string>();
            if (HttpContext.Session.GetString("isAdmin") == null || HttpContext.Session.GetString("isAdmin") != "1" || sinhvien == null)
            {
                return NotFound();
            }
            var oldSinhVien = _context.SinhViens.FirstOrDefault(x => x.MaSinhVien == sinhvien.MaSinhVien);
            if (oldSinhVien == null)
            {
                return NotFound();
            }
            try
            {
                oldSinhVien.NgaySinh = DateTime.Parse(BirthDay);
                oldSinhVien.HoTen = sinhvien.HoTen;
                oldSinhVien.Email = sinhvien.Email;
                oldSinhVien.GioiTinh = sinhvien.GioiTinh;
                oldSinhVien.SoDienThoai = sinhvien.SoDienThoai;
                if (Image != null)
                {
                    string fileName = sinhvien.MaSinhVien.ToString() + ".jpg";
                    bool uploadSuccess = await Utils.FileUtil.SaveFile("avatars", fileName, Image);
                    if (uploadSuccess)
                    {
                        oldSinhVien.HinhAnh = fileName;
                    }
                }

                //Thay đổi mật khẩu
                if (new_password != null && new_password != "")
                {
                    if (new_password.Length < 6)
                    {
                        notis.Add("Mật khẩu phải có ít nhất 6 ký tự !");
                        ViewBag.TypeTB = "danger";
                        ViewBag.ThongBao = notis;
                        ViewBag.Current = "Student";
                        ViewBag.isEdit = true;
                        return View("ViewStudent", oldSinhVien);
                    }
                    Account? account = _context.Accounts.FirstOrDefault(x => x.Username == oldSinhVien.Username);
                    if (account != null)
                    {
                        account.Password = new_password;
                        notis.Add("Reset mật khẩu thành công cho tài khoản " + oldSinhVien.Username + " thành công !");
                    }

                }

                _context.SaveChanges();
                notis.Add("Cập nhật thông tin thành công !");
                ViewBag.TypeTB = "success";
                oldSinhVien.tailieus = _context.TaiLieus.Where(x => x.MaSinhVien == oldSinhVien.MaSinhVien).ToList();
            }
            catch
            {
                notis.Add("Ngày sinh không hợp lệ !");
                ViewBag.TypeTB = "danger";
            }
            ViewBag.ThongBao = notis;
            ViewBag.Current = "Student";
            ViewBag.isEdit = true;
            return View("ViewStudent", oldSinhVien);
        }
        public IActionResult DeleteStudent(int? msv)
        {
            if (HttpContext.Session.GetString("isAdmin") == null || HttpContext.Session.GetString("isAdmin") != "1")
            {
                return NotFound();
            }
            if (msv == null)
            {
                return NotFound();
            }
            SinhVien sinhVien = _context.SinhViens.FirstOrDefault(x => x.MaSinhVien == msv);
            ViewBag.Current = "Student";
            if (sinhVien == null)
            {
                return NotFound();
            }
            return View("AcceptDelete", msv);
        }
        public IActionResult PerformDelete(int? msv)
        {
            if (HttpContext.Session.GetString("isAdmin") == null || HttpContext.Session.GetString("isAdmin") != "1")
            {
                return NotFound();
            }
            if (msv == null)
            {
                return NotFound();
            }
            SinhVien sinhVien = _context.SinhViens.FirstOrDefault(x => x.MaSinhVien == msv);
            if (sinhVien == null)
            {
                return NotFound();
            }
            _context.TaiLieus.RemoveRange(_context.TaiLieus.Where(m => m.MaSinhVien == sinhVien.MaSinhVien));
            _context.GiaDinhSinhViens.RemoveRange(_context.GiaDinhSinhViens.Where(m => m.IdSinhVien == sinhVien.MaSinhVien));
            _context.DonDangKys.RemoveRange(_context.DonDangKys.Where(m => m.MaSinhVien == sinhVien.MaSinhVien));
            _context.Accounts.RemoveRange(_context.Accounts.Where(m => m.Username == sinhVien.Username));
            _context.SinhViens.Remove(sinhVien);
            _context.SaveChanges();
            return RedirectToAction("Student");
        }
        //Course
        public IActionResult Course(int? page)
        {
            if (page == null || page < 1)
            {
                page = 1;
            }
            if (HttpContext.Session.GetString("isAdmin") == null || HttpContext.Session.GetString("isAdmin") != "1")
            {
                return NotFound();
            }
            List<DuHoc> courses = new List<DuHoc>();
            int total = _context.DuHocs.Count();
            int perPage = 10;
            int totalPages = (int)Math.Ceiling((double)total / perPage);
            if (page > totalPages)
            {
                page = totalPages;
            }
            else if (page < 1)
            {
                page = 1;
            }
            if (totalPages != 0)
            {
                courses = _context.DuHocs.Skip((int)(perPage * (page - 1))).Take(perPage).ToList();
            }
            ViewBag.page = page;
            ViewBag.total = total;
            ViewBag.Current = "Course";
            ViewBag.isSearch = false;

            ViewBag.SearchBy = "";
            ViewBag.SortOrder = "";
            ViewBag.OrderBy = "";
            ViewBag.Keyword = "";
            return View(courses);

        }
        public IActionResult SearchCourse(String SearchBy, String SortOrder, String OrderBy, String Keyword, int page = 1)
        {
            List<DuHoc> courses = new List<DuHoc>();
            string searchBy = SearchBy;
            string sortOrder = SortOrder;
            string orderBy = OrderBy;
            String keyWord = Keyword;
            switch (SearchBy.Trim())
            {
                case "name":
                    {
                        courses = _context.DuHocs.Where(x => x.Ten.Trim().ToLower().Contains(Keyword.ToLower().Trim())).ToList();
                        break;
                    }
                case "school":
                    {
                        courses = _context.DuHocs.Where(x => x.TenTruong.Trim().ToLower().Contains(Keyword.ToLower().Trim())).ToList();
                        break;
                    }
                case "country":
                    {
                        courses = _context.DuHocs.Where(x => x.QuocGia.Trim().ToLower().Contains(Keyword.ToLower().Trim())).ToList();
                        break;
                    }
            }
            bool isAsc = SortOrder == "asc";
            switch (OrderBy)
            {
                case "name":
                    {
                        courses = isAsc ? courses.OrderBy(x => x.Ten).ToList() : courses.OrderByDescending(x => x.Ten).ToList();
                        break;
                    }
                case "price":
                    {
                        courses = isAsc ? courses.OrderBy(x => x.HocPhi).ToList() : courses.OrderByDescending(x => x.HocPhi).ToList();
                        break;
                    }
            }
            int total = courses.Count();
            int perPage = 10;
            int totalPages = (int)Math.Ceiling((double)total / perPage);
            if (page > totalPages)
            {
                page = totalPages;
            }
            else if (page < 1)
            {
                page = 1;
            }
            if (totalPages != 0)
            {
                courses = courses.Skip((int)(perPage * (page - 1))).Take(perPage).ToList();
            }
            ViewBag.Current = "Course";
            ViewBag.page = page;
            ViewBag.total = total;
            ViewBag.isSearch = true;

            ViewBag.SearchBy = searchBy;
            ViewBag.SortOrder = sortOrder;
            ViewBag.OrderBy = orderBy;
            ViewBag.Keyword = keyWord;
            return View("Course", courses);
        }
        public IActionResult AddCourse()
        {
            if (HttpContext.Session.GetString("isAdmin") == null || HttpContext.Session.GetString("isAdmin") != "1")
            {
                return NotFound();
            }
            ViewBag.Current = "Course";
            ViewBag.TypeTB = "";
            ViewBag.ThongBao = new List<string>();
            ViewBag.ListTypeTaiLieu = _context.TypeDocuments.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCourse(DuHoc? duhoc, IFormFile Image, string Requirements)
        {
            ViewBag.TypeTB = "";
            List<string> notis = new List<string>();
            if (HttpContext.Session.GetString("isAdmin") == null || HttpContext.Session.GetString("isAdmin") != "1" || duhoc == null)
            {
                return NotFound();
            }
            if (Image != null)
            {
                // Generate ra 1 uuid ngẫu nhiên cho filename ví dụ New GUID: 4cc7b483-3354-4a43-8ed8-d7dc3be2fc36
                string fileName = System.Guid.NewGuid().ToString() + ".jpg";
                bool uploadSuccess = await Utils.FileUtil.SaveFile("courses", fileName, Image);
                if (uploadSuccess)
                {
                    duhoc.Image = fileName;
                }
            }
            _context.DuHocs.Add(duhoc);
            _context.SaveChanges();
            int makhoaduhoc = duhoc.MaKhoaDuHoc;
            string[] yeuCau = Requirements.Split("^");
            foreach (string item in yeuCau)
            {
                string[] temp = item.Split("|");
                YeuCauDuHoc yc = new YeuCauDuHoc();
                yc.MaKhoaDuHoc = makhoaduhoc;
                yc.LoaiTaiLieu = temp[0];
                yc.MoTa = temp[1];
                yc.TenTaiLieu = _context.TypeDocuments.FirstOrDefault(x => x.LoaiTaiLieu == temp[0]).TenTaiLieu;
                Console.WriteLine(yc.TenTaiLieu);
                _context.YeuCauDuHocs.Add(yc);
            }
            _context.SaveChanges();
            ViewBag.TypeTB = "success";
            notis.Add("Thêm khóa học thành công !");
            ViewBag.ThongBao = notis;
            ViewBag.Current = "Course";
            ViewBag.ListTypeTaiLieu = _context.TypeDocuments.ToList();
            return View();
        }
        public IActionResult EditCourse(int? id)
        {
            var user = SessionUtil.getAccountFromUsr(_context, HttpContext.Session);
            if (user == null || !user.isAdmin) return BadRequest();
            var course = _context.DuHocs.FirstOrDefault(m => m.MaKhoaDuHoc == id);
            if (course == null) return NotFound();
            List<YeuCauDuHoc> ycDuHocs = _context.YeuCauDuHocs.Where(m => m.MaKhoaDuHoc == course.MaKhoaDuHoc).ToList();
            ViewBag.YeuCauDuHocs = ycDuHocs;
            ViewBag.Current = "Course";
            ViewBag.TypeTB = "";
            ViewBag.ThongBao = new List<string>();
            ViewBag.ListTypeTaiLieu = _context.TypeDocuments.ToList();
            return View(course);
        }
        [HttpPost]
        public async Task<IActionResult> EditCourse(DuHoc? duhoc, IFormFile Image, string Requirements)
        {
            ViewBag.TypeTB = "";
            List<string> notis = new List<string>();
            var user = SessionUtil.getAccountFromUsr(_context, HttpContext.Session);
            var old_dh = _context.DuHocs.FirstOrDefault(m => m.MaKhoaDuHoc == duhoc.MaKhoaDuHoc);
            if (user == null || !user.isAdmin || duhoc == null || old_dh == null) return BadRequest();
            if (Image != null)
            {
                // Generate ra 1 uuid ngẫu nhiên cho filename ví dụ New GUID: 4cc7b483-3354-4a43-8ed8-d7dc3be2fc36
                string fileName = System.Guid.NewGuid().ToString() + ".jpg";
                bool uploadSuccess = await Utils.FileUtil.SaveFile("courses", fileName, Image);
                if (uploadSuccess)
                {
                    old_dh.Image = fileName;
                }
            }
            //Change info
            old_dh.Ten = duhoc.Ten;
            old_dh.TenTruong = duhoc.TenTruong;
            old_dh.QuocGia = duhoc.QuocGia;
            old_dh.Website = duhoc.Website;
            old_dh.Email = duhoc.Email;
            old_dh.SoDienThoai = duhoc.SoDienThoai;
            old_dh.HocPhi = duhoc.HocPhi;
            old_dh.ThoiGian = duhoc.ThoiGian;
            old_dh.MoTa = duhoc.MoTa;
            //Xoa cai cu
            var oldYeuCau = _context.YeuCauDuHocs.Where(m => m.MaKhoaDuHoc == duhoc.MaKhoaDuHoc).ToList();
            foreach (var item in oldYeuCau)
            {
                _context.YeuCauDuHocs.Remove(item);
            }

            int makhoaduhoc = duhoc.MaKhoaDuHoc;
            string[] yeuCau = Requirements.Split("^");
            foreach (string item in yeuCau)
            {
                string[] temp = item.Split("|");
                YeuCauDuHoc yc = new YeuCauDuHoc();
                yc.MaKhoaDuHoc = makhoaduhoc;
                yc.LoaiTaiLieu = temp[0];
                yc.MoTa = temp[1];
                yc.TenTaiLieu = _context.TypeDocuments.FirstOrDefault(x => x.LoaiTaiLieu == temp[0]).TenTaiLieu;
                Console.WriteLine(yc.TenTaiLieu);
                _context.YeuCauDuHocs.Add(yc);
            }
            _context.SaveChanges();
            ViewBag.TypeTB = "success";
            List<YeuCauDuHoc> ycDuHocs = _context.YeuCauDuHocs.Where(m => m.MaKhoaDuHoc == makhoaduhoc).ToList();
            ViewBag.YeuCauDuHocs = ycDuHocs;
            notis.Add("Sửa khóa học thành công !");
            ViewBag.ThongBao = notis;
            ViewBag.Current = "Course";
            ViewBag.ListTypeTaiLieu = _context.TypeDocuments.ToList();
            return View(duhoc);
        }
        public IActionResult DeleteCourse(int? id)
        {
            if (HttpContext.Session.GetString("isAdmin") == null || HttpContext.Session.GetString("isAdmin") != "1")
            {
                return NotFound();
            }
            if (id == null)
            {
                return NotFound();
            }
            DuHoc h = _context.DuHocs.FirstOrDefault(x => x.MaKhoaDuHoc == id);
            ViewBag.Current = "Course";
            if (h == null)
            {
                return NotFound();
            }
            return View(id);
        }
        public IActionResult PerformDeleteCourse(int? id)
        {
            if (HttpContext.Session.GetString("isAdmin") == null || HttpContext.Session.GetString("isAdmin") != "1")
            {
                return NotFound();
            }
            if (id == null)
            {
                return NotFound();
            }
            DuHoc h = _context.DuHocs.FirstOrDefault(x => x.MaKhoaDuHoc == id);
            if (h == null)
            {
                return NotFound();
            }
            _context.YeuCauDuHocs.RemoveRange(_context.YeuCauDuHocs.Where(m => m.MaKhoaDuHoc == h.MaKhoaDuHoc));
            _context.DonDangKys.RemoveRange(_context.DonDangKys.Where(m => m.MaKhoaDuHoc == h.MaKhoaDuHoc));
            _context.DuHocs.Remove(h);
            _context.SaveChanges();
            return RedirectToAction("Course");
        }
        public IActionResult Form(int page = 1)
        {
            ViewBag.Current = "Register";
            var user = SessionUtil.getAccountFromUsr(_context, HttpContext.Session);
            if (user == null || !user.isAdmin) return BadRequest();
            var datas = _context.DonDangKys.OrderByDescending(m => m.TrangThai == 0).ThenByDescending(m => m.NgayTao);
            int totalPage = (int)Math.Ceiling((double)datas.Count() / 7);
            if (page < 1) page = 1;
            if (page > totalPage) page = totalPage;
            var result = datas.Skip((page - 1) * 7).Take(7).ToList();
            result.ForEach(m => m.KhoaDuHoc = _context.DuHocs.FirstOrDefault(d => d.MaKhoaDuHoc == m.MaKhoaDuHoc));
            ViewBag.page = page;
            ViewBag.totalPage = totalPage;
            return View(result);
        }
        public IActionResult SearchRegister(string? Keyword)
        {
            var user = SessionUtil.getAccountFromUsr(_context, HttpContext.Session);
            if (user == null || !user.isAdmin) return BadRequest();
            if (Keyword == null || Keyword == "")
            {
                return View("NotFoundRegister");
            }
            int id = int.Parse(Keyword);
            var data = _context.DonDangKys.FirstOrDefault(m => m.MaDangKy == id);
            if (data == null)
            {
                return View("NotFoundRegister");
            }
            return RedirectToAction("ViewRegister", new { id_register = id });
        }
        public IActionResult ViewRegister(int? id_register)
        {
            var user = SessionUtil.getAccountFromUsr(_context, HttpContext.Session);
            if (user == null || !user.isAdmin) return BadRequest();
            var data = _context.DonDangKys.FirstOrDefault(m => m.MaDangKy == id_register);
            if (data == null)
            {
                return View("NotFoundRegister");
            }
            data.KhoaDuHoc = _context.DuHocs.FirstOrDefault(d => d.MaKhoaDuHoc == data.MaKhoaDuHoc);
            List<YeuCauDuHoc> yeuCauDuHocs = _context.YeuCauDuHocs.Where(m => m.MaKhoaDuHoc == data.KhoaDuHoc.MaKhoaDuHoc).ToList();
            for (int i = 0; i < yeuCauDuHocs.Count; i++)
            {
                yeuCauDuHocs[i].taiLieuSV = _context.TaiLieus.FirstOrDefault(m => m.LoaiTaiLieu == yeuCauDuHocs[i].LoaiTaiLieu && m.MaSinhVien == data.MaSinhVien);
            }
            ViewBag.Current = "Register";
            ViewBag.SinhVien = _context.SinhViens.FirstOrDefault(m => m.MaSinhVien == data.MaSinhVien);
            ViewBag.YeuCau = yeuCauDuHocs;
            return View(data);
        }
        [HttpPost]
        public IActionResult ChangeStatusRegister(int? IdRegister, int? StatusCode, string Note = "")
        {
            var user = SessionUtil.getAccountFromUsr(_context, HttpContext.Session);
            if (user == null || !user.isAdmin) return BadRequest();
            if (IdRegister == null || StatusCode == null)
            {
                return NotFound();
            }
            ViewBag.Current = "Register";
            var data = _context.DonDangKys.FirstOrDefault(m => m.MaDangKy == IdRegister);
            if (data == null)
            {
                return NotFound();
            }
            data.TrangThai = StatusCode.Value;
            data.NguoiDuyet = user.Username;
            data.NgayCapNhat = DateTime.Now;
            data.GhiChuAdmin = Note;
            _context.SaveChanges();
            return RedirectToAction("ViewRegister", new { id_register = IdRegister });
        }
        public IActionResult Profile()
        {
            var user = SessionUtil.getAccountFromUsr(_context, HttpContext.Session);
            if (user == null || !user.isAdmin) return BadRequest();
            GiangVien giangvien = SessionUtil.getGiangVienFromUsr(_context, HttpContext.Session);
            if (giangvien == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.Current = "Profile";
            ViewBag.Notifications = new List<CustomNotification>();
            return View(giangvien);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(GiangVien? gv, IFormFile Image, string? new_password, string birth)
        {
            ViewBag.Current = "Profile";
            var x = new List<CustomNotification>();
            var user = SessionUtil.getAccountFromUsr(_context, HttpContext.Session);
            if (user == null || !user.isAdmin) return BadRequest();
            GiangVien giangvien = SessionUtil.getGiangVienFromUsr(_context, HttpContext.Session);
            if (giangvien == null || gv == null)
            {
                return RedirectToAction("Login");
            }
            if (Image != null)
            {
                string uuid = System.Guid.NewGuid().ToString();
                bool isSuccess = await Utils.FileUtil.SaveFile("avatars", uuid + "_" + Image.FileName, Image);
                if (isSuccess)
                {
                    giangvien.AnhDaiDien = uuid + "_" + Image.FileName;
                }
            }
            //Get Entity
            user = _context.Accounts.FirstOrDefault(m => m.Username == giangvien.Username);
            giangvien = _context.GiangViens.FirstOrDefault(m => m.MaGiangVien == giangvien.MaGiangVien);
            if (user == null || giangvien == null)
            {
                return RedirectToAction("Login");
            }

            //Thay đổi mật khẩu
            if (new_password != null && new_password != "")
            {
                if (new_password.Length < 6)
                {
                    var z = new List<CustomNotification>
                    {
                        CustomNotification.Error("Mật khẩu phải có ít nhất 6 ký tự !")
                    };
                    ViewBag.Notifications = z;
                    return View("Profile", giangvien);
                }
                user.Password = new_password;
            }


            giangvien.Email = gv.Email;
            giangvien.SoDienThoai = gv.SoDienThoai;
            giangvien.DiaChi = gv.DiaChi;
            giangvien.HocVi = gv.HocVi;
            try
            {
                giangvien.NgaySinh = DateTime.Parse(birth);
            }
            catch
            {
                x.Add(CustomNotification.Error("Ngày sinh không hợp lệ !"));
            }

            x.Add(CustomNotification.Success("Cập nhật thông tin thành công !"));
            ViewBag.Notifications = x;
            _context.SaveChanges();
            return View("Profile", giangvien);
        }

        public IActionResult RejectRegister(int? id_register)
        {
            var user = SessionUtil.getAccountFromUsr(_context, HttpContext.Session);
            if (user == null || !user.isAdmin) return BadRequest();
            if (id_register == null)
            {
                return NotFound();
            }
            var data = _context.DonDangKys.FirstOrDefault(m => m.MaDangKy == id_register);
            if (data == null)
            {
                return NotFound();
            }
            data.TrangThai = -1;
            data.NguoiDuyet = user.Username;
            data.NgayCapNhat = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("Form");
        }
    }
}
