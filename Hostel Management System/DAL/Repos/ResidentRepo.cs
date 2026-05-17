using DAL.EF;
using DAL.EF.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class ResidentRepo
    {
        HostelContext db;
        public ResidentRepo(HostelContext db)
        {
            this.db = db;
        }
        public bool Create(Resident resident)
        {
            db.Residents.Add(resident);

            return db.SaveChanges() > 0;
        }

        public Resident GetByUserId(int userId)
        {
            var res=db.Residents.FirstOrDefault(r => r.UserId == userId);
            return res;
        }
    }
}
