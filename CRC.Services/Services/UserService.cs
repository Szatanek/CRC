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
    public class UserService: IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<RoleSimply> _roleRepository;

        public UserService(IGenericRepository<User> userRepository,
            IGenericRepository<RoleSimply> roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;

        }
        public IEnumerable<UserViewModel> GetAllUsers()
        {
           var users = _userRepository.GetAll();
           return Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);            
        }

        public UserViewModel GetUserByLogin(string login)
        {
           var user = _userRepository.FindBy(u => u.Login == login).FirstOrDefault();
           return Mapper.Map<UserViewModel>(user);          
        }

        public UserViewModel GetUserById(int id)
        {
            var user = _userRepository.FindBy(u => u.Id == id).FirstOrDefault();
            return Mapper.Map<UserViewModel>(user);
        }

        public int LogInUser(string login, string password)
        {
            var user = _userRepository
                               .FindBy(u => u.Login == login && u.Password == password) 
                               .FirstOrDefault();
            if (user != null)
            {
                user.IsLogin = true;
                _userRepository.Edit(user);               
                return user.Id;
            }

            return 0;
        }

        public bool LogOutUser(int id)
        {
            var user = _userRepository
                               .FindBy(u => u.Id == id )
                               .FirstOrDefault();
            if (user != null)
            {
                user.IsLogin = false;
                _userRepository.Edit(user);               
                return true;
            }

            return false;
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(_userRepository.GetById(id));            
        }
    }
}
