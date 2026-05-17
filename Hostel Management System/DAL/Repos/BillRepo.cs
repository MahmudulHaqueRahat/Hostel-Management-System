using DAL.EF;
using DAL.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repos
{
    public class BillRepo
    {
        HostelContext db;
        public BillRepo(HostelContext db)
        {
            this.db = db;
        }

        public List<Bill> GetAll()
        {
            return db.Bills
                .Include(b => b.Resident)
                .ThenInclude(r => r.User)
                .ToList();
        }

        //public List<Bill> GetByResident(int residentId)
        //{
        //    return db.Bills
        //        .Where(b => b.ResidentId == residentId)
        //        .OrderByDescending(b => b.BillingMonth)
        //        .ToList();
        //}
        public List<Bill> GetByResident(int residentId)
        {
            return db.Bills
                .Include(b => b.Resident)
                .ThenInclude(r => r.User)
                .Where(b => b.ResidentId == residentId)
                .OrderByDescending(b => b.BillId)
                .ToList();
        }

        public bool Create(Bill bill)
        {
            db.Bills.Add(bill);
            return db.SaveChanges() > 0;
        }

        public Bill Get(int id)
        {
            return db.Bills.Find(id);
        }

        public bool Update(Bill bill)
        {
            var exobj = Get(bill.BillId);
            db.Entry(exobj).CurrentValues.SetValues(bill);
            return db.SaveChanges() > 0;
        }

        
        public bool BillExists(int residentId, string month)
        {
            return db.Bills.Any(b =>
                b.ResidentId == residentId &&
                b.BillingMonth == month);
        }
        // ACTIVE RESIDENTS WITH APPROVED ROOM
        public List<Resident> GetActiveResidents()
        {
            return db.Residents
                .Include(r => r.RoomAllocations)
                .ThenInclude(a => a.Room)
                .Where(r => r.Status == "Active")
                .ToList();
        }
    }
}
