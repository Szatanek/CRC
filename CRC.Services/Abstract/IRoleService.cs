using CRC.Repository.Models;

namespace CRC.Services.Abstract
{
    public interface IRoleService
    {
        RoleSimply GetUserRoles(string login);
    }
}