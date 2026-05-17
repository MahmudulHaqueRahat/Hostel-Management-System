using AutoMapper;
using BLL.DTOs;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class RoomService
    {
        RoomRepo repo;
        Mapper mapper;
        

        public RoomService(RoomRepo repo)
        {
            this.repo = repo;
            this.mapper = MapperConfig.GetMapper();
        }
        public List<RoomDTO> Get()
        {
            var data = repo.Get();
            var res = mapper.Map<List<RoomDTO>>(data);
            return res;

        }
        public RoomDTO GetById(int id)
        {
            var data = repo.Get(id);
            return mapper.Map<RoomDTO>(data);
        }
        public List<RoomDTO> SearchRooms(string search)
        {
            var data = repo.SearchRooms(search);

            return mapper.Map<List<RoomDTO>>(data);
        }
        public bool Delete(int id)
        {
            return repo.Delete(id);
        }
        public bool Update(RoomDTO c)
        {
            var converted = mapper.Map<DAL.EF.Tables.Room>(c);
            return repo.Update(converted);
        }

    }
}
