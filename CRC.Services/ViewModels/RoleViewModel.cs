using System.Collections.Generic;
using CRC.Repository.Models;
using CRC.Services.ViewModels.Base;

namespace CRC.Services.ViewModels
{
    public class RoleViewModel: BaseViewModel
    {
        public string Login { get; set; }
        public IEnumerable<Role> Roles { get; set; }


}
}
