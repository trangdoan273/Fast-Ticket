using System;
using System.Collections.Generic;

namespace TICKETBOX.Models.Tables;

public partial class Showdate
{
    public int ShowdateId { get; set; }

    public int? MovieId { get; set; }

    public DateTime? ShowDate1 { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
