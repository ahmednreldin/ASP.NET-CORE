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

namespace Bookstore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Enable MVC( architectural pattern ) Dependencies

            services.AddMvc(option => option.EnableEndpointRouting = false);



            // Set singleton Design ( singleton design pattern instanciate one copy of object )
            services.AddSingleton<IBookstoreRepositories<Author>, AuthorRepository>();
            services.AddSingleton<IBookstoreRepositories<Book>, BookRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute(); //  url ../author/index

         }
    }
}
