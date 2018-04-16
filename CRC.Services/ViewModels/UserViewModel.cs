using CRC.Services.ViewModels.Base;

namespace CRC.Services.ViewModels
{
    public class UserViewModel: BaseViewModel
    {
        public string Name { get; set; }
        public string Login { get; set; }       
        public bool IsLogin { get; set; }
    }
}
