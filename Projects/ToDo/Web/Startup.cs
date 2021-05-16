using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Core.Interfaces;
using Infrastructure.Repository;
using Core.Interfaces.FileManager;
using Infrastructure.FileManager;

namespace Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var connString = Configuration.GetConnectionString("SqlConn");
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(connString)
                );

            services.AddDefaultIdentity<ApplicationUser>(options =>
                options.SignIn.RequireConfirmedAccount = true
            ).AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                "IsAdmin",
                policyBuilder => policyBuilder
                .RequireClaim("Admin"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IFileManager, FileManager>();

            services.AddControllersWithViews();

            services.AddRazorPages();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "defualt",
                    pattern: "{Controller=Home}/{action=index}/{id?}"
                    );
                endpoints.MapRazorPages();
            });

        }
    }
}
