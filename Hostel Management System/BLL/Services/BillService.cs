using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repos;
namespace BLL.Services
{
    public class BillService
    {
        BillRepo repo;
        Mapper mapper;
        public BillService(BillRepo repo)
        {
            this.repo = repo;
            this.mapper = MapperConfig.GetMapper();
        }
        public List<BillDTO> GetAll()
        {
            var data = repo.GetAll();
            return mapper.Map<List<BillDTO>>(data);
        }
        public List<BillDTO> GetByResident(int residentId)
        {
            var data = repo.GetByResident(residentId);
            return mapper.Map<List<BillDTO>>(data);
        }
        // ADMIN GENERATE MONTHLY BILL
        public void GenerateMonthlyBills(string month)
        {
            var residents = repo.GetActiveResidents();
            foreach (var resident in residents)
            {
                bool exists = repo.BillExists(resident.ResidentId, month);
                if (exists)
                {
                    continue;
                }
                var approvedRoom = resident.RoomAllocations
                .FirstOrDefault(a => a.Status == "Approved");
                if (approvedRoom == null)
                {
                    continue;
                }
                decimal roomRent = approvedRoom.Room.MonthlyRent;
                // SIMPLE STATIC VALUES
                decimal utilityCharge = 500;
                decimal mealCharge = 0;
                // PREVIOUS DUE
                decimal previousDue = repo
                .GetByResident(resident.ResidentId)
                .OrderByDescending(b => b.BillId)
                .FirstOrDefault()?.DueAmount ?? 0;
                decimal total =
                roomRent +
                utilityCharge +
                mealCharge +
                previousDue;
                Bill bill = new Bill()
                {
                    ResidentId = resident.ResidentId,
                    BillingMonth = month,
                    RoomRent = roomRent,
                    MealCharge = mealCharge,
                    UtilityCharge = utilityCharge,
                    PreviousDue = previousDue,
                    TotalAmount = total,
                    DueAmount = total,
                    GeneratedDate = DateTime.Now,
                    Status = "Unpaid"
                };
                repo.Create(bill);
            }
        }
        // SIMPLE PAYMENT UPDATE
        public bool MarkAsPaid(int billId)
        {
            var bill = repo.Get(billId);
            if (bill == null)
            {
                return false;
            }
            bill.DueAmount = 0;
            bill.Status = "Paid";
            return repo.Update(bill);
        }
    }
}
