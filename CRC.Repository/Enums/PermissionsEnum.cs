using System.ComponentModel;

namespace CRC.Repository.Enums
{
    public enum PermissionsEnum
    {
        [Description("Read only")]
        ReadOnly,
        [Description("Admin")]
        Admin,
        [Description("HPA")]
        Hpa
    }
}
