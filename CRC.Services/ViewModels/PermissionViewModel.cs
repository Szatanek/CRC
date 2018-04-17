using CRC.Services.ViewModels.Base;

namespace CRC.Services.ViewModels
{
    public class ProvisionedPermissionViewModel : BaseViewModel
    {       
        public string ServerName { get; set; }       
        public string ServerAddress { get; set; }       
        public string Permission { get; set; }
        public int UserId { get; set; }            
        public string AdditionalInfo { get; set; }
        public string ApprovedAt { get; set; }
    }
}
