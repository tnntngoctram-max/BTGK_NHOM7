using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace BTGK_NHOM7.EntityFrameworkCore;

public static class BTGK_NHOM7DbContextConfigurer
{
    public static void Configure(DbContextOptionsBuilder<BTGK_NHOM7DbContext> builder, string connectionString)
    {
        builder.UseSqlServer(connectionString);
    }

    public static void Configure(DbContextOptionsBuilder<BTGK_NHOM7DbContext> builder, DbConnection connection)
    {
        builder.UseSqlServer(connection);
    }
}
