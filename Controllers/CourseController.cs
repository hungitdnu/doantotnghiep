using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HeThongDangKyDuHoc.Models;
using HeThongDangKyDuHoc.Context;
using HeThongDangKyDuHoc.Utils;

namespace HeThongDangKyDuHoc.Controllers
{
    public class CourseController : Controller
    {
        private readonly MyContext _context;

        public CourseController(MyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int ?page)
        {
            int pageSize = 6;
            int maxPage = (int)Math.Ceiling((double)_context.DuHocs.Count() / pageSize);
            int pageNumber = (page ?? 1);
            if(pageNumber < 1)
            {
                pageNumber = 1;
            }
            else if(pageNumber > maxPage)
            {
                pageNumber = maxPage;
            }
            var list = await _context.DuHocs.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            ViewData["pageNumber"] = pageNumber;
            return View(list);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duHoc = await _context.DuHocs
                .FirstOrDefaultAsync(m => m.MaKhoaDuHoc == id);
            ViewData["recentList"] = _context.DuHocs.OrderByDescending(x => x.MaKhoaDuHoc).Take(4).ToList();
            if (duHoc == null)
            {
                return NotFound();
            }

            return View(duHoc);
        }

        public IActionResult Enroll(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var sinhvien = SessionUtil.getSinhVienFromUsr(_context, HttpContext.Session);
            var duhoc = _context.DuHocs.FirstOrDefault(x => x.MaKhoaDuHoc == id);
            if (sinhvien == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else if (duhoc == null)
            {
                return NotFound();
            }
            //Check exist register
            var register_old = _context.DonDangKys.FirstOrDefault(x => x.MaSinhVien == sinhvien.MaSinhVien && x.MaKhoaDuHoc == id);
            if (register_old != null)
            {
                return RedirectToAction("ViewRegisterStatus", "Account");
            }

            var register = new Register();
            register.MaSinhVien = sinhvien.MaSinhVien;
            register.MaKhoaDuHoc = (int)id;
            register.NgayTao = DateTime.Now;
            register.NgayCapNhat = DateTime.Now;
            register.TrangThai = 0;
            register.GhiChu = "Đăng ký du học cho sinh viên " + sinhvien.MaSinhVien;
            register.GhiChuAdmin = "";
            register.NguoiDuyet = "";
            _context.Add(register);
            _context.SaveChanges();
            return RedirectToAction("ViewRegisterStatus", "Account");
        }
        [HttpPost]
        public IActionResult RequestAgain(int MaDangKy, string GhiChu)
        {
            var register = _context.DonDangKys.FirstOrDefault(x => x.MaDangKy == MaDangKy);
            var sinhvien = SessionUtil.getSinhVienFromUsr(_context, HttpContext.Session);
            if (register == null || sinhvien == null)
            {
                return NotFound();
            }
            register.TrangThai = 0;
            register.GhiChu = GhiChu;
            register.NgayCapNhat = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("ViewRegisterStatus", "Account");
        }
    }
}
