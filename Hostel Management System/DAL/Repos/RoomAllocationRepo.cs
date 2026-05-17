using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class RoomAllocationRepo
    {
        HostelContext db;
        public RoomAllocationRepo(HostelContext db)
        {
            this.db = db;
        }
        public bool Create(RoomAllocation a)
        {
            db.RoomAllocations.Add(a);

            return db.SaveChanges() > 0;
        }
        public List<int> GetPendingRoomIds(int residentId)
        {
            return db.RoomAllocations
                .Where(r =>
                    r.ResidentId == residentId &&
                    r.Status == "Pending")
                .Select(r => r.RoomId)
                .ToList();
        }
        public List<RoomAllocation> GetPendingRooms(int residentId)
        {
            return db.RoomAllocations
            .Where(r =>
                        r.ResidentId == residentId &&
                        r.Status == "Pending")
                .Select(r => new RoomAllocation
                {
                  AllocationId = r.AllocationId,
                 RoomId = r.RoomId,
                 AllocationDate = r.AllocationDate,
                 Status = r.Status,
                 Room = r.Room
                 }).ToList();
        }
        public List<RoomAllocation> GetAllPendingRequests()
        {
            return db.RoomAllocations
                .Include(r => r.Room)
                .Include(r => r.Resident)
                .ThenInclude(r => r.User)
                .Where(r => r.Status == "Pending")
                .ToList();
        }
        //public bool ApproveRoom(int allocationId)
        //{
        //    // 1. Find the specific booking
        //    var approvedBooking = db.RoomAllocations.Find(allocationId);
        //    if (approvedBooking == null) return false;

        //    // 2. Approve it
        //    approvedBooking.Status = "Approved";
        //    approvedBooking.IsActive = true;

        //    // 3. Find and remove any OTHER pending requests this resident made
        //    var otherPendingBookings = db.RoomAllocations
        //        .Where(a => a.ResidentId == approvedBooking.ResidentId
        //                 && a.AllocationId != allocationId
        //                 && a.Status == "Pending")
        //        .ToList();

        //    if (otherPendingBookings.Any())
        //    {
        //        db.RoomAllocations.RemoveRange(otherPendingBookings);
        //    }

        //    // 4. Update the Room Occupancy
        //    var room = db.Rooms.Find(approvedBooking.RoomId);
        //    if (room != null)
        //    {
        //        room.OccupiedBeds = (room.OccupiedBeds ?? 0) + 1;

        //        // If the room just hit max capacity, mark it full
        //        if (room.OccupiedBeds >= room.Capacity)
        //        {
        //            room.Status = "Full";
        //        }
        //    }

        //    // 5. Update the Resident Profile
        //    var resident = db.Residents.Find(approvedBooking.ResidentId);
        //    if (resident != null)
        //    {
        //        resident.Status = "Active";
        //        resident.CheckInDate = DateOnly.FromDateTime(DateTime.Now);
        //    }

        //    // 6. Save everything at once
        //    return db.SaveChanges() > 0;
        //}
        public bool ApproveRoom(int allocationId)
        {
            var approvedBooking =
                db.RoomAllocations.Find(allocationId);

            if (approvedBooking == null)
            {
                return false;
            }

            // prevent double approval
            if (approvedBooking.Status == "Approved")
            {
                return false;
            }

            var room = db.Rooms.Find(approvedBooking.RoomId);

            if (room == null)
            {
                return false;
            }

            // prevent overbooking
            if ((room.OccupiedBeds ?? 0) >= room.Capacity)
            {
                room.Status = "Full";
                db.SaveChanges();

                return false;
            }

            // approve booking
            approvedBooking.Status = "Approved";
            approvedBooking.IsActive = true;

            // reject other pending bookings
            var otherPendingBookings = db.RoomAllocations
                .Where(a =>
                    a.ResidentId == approvedBooking.ResidentId &&
                    a.AllocationId != allocationId &&
                    a.Status == "Pending")
                .ToList();

            foreach (var booking in otherPendingBookings)
            {
                booking.Status = "Rejected";
                booking.IsActive = false;
            }

            // update occupancy
            room.OccupiedBeds = (room.OccupiedBeds ?? 0) + 1;

            // auto status update
            if (room.OccupiedBeds >= room.Capacity)
            {
                room.Status = "Full";
            }
            else
            {
                room.Status = "Available";
            }

            // update resident
            var resident = db.Residents.Find(approvedBooking.ResidentId);

            if (resident != null)
            {
                resident.Status = "Active";

                if (resident.CheckInDate == null)
                {
                    resident.CheckInDate =
                        DateOnly.FromDateTime(DateTime.Now);
                }
            }

            return db.SaveChanges() > 0;
        }

        public bool RejectRoom(int allocationId)
        {
            var rejectedBooking = db.RoomAllocations.Find(allocationId);

            if (rejectedBooking == null) return false;

            rejectedBooking.Status = "Rejected";
            rejectedBooking.IsActive = false;

            return db.SaveChanges() > 0;
        }



        //public bool DeletePendingRequest(int residentId, int roomId)
        //{
        //    var data = db.RoomAllocations.FirstOrDefault(r =>
        //        r.ResidentId == residentId &&
        //        r.RoomId == roomId &&
        //        r.Status == "Pending");

        //    if (data == null)
        //    {
        //        return false;
        //    }

        //    db.RoomAllocations.Remove(data);

        //    return db.SaveChanges() > 0;
        //}

        public bool DeletePendingRequest(int residentId, int roomId)
        {
            var data = db.RoomAllocations.FirstOrDefault(r =>
                r.ResidentId == residentId &&
                r.RoomId == roomId &&
                r.Status == "Pending");

            if (data == null)
            {
                return false;
            }

            data.Status = "Cancelled";
            data.IsActive = false;

            return db.SaveChanges() > 0;
        }
        public bool HasPendingRequest(int residentId, int roomId)
        {
            return db.RoomAllocations.Any(r =>
                r.ResidentId == residentId &&
                r.RoomId == roomId &&
                r.Status == "Pending");
        }
        public bool HasApprovedRoom(int residentId)
        {
            return db.RoomAllocations.Any(r =>
                r.ResidentId == residentId &&
                r.Status == "Approved");
        }
        public bool IsRoomAvailable(int roomId)
        {
            var room = db.Rooms.Find(roomId);

            if (room == null)
            {
                return false;
            }

            // inactive room
            if (room.Status == "Inactive")
            {
                return false;
            }

            // room full
            if ((room.OccupiedBeds ?? 0) >= room.Capacity)
            {
                room.Status = "Full";
                db.SaveChanges();

                return false;
            }

            // auto-fix status if beds available
            room.Status = "Available";
            db.SaveChanges();

            return true;
        }

        public RoomAllocation GetApprovedRoom(int residentId)
        {
            return db.RoomAllocations.Include(r => r.Room).FirstOrDefault(r =>
                    r.ResidentId == residentId &&
                    r.Status == "Approved");
        }
    }
}
