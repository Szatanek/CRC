using System.ComponentModel;

namespace CRC.Repository.Enums
{
    public enum ServersEnum
    {
        [Description("Development")]
        Dev,
        [Description("Test")]
        Tst,
        [Description("Acceptance")]
        Acc,
        [Description("Production")]
        Prd
    }
}
