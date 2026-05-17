using AutoMapper;
using BLL.DTOs;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class AdminService
    {
        UserRepo repo;
        Mapper mapper;

        public AdminService(UserRepo repo)
        {
            this.repo = repo;
            this.mapper = MapperConfig.GetMapper();
        }
        public List<UserDTO> Get()
        {
            var data = repo.Get();
            var res = mapper.Map<List<UserDTO>>(data);
            return res;

        }
        public UserDTO GetById(int id)
        {
            var data = repo.GetById(id);

            return mapper.Map<UserDTO>(data);
        }

        public List<UserDTO> SearchUsers(string search)
        {
            var data = repo.SearchUsers(search);

            return mapper.Map<List<UserDTO>>(data);
        }

        public bool Update(UserDTO dto)
        {
            var data = mapper.Map<DAL.EF.Tables.User>(dto);

            return repo.Update(data);
        }
        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
    }
}
