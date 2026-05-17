using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.DTOs
{
    public class RoomDTO
    {
        [Required(ErrorMessage = "Room ID is required")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "Room Number is required")]
        [StringLength(10, ErrorMessage = "Room Number cannot exceed 10 characters")]
        public string RoomNumber { get; set; } = null!;

        [Required(ErrorMessage = "Floor Number is required")]
        public int FloorNumber { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, 10, ErrorMessage = "Capacity must be between 1 and 10")]
        public int Capacity { get; set; }

        public int? OccupiedBeds { get; set; }

        [Required(ErrorMessage = "Monthly Rent is required")]
        [Range(0, 100000, ErrorMessage = "Please enter a valid rent amount")]
        public decimal MonthlyRent { get; set; }

        public string? Status { get; set; }
    }
}
