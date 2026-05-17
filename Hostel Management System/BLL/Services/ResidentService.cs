using AutoMapper;
using BLL.DTOs;
using DAL.EF.Tables;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class ResidentService
    {
        ResidentRepo repo;
        Mapper mapper;
        public ResidentService(ResidentRepo repo)
        {
            this.repo = repo;
            this.mapper = MapperConfig.GetMapper();
        }

        public bool Create(ResidentDTO dto)
        {
            dto.Status = "Pending";

            var data = mapper.Map<Resident>(dto);

            return repo.Create(data);
        }

        public ResidentDTO GetByUserId(int userId)
        {
            var data = repo.GetByUserId(userId);

            return mapper.Map<ResidentDTO>(data);
        }
    }
}
