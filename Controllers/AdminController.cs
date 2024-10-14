using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Iana;
using TICKETBOX.Models.Tables;


namespace TICKETBOX.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }
        //HomeAdmin
        public IActionResult HomeAdmin()
        {
            using (var db = new FastticketContext())
            {
                var movies = db.Movies.ToList();
                return View(movies);
            }
        }
        //Chức năng xóa phim
        public IActionResult DeleteMovie(int id)
        {
            using (var db = new FastticketContext())
            {
                var showdates = db.Showdates.Where(sd => sd.MovieId == id).ToList();
                db.Showdates.RemoveRange(showdates);

                var showtimes = db.Showtimes.Where(st => st.MovieId == id).ToList();
                db.Showtimes.RemoveRange(showtimes);

                var movie = db.Movies.FirstOrDefault(u => u.MovieId == id);
                if (movie != null)
                {
                    db.Movies.Remove(movie);
                    db.SaveChanges();
                }
                return RedirectToAction("HomeAdmin", "Admin");
            }
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
        //Chức năng xóa người dùng
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

        public IActionResult TicketManagement()
        {
            using (var db = new FastticketContext())
            {
                var tickets = db.Tickets.Include(t => t.Movie).Include(t => t.Seat).Include(t => t.User).ToList();
                return View(tickets);
            }
        }

        [HttpPost]
        public IActionResult ChangeAllTicketStatus()
        {
            using (var db = new FastticketContext())
            {
                var tickets = db.Tickets.ToList();
                foreach (var ticket in tickets)
                {
                    // Thay đổi trạng thái vé ở đây
                    ticket.TicketStatus = ticket.TicketStatus == "Booked" ? "Used" : "Used"; // Ví dụ trạng thái
                }
                db.SaveChanges();
            }
            return RedirectToAction("TicketManagement");
        }
    }
}