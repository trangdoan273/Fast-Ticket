using Microsoft.AspNetCore.Mvc;
using TICKETBOX.Models.Tables;
using TICKETBOX.Models;
using Microsoft.AspNetCore.Authorization;

namespace TICKETBOX.Controllers
{
    public class StoreController : Controller
    {
        private readonly ILogger<RapController> _logger;

        public StoreController(ILogger<RapController> logger)
        {
            _logger = logger;
        }
        public IActionResult Store(int id)
        {
            using (var db = new FastticketContext())
            {
                var concessions = db.Concessions.ToList();
                return View(concessions); 
            }
        }
    }
}
