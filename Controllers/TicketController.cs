using Microsoft.AspNetCore.Mvc;
using TICKETBOX.Models.Tables;
using TICKETBOX.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Newtonsoft.Json;



namespace PROJECT.Models
{
    public class TicketController : Controller
    {
        private readonly ILogger<TicketController> _logger;
        public TicketController(ILogger<TicketController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "User")]
        public IActionResult SelectSeat(int id)
        {
            using (var db = new FastticketContext())
            {
                var movie = db.Movies.FirstOrDefault(m => m.MovieId == id);
                var showdates = db.Showdates.Where(m => m.MovieId == id).ToList();
                
                var showtimes = db.Showtimes.Where(m => m.MovieId == id).ToList();
                var seat = db.Seats.Where(m => m.MovieId == id).ToList();

                ViewBag.SelectSeatInfo = new SelectSeatModel()
                {
                    Id = movie.MovieId,
                    ShowtimeId = showtimes.Select(st => st.ShowtimeId).FirstOrDefault(),
                    ShowdateId = showdates.Select(sd => sd.ShowdateId).FirstOrDefault(),
                    MovieName = movie.MovieName,
                    Content = movie.MovieContent,
                    Director = movie.MovieDirector,
                    Actor = movie.MovieActor,
                    Genre = movie.MovieGenre,
                    ReleaseDate = movie.ReleaseDate.HasValue ? movie.ReleaseDate.Value.ToString("dd/MM/yyyy") : null,
                    Duration = movie.Duration,
                    MovieImage = movie.MovieImage,
                    ShowDates = showdates.Select(sd => sd.ShowDate1.HasValue ? sd.ShowDate1.Value.ToString("dd/MM/yyyy") : null).ToList(),
                    ShowTimes = showtimes.Select(st => st.StartTime.ToString()).ToList(),
                    Seat = seat.Select(s => new SeatInfo
                    {
                        SeatNumb = s.SeatNumb,
                        Price = s.Price
                    }).ToList()
                };
                return View();
            }
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult ProcessTickets(string selectedSeats, int movieId, int showtimeId, int showdateId)
        {
            using (var db = new FastticketContext())
            {
                var seatNumbers = JsonConvert.DeserializeObject<List<string>>(selectedSeats);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


                foreach (var seatNumb in seatNumbers)
                {
                    var seat = db.Seats.FirstOrDefault(s => s.SeatNumb == seatNumb && s.MovieId == movieId);
                    if (seat != null)
                    {
                        var ticket = new Ticket
                        {
                            UserId = int.Parse(userId),
                            MovieId = movieId,
                            SeatId = seat.SeatId,
                            ShowtimeId = showtimeId,
                            ShowdateId = showdateId,
                            TicketStatus = "Booked"
                        };
                        db.Tickets.Add(ticket);
                        db.SaveChanges();
                    }
                }

                db.SaveChanges(); 
                return RedirectToAction("Ticket", new { id = db.Tickets.OrderByDescending(t => t.TicketId).First().TicketId });
            }
        }


        [Authorize(Roles = "User")]
        public IActionResult Ticket(int id)
        {
            using (var db = new FastticketContext())
            {

                var ticket = db.Tickets.FirstOrDefault(t => t.TicketId == id);
                var showtimes = db.Showtimes.FirstOrDefault(t => t.ShowtimeId == ticket.ShowtimeId);
                var showdates = db.Showdates.FirstOrDefault(t => t.ShowdateId == ticket.ShowdateId);
                var movie = db.Movies.FirstOrDefault(t => t.MovieId == ticket.MovieId);
                var seat = db.Seats.FirstOrDefault(t => t.SeatId == ticket.SeatId);

                ViewBag.TicketInfo = new TicketInfoModel()
                {
                    Id = ticket.TicketId,
                    MovieName = movie.MovieName,
                    ShowDates = showdates.ShowDate1.HasValue ? showdates.ShowDate1.Value.ToString("dd/MM/yyyy") : null,
                    Time = $"{showtimes.StartTime} ~ {showtimes.EndTime}",
                    SeatName = seat.SeatNumb,
                    Price = seat.Price.ToString(),
                    MovieImage = movie.MovieImage
                };
                return View();
            }
        }
    }
}