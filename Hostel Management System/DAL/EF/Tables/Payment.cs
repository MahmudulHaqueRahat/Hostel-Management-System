using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int ResidentId { get; set; }

    public int BillId { get; set; }

    public decimal Amount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string? TransactionId { get; set; }

    public string? PaymentStatus { get; set; }

    public virtual Bill Bill { get; set; } = null!;

    public virtual Resident Resident { get; set; } = null!;
}
