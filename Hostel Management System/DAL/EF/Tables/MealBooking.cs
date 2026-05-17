using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class MealBooking
{
    public int BookingId { get; set; }

    public int ResidentId { get; set; }

    public int MealId { get; set; }

    public DateOnly BookingDate { get; set; }

    public int? Quantity { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Meal Meal { get; set; } = null!;

    public virtual Resident Resident { get; set; } = null!;
}
