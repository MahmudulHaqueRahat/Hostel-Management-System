using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }


        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [RegularExpression(@"^[a-zA-Z\s.-]+$", ErrorMessage = "Name cannot contain numbers or special characters")]
        public string FullName { get; set; } = null!;

        [StringLength(50)]
        public string? StudentId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^(01)[0-9]{9}$", ErrorMessage = "Phone number must contain only numbers")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Phone number must be between 10 and 15 digits")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string PasswordHash { get; set; } = null!;

        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }


        public string Role { get; set; } = "2";

        public DateTime? CreatedAt { get; set; }

        public bool? IsActive { get; set; } = true;

    }
}
