using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace TICKETBOX.Controllers{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller{
        private readonly ILogger<AdminController> _logger;
        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }
        public IActionResult Management(){
            return View();
        }
    }
}