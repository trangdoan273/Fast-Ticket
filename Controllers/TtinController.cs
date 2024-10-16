using Microsoft.AspNetCore.Mvc;
using TICKETBOX.Models.Tables;
using TICKETBOX.Models;

namespace TICKETBOX.Controllers
{
    public class TtinController : Controller
    {
        public IActionResult Ttin()
        {
            using (var db = new FastticketContext())
            {
                var infoList = db.Infos.ToList();
                return View(infoList);
            }
        }

        // Action cho trang chi tiết Ttin1
        public IActionResult Ttin1(int id)
        {
            using (var db = new FastticketContext())
            {
                var info = db.Infos.FirstOrDefault(i => i.InfoId == id);
                return View(info); // Truyền dữ liệu sang view Ttin1.cshtml
            }
        }
    }
}
