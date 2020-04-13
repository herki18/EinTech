using AutoMapper;
using EinTech.Api.Contracts.Interfaces.Repositories;
using EinTech.Api.Contracts.Mappers;
using EinTech.Api.DAL;
using EinTech.Api.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EinTech
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddDbContext<ApiContext>(
                opt => opt.UseSqlite(Configuration.GetConnectionString("DatabaseConnection")));

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                {
                    mc.AddProfile(new MappingProfiles());
                }
            });

            // Set Adapter pattern
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApiContext apiContext)
        {
            if (env.IsDevelopment())
            {
                apiContext.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                apiContext.Database.Migrate();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Persons}/{action=Index}/{id?}");
            });
        }
    }
}
