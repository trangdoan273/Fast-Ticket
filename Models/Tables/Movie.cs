using System;
using System.Collections.Generic;

namespace TICKETBOX.Models.Tables;

public partial class Movie
{
    public int MovieId { get; set; }

    public string? MovieName { get; set; }

    public string? MovieContent { get; set; }

    public string? MovieGenre { get; set; }

    public string? MovieLabel { get; set; }

    public string? MovieFormat { get; set; }

    public string? MovieDirector { get; set; }

    public string? MovieActor { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public string? Duration { get; set; }

    public string? Language { get; set; }

    public string? MovieImage { get; set; }

    public string? MoviePoster { get; set; }

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    public virtual ICollection<Showdate> Showdates { get; set; } = new List<Showdate>();

    public virtual ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
