using CRC.Repository.Models.Base;

namespace CRC.Repository.Models
{
    public class User: Entity
    {       
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsLogin { get; set; }
    }
}
