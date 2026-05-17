using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class ResidentDTO
    {
        public int ResidentId { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string GuardianName { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$",
            ErrorMessage = "Phone number must contain only numbers.")]
        public string GuardianPhone { get; set; }

        [Required]
        public string Address { get; set; }

        public DateOnly? CheckInDate { get; set; }

        public DateOnly? CheckOutDate { get; set; }

        public string? Status { get; set; }
    }
}
