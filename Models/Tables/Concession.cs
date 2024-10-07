using System;
using System.Collections.Generic;

namespace TICKETBOX.Models.Tables;

public partial class Concession
{
    public int ConcessionId { get; set; }

    public string? ConcessionName { get; set; }

    public string? ConcessionDescription { get; set; }

    public int? ConcessionPrice { get; set; }

    public string? ConcessionImage { get; set; }
}
