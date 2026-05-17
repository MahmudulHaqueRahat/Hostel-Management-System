using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Expense
{
    public int ExpenseId { get; set; }

    public string ExpenseTitle { get; set; } = null!;

    public string ExpenseCategory { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateOnly ExpenseDate { get; set; }

    public string? Description { get; set; }
}
