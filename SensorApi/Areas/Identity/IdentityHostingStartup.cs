using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(SensorApi.Areas.Identity.IdentityHostingStartup))]
namespace SensorApi.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}