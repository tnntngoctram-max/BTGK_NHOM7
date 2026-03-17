using BTGK_NHOM7.Configuration;
using BTGK_NHOM7.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BTGK_NHOM7.EntityFrameworkCore;

/* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
public class BTGK_NHOM7DbContextFactory : IDesignTimeDbContextFactory<BTGK_NHOM7DbContext>
{
    public BTGK_NHOM7DbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<BTGK_NHOM7DbContext>();

        /*
         You can provide an environmentName parameter to the AppConfigurations.Get method. 
         In this case, AppConfigurations will try to read appsettings.{environmentName}.json.
         Use Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") method or from string[] args to get environment if necessary.
         https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli#args
         */
        var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

        BTGK_NHOM7DbContextConfigurer.Configure(builder, configuration.GetConnectionString(BTGK_NHOM7Consts.ConnectionStringName));

        return new BTGK_NHOM7DbContext(builder.Options);
    }
}
