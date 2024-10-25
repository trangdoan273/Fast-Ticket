using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TICKETBOX.Models;
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

        //Quản lý vé
        public IActionResult TicketManagement()
        {
            using (var db = new FastticketContext())
            {
                var tickets = db.Tickets.Include(t => t.Movie).Include(t => t.Seat).Include(t => t.User).ToList();
                return View(tickets);
            }
        }

        //Đổi trạng thái vé
        [HttpPost]
        public IActionResult ChangeAllTicketStatus()
        {
            using (var db = new FastticketContext())
            {
                var tickets = db.Tickets.ToList();
                foreach (var ticket in tickets)
                {
                    ticket.TicketStatus = ticket.TicketStatus == "Booked" ? "Used" : "Used";
                }
                db.SaveChanges();
            }
            return RedirectToAction("TicketManagement");
        }
        [HttpGet]
        public IActionResult Post()
        {
            using (var db = new FastticketContext())
            {
                var infoList = db.Infos.ToList();
                return View(infoList);
            }
        }
        //Tạo post
        [HttpGet]
        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(Info newInfo)
        {
            if (ModelState.IsValid)
            {
                using (var db = new FastticketContext())
                {
                    db.Infos.Add(newInfo);
                    db.SaveChanges();
                }
                return RedirectToAction("Post", "Admin"); // Chuyển hướng về trang danh sách bài đăng
            }
            return View(newInfo); // Trả lại model để hiển thị lỗi nếu có
        }
        //Chức năng xóa post
        public IActionResult DeletePost(int id)
        {
            using (var db = new FastticketContext())
            {
                var info = db.Infos.FirstOrDefault(u => u.InfoId == id);
                if (info != null)
                {
                    db.Infos.Remove(info);
                    db.SaveChanges();
                }
                return RedirectToAction("Post", "Admin");
            }
        }
        //xem post
        public IActionResult ViewPost(int id)
        {
            using (var db = new FastticketContext())
            {
                var post = db.Infos.FirstOrDefault(p => p.InfoId == id);
                if (post == null)
                {
                    return NotFound();
                }
                return View(post); // Trả về view để xem bài đăng
            }
        }
        // sửa post
        // Sửa bài đăng (GET)
        [HttpGet]
        public IActionResult EditPost(int id)
        {
            using (var db = new FastticketContext())
            {
                var post = db.Infos.FirstOrDefault(p => p.InfoId == id);
                if (post == null)
                {
                    return NotFound(); // Trả về 404 nếu không tìm thấy bài đăng
                }
                return View(post); // Trả về view để sửa bài đăng
            }
        }

        // Sửa bài đăng (POST)
        [HttpPost]
        public IActionResult EditPost(Info updatedInfo)
        {
            if (ModelState.IsValid)
            {
                using (var db = new FastticketContext())
                {
                    var existingPost = db.Infos.FirstOrDefault(p => p.InfoId == updatedInfo.InfoId);
                    if (existingPost == null)
                    {
                        return NotFound(); // Trả về 404 nếu không tìm thấy bài đăng
                    }

                    // Cập nhật thông tin bài đăng
                    existingPost.InfoTitle = updatedInfo.InfoTitle;
                    existingPost.InfoContent = updatedInfo.InfoContent;
                    existingPost.InfoImage = updatedInfo.InfoImage;

                    db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                }
                return RedirectToAction("Post", "Admin"); // Chuyển hướng về trang danh sách bài đăng
            }
            return View(updatedInfo); // Trả lại model để hiển thị lỗi nếu có
        }
        public IActionResult Admin1()
        {
            var userID = User.Identity.Name;
            var user = new User();
            using (var db = new FastticketContext())
            {
                user = db.Users.FirstOrDefault(u => u.UserName == userID);
                if (user == null || user.Role == "Admin")
                {
                    return View(user);
                }

            }
            return View(user);
        }

        public IActionResult DetailAdmin1(int id)
        {
            using (var db = new FastticketContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserId == 1);

                if (user == null)
                {
                    return NotFound(); // Trả về 404 nếu không tìm thấy người dùng
                }

                var viewModel = new UpdateUserModel()
                {
                    UserId = user.UserId,
                    UserFullname = user.FullName,
                    UserPhoneNumber = user.UserPhoneNumber,
                    UserSex = user.Sex,
                    DoB = user.DoB,
                    UserAddress = user.UserAddress,
                    UserEmail = user.UserEmail
                };

                return View(viewModel); // Trả về view với thông tin người dùng
            }
        }
        [HttpPost]
        public IActionResult DetailAdmin1(UpdateUserModel model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new FastticketContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.UserId == model.UserId);

                    if (user == null)
                    {
                        return NotFound(); // Trả về 404 nếu không tìm thấy người dùng
                    }

                    // Cập nhật thông tin người dùng
                    user.FullName = model.UserFullname;
                    user.UserPhoneNumber = model.UserPhoneNumber;
                    user.Sex = model.UserSex;
                    user.DoB = model.DoB;
                    user.UserAddress = model.UserAddress;
                    user.UserEmail = model.UserEmail;

                    db.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu
                }

                return RedirectToAction("DetailAdmin1"); // Chuyển hướng đến danh sách người dùng hoặc trang khác
            }

            return View(model); // Nếu model không hợp lệ, trả về view với dữ liệu đã nhập
        }

        public IActionResult ChangePasswordAdmin()
        {
            var currentUserId = User.Identity.Name;

            using (var db = new FastticketContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserId == 1);
                if (user == null || user.UserName != currentUserId)
                {
                    return RedirectToAction("AccessDenied", "Home");
                }
            }
            var model = new ChangePasswordModel
            {
                UserId = 1
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult ChangePasswordAdmin(ChangePasswordModel model)
        {
            var userID = User.Identity.Name;
            if (ModelState.IsValid)
            {
                using (var db = new FastticketContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.UserId == model.UserId);
                    if (user != null && user.Role == "Admin")
                    {
                        user.UserPassword = model.NewPassword;
                        db.SaveChanges();

                        return RedirectToAction("DetailAdmin1", new { id = model.UserId });
                    }
                }
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult CreateMovie(Movie movie, IFormFile MovieImage)
        {
            if (ModelState.IsValid)
            {
                if (MovieImage != null && MovieImage.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets", MovieImage.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        MovieImage.CopyTo(stream);
                    }

                    movie.MovieImage = $"/assets/{MovieImage.FileName}";
                }
                using (var db = new FastticketContext())
                {
                    db.Movies.Add(movie); // Thêm movie vào db
                    db.SaveChanges(); // Lưu thay đổi
                }
                return RedirectToAction("CreateMovie"); // Chuyển hướng đến danh sách hoặc trang khác sau khi lưu
            }

            return View(movie); // Nếu model không hợp lệ, trả về view với dữ liệu đã nhập
        }
        [HttpGet]
        public IActionResult FixMovie(int id)
        {
            using (var db = new FastticketContext())
            {
                var movie = db.Movies.FirstOrDefault(m => m.MovieId == id);

                if (movie == null)
                {
                    return NotFound(); // Trả về 404 nếu không tìm thấy bộ phim
                }

                return View(movie); // Trả về view với dữ liệu bộ phim
            }
        }

        [HttpPost]
        public IActionResult FixMovie(Movie movie, IFormFile MovieImage)
        {
            using (var db = new FastticketContext())
            {
                var existingMovie = db.Movies.FirstOrDefault(m => m.MovieId == movie.MovieId);

                if (existingMovie == null)
                {
                    return NotFound(); // Trả về 404 nếu không tìm thấy bộ phim
                }

                // Cập nhật các thuộc tính của phim
                existingMovie.MovieName = movie.MovieName;
                existingMovie.MovieContent = movie.MovieContent;
                existingMovie.MovieGenre = movie.MovieGenre;
                existingMovie.MovieLabel = movie.MovieLabel;
                existingMovie.MovieFormat = movie.MovieFormat;
                existingMovie.MovieDirector = movie.MovieDirector;
                existingMovie.MovieActor = movie.MovieActor;
                existingMovie.ReleaseDate = movie.ReleaseDate;
                existingMovie.Duration = movie.Duration;
                existingMovie.Language = movie.Language;

                // Kiểm tra nếu có tải lên ảnh mới
                if (MovieImage != null && MovieImage.Length > 0)
                {
                    // Tạo đường dẫn để lưu ảnh
                    var filePath = Path.Combine("wwwroot/assets", MovieImage.FileName);

                    // Lưu ảnh vào thư mục
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        MovieImage.CopyTo(stream);
                    }

                    // Cập nhật đường dẫn ảnh trong cơ sở dữ liệu
                    existingMovie.MovieImage = "/assets/" + MovieImage.FileName;
                }

                // Lưu thay đổi vào database
                db.SaveChanges();

                // Điều hướng lại trang hoặc hiển thị thông báo thành công
                return RedirectToAction("FixMovie", new { id = existingMovie.MovieId });
            }
        }


        public IActionResult CreateMovie()
        {
            return View(); // Nếu model không hợp lệ, trả về view với dữ liệu đã nhập
        }
    }
}