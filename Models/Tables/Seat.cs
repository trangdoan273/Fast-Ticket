using System;
using System.Collections.Generic;

namespace TICKETBOX.Models.Tables;

public partial class Seat
{
    public int SeatId { get; set; }

    public int? MovieId { get; set; }

    public int? Price { get; set; }

    public string? SeatNumb { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
