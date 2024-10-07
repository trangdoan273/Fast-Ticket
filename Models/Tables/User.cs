using System;
using System.Collections.Generic;

namespace TICKETBOX.Models.Tables;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? UserPassword { get; set; }

    public string? UserEmail { get; set; }

    public string? FullName { get; set; }

    public string? UserPhoneNumber { get; set; }

    public DateTime? DoB { get; set; }

    public string? Sex { get; set; }

    public string? UserAddress { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
