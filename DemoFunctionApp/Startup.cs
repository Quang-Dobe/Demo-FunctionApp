using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

[assembly: FunctionsStartup(typeof(DemoFunctionApp.Startup))]
namespace DemoFunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var keyVaultUrl = new Uri(Environment.GetEnvironmentVariable(""));
            var secretClient = new SecretClient(keyVaultUrl, new DefaultAzureCredential());
            var connectionString = secretClient.GetSecret("sql").Value.Value;

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
