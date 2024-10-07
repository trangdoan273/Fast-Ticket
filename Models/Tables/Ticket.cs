using System;
using System.Collections.Generic;

namespace TICKETBOX.Models.Tables;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int? UserId { get; set; }

    public int? MovieId { get; set; }

    public int? SeatId { get; set; }

    public int? ShowtimeId { get; set; }

    public int? ShowdateId { get; set; }

    public string? TicketStatus { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual Seat? Seat { get; set; }

    public virtual Showdate? Showdate { get; set; }

    public virtual Showtime? Showtime { get; set; }

    public virtual User? User { get; set; }
}
