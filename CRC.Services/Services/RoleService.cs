using AutoMapper;
using CRC.Repository.Abstract;
using CRC.Repository.Models;
using CRC.Services.Abstract;
using CRC.Services.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CRC.Services.Services
{
    public class RoleService: IRoleService
    {
        private readonly IGenericRepository<RoleSimply> _roleRepository;

        public RoleService(IGenericRepository<RoleSimply> roleRepository)
        {
            _roleRepository = roleRepository;

        }

        public RoleSimply GetUserRoles(string login)
        {
            return _roleRepository.GetAll().Include(y => y.Roles).FirstOrDefault(x => x.Login == login);
        }
    }
}
