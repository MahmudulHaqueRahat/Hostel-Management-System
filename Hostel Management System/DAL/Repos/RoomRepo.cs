using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class RoomRepo
    {
        HostelContext db;
        public RoomRepo(HostelContext db)
        {
            this.db = db;
        }

        public List<Room> Get()
        {
            return db.Rooms.ToList();
        }
        public bool Create(Room room)
        {
            db.Rooms.Add(room);
             db.SaveChanges();
             return true;
        }
        public Room Get(int id)
        {
            return db.Rooms.Find(id);
        }
        public bool Delete(int id)
        {
            var room = db.Rooms.Find(id);

            if (room == null)
            {
                return false;
            }

            db.Rooms.Remove(room);

            return db.SaveChanges() > 0;
        }
        public bool Update(Room c)
        {
            var exobj = Get(c.RoomId);
            db.Entry(exobj).CurrentValues.SetValues(c);
            return db.SaveChanges() > 0;
        }
        public List<Room> SearchRooms(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return db.Rooms.ToList();
            }

            search = search.ToLower();

            return db.Rooms
                .Where(r =>
                    r.RoomNumber.ToLower().Contains(search) ||
                    r.MonthlyRent.ToString().Contains(search) ||
                    r.Capacity.ToString().Contains(search))
                .ToList();
        }

    }
    }
