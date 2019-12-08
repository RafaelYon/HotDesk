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
using Web.Helpers;

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
				(options => options
					.UseSqlServer(Configuration.GetConnectionString("RW"))
					.UseLazyLoadingProxies());

            services.AddScoped<AuthUser>();
            services.AddScoped<UserDAO>();
			services.AddScoped<CategoryDAO>();
			services.AddScoped<GroupDAO>();
            services.AddScoped<IssueDAO>();

			services.AddScoped<UserRepository>();

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
				options.AddPolicy(PermissionType.IssueAccept.ToString(), 
					p => p.RequireClaim(PermissionType.IssueAccept.ToString()));

				options.AddPolicy(PermissionType.IssueClose.ToString(),
					p => p.RequireClaim(PermissionType.IssueClose.ToString()));

				options.AddPolicy(PermissionType.IssueCreate.ToString(),
					p => p.RequireClaim(PermissionType.IssueCreate.ToString()));

				options.AddPolicy(PermissionType.IssueRateAssistence.ToString(),
					p => p.RequireClaim(PermissionType.IssueRateAssistence.ToString()));

				options.AddPolicy(PermissionType.ManageAccounts.ToString(),
					p => p.RequireClaim(PermissionType.ManageAccounts.ToString()));

				options.AddPolicy(PermissionType.ManageGroups.ToString(),
					p => p.RequireClaim(PermissionType.ManageGroups.ToString()));

				options.AddPolicy(PermissionType.ManageCategories.ToString(),
					p => p.RequireClaim(PermissionType.ManageCategories.ToString()));
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
