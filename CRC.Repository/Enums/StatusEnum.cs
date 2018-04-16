using System.ComponentModel;

namespace CRC.Repository.Enums
{
    public enum StatusEnum
    {
        [Description("In progress")]
        InProgress,
        [Description("Approved")]
        Approved,
        [Description("Rejected")]
        Rejected,       
    }
}
