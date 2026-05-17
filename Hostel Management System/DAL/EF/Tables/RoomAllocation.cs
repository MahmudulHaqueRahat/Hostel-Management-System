using System;
using System.Collections.Generic;

namespace DAL.EF.Tables;

public partial class RoomAllocation
{
    public int AllocationId { get; set; }

    public int ResidentId { get; set; }

    public int RoomId { get; set; }

    public DateOnly AllocationDate { get; set; }

    public bool? IsActive { get; set; }

    public string Status { get; set; } = null!;

    public virtual Resident Resident { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
