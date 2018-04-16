using CRC.Services.ViewModels.Base;

namespace CRC.Services.ViewModels
{
    public class ReadRequestViewModel : BaseViewModel
    {       
        public string ServerName { get; set; }        
        public string ServerAddress { get; set; }      
        public string Permission { get; set; }      
        public string Status { get; set; }      
        public string AdditionalInfo { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } // to jest nazwa usera - koledzy z frontu to maja fantazje...
    }
}
