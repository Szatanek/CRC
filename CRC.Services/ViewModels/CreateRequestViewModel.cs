using System.ComponentModel.DataAnnotations;
using CRC.Repository.Enums;
using CRC.Services.ViewModels.Base;

namespace CRC.Services.ViewModels
{
    public class CreateRequestViewModel: BaseViewModel
    {

        [Required]
        public ServersEnum ServerName { get; set; }
        [Required]
        [StringLength(200)]
        public string ServerAddress { get; set; }
        [Required]
        public PermissionsEnum Permission { get; set; }
        public int UserId { get; set; }       
        [StringLength(500)]
        public string AdditionalInfo { get; set; }
    }
}
