using System.Linq;
using EinTech.Api.DAL;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EinTech.Api.Integration.Tests.Helpers
{
    public class TestingWebAppFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {

                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApiContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<ApiContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryTest");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                services.AddAntiforgery(t =>
                {
                    t.Cookie.Name = AntiForgeryTokenExtractor.AntiForgeryCookieName;
                    t.FormFieldName = AntiForgeryTokenExtractor.AntiForgeryFieldName;
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                using var appContext = scope.ServiceProvider.GetRequiredService<ApiContext>();
                appContext.Database.EnsureCreated();
            });
        }
    }
}