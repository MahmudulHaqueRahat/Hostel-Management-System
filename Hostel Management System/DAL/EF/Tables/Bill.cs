using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Bill
{
    public int BillId { get; set; }

    public int ResidentId { get; set; }

    public string BillingMonth { get; set; } = null!;

    public decimal RoomRent { get; set; }

    public decimal? MealCharge { get; set; }

    public decimal? UtilityCharge { get; set; }

    public decimal? PreviousDue { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal DueAmount { get; set; }

    public DateTime? GeneratedDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Resident Resident { get; set; } = null!;
}
