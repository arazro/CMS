using Microsoft.Extensions.Configuration;
using System.IO;


namespace UserService.Tests.Helpers
{
    public static class TestConfigurationBuilder
    {
        public static IConfiguration BuildConfiguration()
        {
            var basePath = Directory.GetCurrentDirectory();
            return new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                .Build();
        }
    }
}
