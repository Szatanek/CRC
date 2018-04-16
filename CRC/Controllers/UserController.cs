using System.Collections.Generic;
using CRC.Repository.Models;
using CRC.Services.Abstract;
using CRC.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CRC.Controllers
{
    [Route("api/user/")]
    public class UserController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public UserController(IUserService userService,
            IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet("getAllUsers")]
        public IEnumerable<UserViewModel> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }


        [HttpPut("userLoginChange/{id}")]
        public IActionResult UserLoginChange(int id)
        {
            return Ok();
        }

        [HttpGet("getUserByName/{name}")]
        public UserViewModel GetUserByLogin(string login)
        {
            return
                _userService
                    .GetUserByLogin(
                        login); //pytanie czy tu szukać po name czy po login? A waściwie to czemu nie po id?? :-)    
        }

        [HttpGet("GetUserById/{id}")]
        public UserViewModel GetUserById(int id)
        {
            return _userService.GetUserById(id);
        }

        [HttpGet("logInUser/{login}/{password}")]
        public int LogInUser(string login, string password)
        {
            return _userService.LogInUser(login, password);
        }

        [HttpGet("UserLogged/{id}")]
        public UserViewModel UserLogged(int id)
        {
            return _userService.GetUserById(id);
        }

        [HttpGet("logOutUser/{id}")]
        public IActionResult LogOutUser(int id)
        {
            if (_userService.LogOutUser(id))
                return Ok();

            return NotFound();
        }

        [HttpDelete("DeleteUser/{id}")]
        public void Delete(int id)
        {
            _userService.DeleteUser(id);
        }

        [HttpGet("GetUserRoles/{login}")]
        public RoleSimply GetUserRoles(string login)
        {
            return _roleService.GetUserRoles(login);
        }
    }
}