using Bookstore.Models;
using Bookstore.Models.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Bookstore
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Enable MVC( architectural pattern ) Dependencies

            services.AddMvc(option => option.EnableEndpointRouting = false);

            /* In Memory Collection Dependencies 
            // Set singleton Design ( singleton design pattern instanciate one copy of object )
            services.AddSingleton<IBookstoreRepositories<Author>, AuthorRepository>();
            services.AddSingleton<IBookstoreRepositories<Book>, BookRepository>();
            */

            // Database Depandcies    
            // change singleton to add scoped beacuse we deal with databases we deal with more copies
            services.AddScoped<IBookstoreRepositories<Book> , BookDbRepository>();
            services.AddScoped<IBookstoreRepositories<Author>, AuthorDbRepository>();


            services.AddDbContext<BookstoreDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlCon"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(); // enable wwwroot folder 

            app.UseMvcWithDefaultRoute(); //  url ../author/index
            app.UseMvc(route => {
                route.MapRoute("defualt", "{controller=Book}/{action=index}/{id?}");
            });
         }
    }
}
