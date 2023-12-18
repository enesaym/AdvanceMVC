using AdvanceUI.ConnectAPI;
using AdvanceUI.Models.DTO.Advance;
using AdvanceUI.Models.Validation.Advance;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvanceUI
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
			services.AddControllersWithViews();
            services.AddHttpClient<TokenService>(conf =>
            {
                //conf.BaseAddress = new Uri("http://localhost:5000");
                conf.BaseAddress = new Uri(Configuration["myBaseUri"]);
            });
            services.AddAuthentication(a =>
            {
				//a.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				//a.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				a.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(a =>
            {
                a.LoginPath = "/Auth/Login";
                a.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
                a.Cookie.HttpOnly = true;
            });
			services.AddAuthorization();
			services.AddScoped<GenericService>();

            services.AddFluentValidationAutoValidation();
            services.AddScoped<IValidator<AdvanceInsertDTO>, AdvanceInsertDTOValidator>();
            //services.AddSession(x=>x.IdleTimeout=TimeSpan.FromSeconds(2));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			//app.UseSession();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Auth}/{action=Login}/{id?}");
			});
		}
	}
}
