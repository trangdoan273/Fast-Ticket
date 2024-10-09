using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Iana;
using TICKETBOX.Models.Tables;


namespace TICKETBOX.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }
        //Quản lý
        public IActionResult Management(string roleFilter)
        {
            using (var db = new FastticketContext())
            {
                var users = db.Users.AsQueryable();
                if (!string.IsNullOrEmpty(roleFilter))
                {
                    users = users.Where(u => u.Role == roleFilter);
                }
                var userList = users.ToList();
                return View(userList);
            }
        }
        //Chức năng xóa
        public IActionResult DeleteUser(int id)
        {
            using (var db = new FastticketContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserId == id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
                return RedirectToAction("Management", "Admin");
            }
        }
        //Tạo Admin
        [HttpGet]
        public IActionResult CreateAdmin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAdmin(User newAdmin)
        {
            if (ModelState.IsValid)
            {
                using (var db = new FastticketContext())
                {
                    newAdmin.Role = "Admin";
                    db.Users.Add(newAdmin);
                    db.SaveChanges();
                }
                return RedirectToAction("Management");
            }
            return View(newAdmin);
        }
    }
}