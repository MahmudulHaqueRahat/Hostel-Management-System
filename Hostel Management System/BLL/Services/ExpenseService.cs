using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repos;

namespace BLL.Services
{
    public class ExpenseService
    {
        ExpenseRepo repo;
        Mapper mapper;

        public ExpenseService(ExpenseRepo repo)
        {
            this.repo = repo;
            this.mapper = MapperConfig.GetMapper();
        }

        public List<ExpenseDTO> Get()
        {
            var data = repo.GetAll();

            return mapper.Map<List<ExpenseDTO>>(data);
        }

        public ExpenseDTO Get(int id)
        {
            var data = repo.Get(id);

            return mapper.Map<ExpenseDTO>(data);
        }

        public bool Create(ExpenseDTO dto)
        {
            var data = mapper.Map<Expense>(dto);

            return repo.Create(data);
        }

        public bool Update(ExpenseDTO dto)
        {
            var data = mapper.Map<Expense>(dto);

            return repo.Update(data);
        }

        public bool Delete(int id)
        {
            return repo.Delete(id);
        }

        public decimal GetTotalExpense()
        {
            return repo.GetTotalExpense();
        }
    }
}