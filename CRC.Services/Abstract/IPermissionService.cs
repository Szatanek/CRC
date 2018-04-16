using System.Collections.Generic;
using CRC.Services.ViewModels;

namespace CRC.Services.Abstract
{
    public interface IPermissionService
    {
        IEnumerable<ProvisionedPermissionViewModel> GetMyPermissions(int userId);
    }
}
