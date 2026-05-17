using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class Room
{
    public int RoomId { get; set; }

    public string RoomNumber { get; set; } = null!;

    public int FloorNumber { get; set; }

    public int Capacity { get; set; }

    public int? OccupiedBeds { get; set; }

    public decimal MonthlyRent { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<RoomAllocation> RoomAllocations { get; set; } = new List<RoomAllocation>();
}
