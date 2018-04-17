using System;
using CRC.Repository.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;
using CRC.Repository.Enums;

namespace CRC.Repository.Models
{
    public class Request: Entity
    {       
        public ServersEnum ServerName { get; set; }
        public string ServerAddress { get; set; }
        public PermissionsEnum Permission { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; } //zmienilem tu Login na User      
        public StatusEnum Status { get; set;  }
        public string AdditionalInfo { get; set; }
        public DateTime RequestedAt { get; set; }
        public string Reason { get; set; }
    }
}
