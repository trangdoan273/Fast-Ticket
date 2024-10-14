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
        //Chọn ghế
        [Authorize(Roles = "User")]
        public IActionResult SelectSeat(int id)
        {
            using (var db = new FastticketContext())
            {
                var movie = db.Movies.FirstOrDefault(m => m.MovieId == id);
                var showdates = db.Showdates.Where(m => m.MovieId == id).ToList();
                var showtimes = db.Showtimes.Where(m => m.MovieId == id).ToList();
                var seat = db.Seats
                .Where(m => m.MovieId == id)
                .Select(s => new
                {
                    SeatName = s.SeatNumb,
                    Price = s.SeatNumb,
                    IsBooked = db.Tickets.Any(t => t.SeatId == s.SeatId && t.MovieId == id && t.TicketStatus == "Booked")
                })
                .ToList();

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
                        SeatName = s.SeatName,
                        Price = decimal.TryParse(s.Price, out decimal parsedPrice) ? (decimal?)parsedPrice : null,
                        IsBooked = s.IsBooked
                    }).ToList()
                };
                return View();
            }
        }

        //Lưu những thông tin đã chọn vào vé
        [HttpPost]
        [Authorize(Roles = "User")]
        public IActionResult ProcessTickets(string selectedSeats, int movieId, int showtimeId, int showdateId)
        {
            using (var db = new FastticketContext())
            {
                var seatNumbers = JsonConvert.DeserializeObject<List<string>>(selectedSeats);
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var ticketIds = new List<int>();

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

                        ticketIds.Add(ticket.TicketId);
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Ticket", new { ids = JsonConvert.SerializeObject(ticketIds) });
            }
        }
        //Vé
        [Authorize(Roles = "User")]
        public IActionResult Ticket(string ids)
        {
            using (var db = new FastticketContext())
            {
                var ticketIds = JsonConvert.DeserializeObject<List<int>>(ids);
                var ticketsInfo = new List<TicketInfoModel>();

                foreach (var ticketId in ticketIds)
                {
                    var ticket = db.Tickets.FirstOrDefault(t => t.TicketId == ticketId);
                    if (ticket != null)
                    {
                        var showtimes = db.Showtimes.FirstOrDefault(t => t.ShowtimeId == ticket.ShowtimeId);
                        var showdates = db.Showdates.FirstOrDefault(t => t.ShowdateId == ticket.ShowdateId);
                        var movie = db.Movies.FirstOrDefault(t => t.MovieId == ticket.MovieId);
                        var seat = db.Seats.FirstOrDefault(t => t.SeatId == ticket.SeatId);

                        var ticketInfo = new TicketInfoModel()
                        {
                            Id = ticket.TicketId,
                            MovieName = movie.MovieName,
                            ShowDates = showdates.ShowDate1.HasValue ? showdates.ShowDate1.Value.ToString("dd/MM/yyyy") : null,
                            Time = $"{showtimes.StartTime} ~ {showtimes.EndTime}",
                            SeatName = seat.SeatNumb,
                            Price = seat.Price.ToString(),
                            MovieImage = movie.MovieImage
                        };

                        ticketsInfo.Add(ticketInfo);
                    }
                }
                ViewBag.TicketsInfo = ticketsInfo;
                ViewBag.SuccessMessage = "Bạn đã mua vé thành công!";
                return View();
            }
        }
    }
}