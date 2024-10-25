using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TICKETBOX.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using TICKETBOX.Models.Tables;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TICKETBOX.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    //Trang chủ người dùng
    [Authorize(Roles = "User")]
    public IActionResult Index()
    {
        using (var db = new FastticketContext())
        {
            var movies = db.Movies.ToList();
            return View(movies);
        }
    }
    //Chuyển trang của Logo
    public IActionResult NavigateHome()
    {
        var userRole = HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
        if (userRole == "Admin")
        {
            return RedirectToAction("HomeAdmin", "Admin");
        }
        else
        {
            return RedirectToAction("Index", "Home");
        }
    }
    //Thông tin phim
    public IActionResult Movies(int id){
       using (var db = new FastticketContext()){
        var movies = db.Movies.FirstOrDefault(m => m.MovieId == id);

        ViewBag.MovieInfo = new MovieModel(){
            Id = movies.MovieId,
            MovieName = movies.MovieName,
            Content = movies.MovieContent,
            Type = movies.MovieLabel,
            Format = movies.MovieFormat,
            Director = movies.MovieDirector,
            Actor = movies.MovieActor,
            Genre = movies.MovieGenre,
            ReleaseDate = movies.ReleaseDate.HasValue ? movies.ReleaseDate.Value.ToString("dd/MM/yyyy") : null,
            Duration = movies.Duration,
            Language = movies.Language,
            MovieImage = movies.MovieImage
        };
        return View();
       }
    }
    public IActionResult Privacy()
    {
        return View();
    }
    //Thoát
    public async Task<IActionResult> LogOut()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); //Xóa cookie 
        return RedirectToAction("HomeClient", "Client");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
