using DAL.EF;
using DAL.EF.Tables;

namespace DAL.Repos
{
    public class ExpenseRepo
    {
        HostelContext db;

       
        public ExpenseRepo(HostelContext db)
        {
            this.db = db;
        }

        public List<Expense> GetAll()
        {
            return db.Expenses
                .OrderByDescending(e => e.ExpenseDate)
                .ToList();
        }

        public Expense Get(int id)
        {
            return db.Expenses.Find(id);
        }

        public bool Create(Expense expense)
        {
            db.Expenses.Add(expense);

             db.SaveChanges();
            return true;
        }

        public bool Update(Expense expense)
        {
            var exobj = Get(expense.ExpenseId);

            db.Entry(exobj)
                .CurrentValues
                .SetValues(expense);

            db.SaveChanges();
            return true;

        }

        public bool Delete(int id)
        {
            var data = Get(id);

            db.Expenses.Remove(data);

            return db.SaveChanges() > 0;
        }

        // ANALYTICS
        public decimal GetTotalExpense()
        {
            return db.Expenses
                .Sum(e => (decimal?)e.Amount) ?? 0;
        }
    }
}