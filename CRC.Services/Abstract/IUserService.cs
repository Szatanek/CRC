using CRC.Services.ViewModels;
using System.Collections.Generic;
using CRC.Repository.Models;

namespace CRC.Services.Abstract
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> GetAllUsers();
        UserViewModel GetUserByLogin(string login);
        UserViewModel GetUserById(int id);
        int LogInUser(string name, string password);
        bool LogOutUser(int id);
        void DeleteUser(int id);
    }
}
