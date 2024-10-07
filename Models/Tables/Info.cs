using System;
using System.Collections.Generic;

namespace TICKETBOX.Models.Tables;

public partial class Info
{
    public int InfoId { get; set; }

    public string? InfoTitle { get; set; }

    public string? InfoContent { get; set; }

    public string? InfoImage { get; set; }
}
