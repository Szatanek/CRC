using AutoMapper;
using CRC.Repository.Abstract;
using CRC.Repository.Models;
using CRC.Services.Abstract;
using CRC.Services.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace CRC.Services.Services
{
    public class PermissionService : IPermissionService
    {
      
        private readonly IGenericRepository<ProvisionedPermission> _permissionRepository;

        public PermissionService(IGenericRepository<ProvisionedPermission> permissionRepository)
        {            
            _permissionRepository = permissionRepository;
        }       

        public IEnumerable<ProvisionedPermissionViewModel> GetMyPermissions(int userId)
        {
            var requests = _permissionRepository.GetAll().Where(r => r.UserId == userId);
            return Mapper.Map<IEnumerable<ProvisionedPermission>, IEnumerable<ProvisionedPermissionViewModel>>(requests);
        }       
    }
}
