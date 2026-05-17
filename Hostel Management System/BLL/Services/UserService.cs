using AutoMapper;
using BLL.DTOs;
using BLL.Helpers;
using DAL.EF.Tables;
using DAL.Repos;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    public class UserService
    {
        UserRepo repo;
        Mapper mapper;
        public UserService(UserRepo repo)
        {
            this.repo = repo;
            mapper= MapperConfig.GetMapper();
        }

        public bool Create(UserDTO U)
        {
           
            var converted = mapper.Map<User>(U);
            converted.PasswordHash = Md5Helper.GetMd5(U.PasswordHash);
            return repo.Create(converted);
        }
        public bool IsNameExist(string name)
        {
            return repo.IsNameExist(name);
        }
        public bool IsEmailExist(string email)
        {
            return repo.IsEmailExist(email);
        }

        //for login
        public User Login(LoginDTO dto)
        {
            var user = repo.GetByEmail(dto.Email);

            if (user == null)
            {
                return null;
            }

            string hashedPassword =
                Md5Helper.GetMd5(dto.Password);

            if (user.PasswordHash != hashedPassword)
            {
                return null;
            }

            return user;
        }
 
    }
}
