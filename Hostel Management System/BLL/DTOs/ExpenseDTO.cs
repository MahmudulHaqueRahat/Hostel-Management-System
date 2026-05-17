using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class ExpenseDTO
    {
        public int ExpenseId { get; set; }

        [Required]
        public string ExpenseTitle { get; set; }

        [Required]
        public string ExpenseCategory { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public DateOnly? ExpenseDate { get; set; }

        public string? Description { get; set; }
    }
}