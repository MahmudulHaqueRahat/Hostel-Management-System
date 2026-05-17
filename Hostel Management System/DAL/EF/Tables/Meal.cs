using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Meal
{
    public int MealId { get; set; }

    public string MealName { get; set; } = null!;

    public string MealType { get; set; } = null!;

    public decimal Price { get; set; }

    public bool? IsAvailable { get; set; }

    public virtual ICollection<MealBooking> MealBookings { get; set; } = new List<MealBooking>();
}
