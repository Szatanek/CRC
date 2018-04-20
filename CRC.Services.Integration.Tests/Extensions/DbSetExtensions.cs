using Microsoft.EntityFrameworkCore;

namespace CRC.Services.Integration.Tests.Extensions
{
    public static class DbSetExtensions
    {
        public static void Clear<T>(this DbSet<T> set) 
            where T : class
        {
            set.RemoveRange(set);
        }
    }
}
