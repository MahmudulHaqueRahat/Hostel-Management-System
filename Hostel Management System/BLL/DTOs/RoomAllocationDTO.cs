using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DTOs
{
    public class RoomAllocationDTO
    {
        public int AllocationId { get; set; }

        public int ResidentId { get; set; }

        public string? RoomNumber { get; set; }

        public int RoomId { get; set; }

        public DateOnly? AllocationDate { get; set; }

        public bool? IsActive { get; set; }

        public string? Status { get; set; }

        public string? ResidentName { get; set; }

        public int FloorNumber { get; set; }

        public decimal MonthlyRent { get; set; }
    }
}
