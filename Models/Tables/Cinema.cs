using System;
using System.Collections.Generic;

namespace TICKETBOX.Models.Tables;

public partial class Cinema
{
    public int CinemaId { get; set; }

    public string? CinemaName { get; set; }

    public string? CinemaAddress { get; set; }

    public string? CinemaPhoneNumber { get; set; }

    public string? CinemaEmail { get; set; }

    public int? SeatCount { get; set; }

    public int? RoomCount { get; set; }

    public string? ScreeningType { get; set; }

    public string? CinemaImage { get; set; }
}
