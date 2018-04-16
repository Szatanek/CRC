using System;
using System.Collections.Generic;
using System.Linq;
using CRC.Repository.Enums;
using Microsoft.AspNetCore.Mvc;
using CRC.Services.Abstract;
using CRC.Services.ViewModels;

namespace CRC.Controllers
{
    [Route("api/permission/")]
    public class PermissionController : Controller
    {
        private readonly IPermissionService _requestService;

        public PermissionController(IPermissionService requestService)
        {
            _requestService = requestService;
        }
     
        [HttpGet("getMyPermissions/{userId}")]
        public IEnumerable<ProvisionedPermissionViewModel> GetMyPermissions(int userId)
        {
            return _requestService.GetMyPermissions(userId);
        }

        [HttpGet("getPermissionsTypes")]
        public List<string> GetPermissionsTypes()
        {
            return Enum.GetNames(typeof(PermissionsEnum)).ToList();           
        }

        [HttpGet("getServerTypes")]
        public List<string> GetServerTypes()
        {
            return Enum.GetNames(typeof(ServersEnum)).ToList();         
        }
    }
}