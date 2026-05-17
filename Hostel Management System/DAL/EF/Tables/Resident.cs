using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Resident
{
    public int ResidentId { get; set; }

    public int? UserId { get; set; }

    public string? GuardianName { get; set; }

    public string? GuardianPhone { get; set; }

    public string? Address { get; set; }

    public DateOnly CheckInDate { get; set; }

    public DateOnly? CheckOutDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual ICollection<MealBooking> MealBookings { get; set; } = new List<MealBooking>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<RoomAllocation> RoomAllocations { get; set; } = new List<RoomAllocation>();

    public virtual User? User { get; set; }
}
