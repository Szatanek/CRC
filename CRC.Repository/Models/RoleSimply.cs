using System.Collections.Generic;
using CRC.Repository.Models.Base;

namespace CRC.Repository.Models
{
    public class RoleSimply : Entity
    {
            public string Login { get; set; }
            public IEnumerable<Role> Roles { get; set; }
    }
}
