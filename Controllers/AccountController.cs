using Microsoft.AspNetCore.Mvc;
using TICKETBOX.Models.Tables;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TICKETBOX.Models;

namespace TICKETBOX.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult SignIn()
        {

            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(User user)
        {

            if (string.IsNullOrEmpty(user.UserName))
            {
                ModelState.AddModelError("UserName", "Tên đăng nhập không được để trống!");
                return View();
            }
            if (string.IsNullOrEmpty(user.UserPassword))
            {
                ModelState.AddModelError("UserPassword", "Mật khẩu không được để trống!");
                return View();
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(user.UserName, @"[a-zA-z0-9]+$"))
            {
                ModelState.AddModelError("UserName", "Tên đăng nhập không được chứa ký tự đặc biệt!");
                return View();
            }

            using (var db = new FastticketContext())
            {
                var existUser = await db.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName && u.UserPassword == user.UserPassword);
                if (existUser == null)
                {
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không đúng!");
                    return View(user);
                }
                var dbUser = await db.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName && u.UserPassword == user.UserPassword);
                if (dbUser != null)
                {
                    List<Claim> claims = new List<Claim>(){
                         new Claim(ClaimTypes.NameIdentifier, dbUser.UserId.ToString()),
                        new Claim(ClaimTypes.Name, dbUser.UserName),
                        new Claim(ClaimTypes.Role, dbUser.Role)
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Index", "Home");
                }
                return View(user);
            }

        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel formData)
        {

            using (var db = new FastticketContext())
            {
                var existUser = await db.Users.FirstOrDefaultAsync(u => u.UserName == formData.UserName);
                if (existUser != null)
                {
                    ModelState.AddModelError("UserName", "Tên đăng nhập đã tồn tại.");
                    return View(formData);
                }

                var existEmail = await db.Users.FirstOrDefaultAsync(u => u.UserEmail == formData.UserEmail);
                if (existEmail != null)
                {
                    ModelState.AddModelError("UserEmail", "Email đã tồn tại.");
                    return View();
                }

                var newUser = new User
                {
                    UserName = formData.UserName,
                    UserEmail = formData.UserEmail,
                    UserPassword = formData.UserPassword,
                    Role = "user"
                };
                db.Users.Add(newUser);
                await db.SaveChangesAsync();

                return RedirectToAction("SignIn", "Account");
            }
        }
    }
}