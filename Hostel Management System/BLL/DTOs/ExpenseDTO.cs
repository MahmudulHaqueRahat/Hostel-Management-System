using System;

namespace BLL.DTOs
{
    public class ExpenseDTO
    {
        public int ExpenseId { get; set; }

        public string ExpenseTitle { get; set; }

        public string ExpenseCategory { get; set; }

        public decimal Amount { get; set; }

        public DateTime? ExpenseDate { get; set; }

        public string Description { get; set; }
    }
}