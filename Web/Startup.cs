using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using Repository.DAL;

namespace Web
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
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            services.AddDbContext<Context>
                (options => options.UseSqlServer(Configuration.GetConnectionString("RW")));

            services.AddScoped<UserDAO>();

			// Session configuration need be set before MVC config
			services.AddSession();
			services.AddDistributedMemoryCache();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.LoginPath = PathString.FromUriComponent("/User/Login");
				});

			services.AddAuthorization(options =>
			{
				options.AddPolicy(Permission.IssueAccept.ToString(), 
					p => p.RequireClaim(Permission.IssueAccept.ToString()));

				options.AddPolicy(Permission.IssueClose.ToString(),
					p => p.RequireClaim(Permission.IssueClose.ToString()));

				options.AddPolicy(Permission.IssueCreate.ToString(),
					p => p.RequireClaim(Permission.IssueCreate.ToString()));

				options.AddPolicy(Permission.IssueRateAssistence.ToString(),
					p => p.RequireClaim(Permission.IssueRateAssistence.ToString()));

				options.AddPolicy(Permission.ManageAccounts.ToString(),
					p => p.RequireClaim(Permission.ManageAccounts.ToString()));

				options.AddPolicy(Permission.ManageGroups.ToString(),
					p => p.RequireClaim(Permission.ManageGroups.ToString()));

				options.AddPolicy(Permission.ManageCategories.ToString(),
					p => p.RequireClaim(Permission.ManageCategories.ToString()));
			});
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
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
			app.UseSession();
			app.UseAuthentication();

			app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
