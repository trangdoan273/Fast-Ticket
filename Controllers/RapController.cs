using Microsoft.AspNetCore.Mvc;
using TICKETBOX.Models.Tables;
using TICKETBOX.Models;
using Microsoft.AspNetCore.Authorization;

namespace TICKETBOX.Controllers
{
    public class RapController : Controller
    {
        private readonly ILogger<RapController> _logger;

        public RapController(ILogger<RapController> logger)
        {
            _logger = logger;
        }
        public IActionResult Rap(int id)
        {
            using (var db = new FastticketContext())
            {
                var cinema = db.Cinemas.FirstOrDefault(c => c.CinemaId == id);
                return View(cinema); 
            }
        }
    }
}
