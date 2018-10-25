using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Mas.Domain.Common;
using Mas.Domain.UserCenter;
using Mas.Grant.UserCenter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UserCenter
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var connection = Configuration.GetConnectionString("MSSql_User");
            services.AddDbContext<ReadDbContext>(options =>
                options.UseSqlServer(connection, b => b.MigrationsAssembly("Mas.Domain.UserCenter")));
            services.AddMvc();

            //services.ConfigureMvcServices();
            //services.ConfigureMvcClientAuthServices("{0}_charge_client", Configuration.GetConnectionString("SqlServer"));

            services.AddIdentityServer()
              .AddInMemoryClients(Clients.Get())
              .AddInMemoryIdentityResources(Mas.Grant.UserCenter.Resources.GetIdentityResources())
              .AddInMemoryApiResources(Mas.Grant.UserCenter.Resources.GetApiResources())
              .AddTestUsers(Users.Get())
              .AddDeveloperSigningCredential();




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseIdentityServer();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
