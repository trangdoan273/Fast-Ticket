using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TICKETBOX.Models.Tables;
using TICKETBOX.Models;
using Org.BouncyCastle.Crypto.Agreement.Srp;
using ZstdSharp.Unsafe;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;



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
        public IActionResult SelectShowtimes(int id)
        {
            using (var db = new FastticketContext())
            {
                var movie = db.Movies.FirstOrDefault(m => m.MovieId == id);
                var showdates = db.Showdates.Where(m => m.MovieId == id).ToList();
                var showtimes = db.Showtimes.Where(m => m.MovieId == id).ToList();
                var seat = db.Seats.Where(m => m.MovieId == id).ToList();

                ViewBag.SelectShowtimesInfo = new SelectShowtimesModel()
                {
                    Id = movie.MovieId,
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
                    Seat = seat.Select(s => s.SeatNumb).ToList()
                };
                return View();
            }
            // using (var db = new FastticketContext())
            // {
            //     var movie = db.Movies.FirstOrDefault(m => m.MovieId == id);
            //     var showdates = db.Showdates.Where(m => m.MovieId == id).ToList();
            //     var showtimes = db.Showtimes.Where(m => m.MovieId == id).ToList();
            //     var seat = db.Seats.Where(s => s.SeatId == id).ToString();

            //     ViewBag.SelectShowtimesInfo = new SelectShowtimesModel()
            //     {
            //         Id = movie.MovieId,
            //         MovieName = movie.MovieName,
            //         Content = movie.MovieContent,
            //         Director = movie.MovieDirector,
            //         Actor = movie.MovieActor,
            //         Genre = movie.MovieGenre,
            //         ReleaseDate = movie.MovieReleaseDate.ToString(),
            //         Duration = movie.MovieDuration,
            //         MovieImage = movie.MovieImage,
            //         ShowDates = showdates.Select(sd => sd.ShowDate1.HasValue ? sd.ShowDate1.Value.ToString("dd/MM/yyyy") : null).ToList(),
            //         ShowTimes = showtimes.Select(st => st.ShowtimeStartTime.ToString()).ToList(),

            //     };
            
            // }
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