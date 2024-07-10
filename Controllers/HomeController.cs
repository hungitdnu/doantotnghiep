using HeThongDangKyDuHoc.Context;
using HeThongDangKyDuHoc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HeThongDangKyDuHoc.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyContext _context;
        public HomeController(MyContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SendMessage(TinNhan tinnhan)
        {
            if(tinnhan == null)
            {
                return View("Message",(string)"Please fill in the form !");
            }
            _context.TinNhans.Add(tinnhan);
            _context.SaveChanges();
            return View("Message",(string)"We've received your message !");
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}
