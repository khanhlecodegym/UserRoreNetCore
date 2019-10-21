using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyMVC.ConnectDB;
using CompanyMVC.Repositories;
using CompanyMVC.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyMVC
{
    public class Startup
    {
        private readonly IHostingEnvironment env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<IAttendeeRepository, AttendeeRepository>();
            services.AddScoped<IConferenceRepository, ConferenceRepository>();
            services.AddScoped<IProposalRepository, ProposalRepository>();
            //services.AddSingleton<ConfArchUser>();

            services.AddDbContextPool<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CompanyMVC")));

            services.AddIdentity<ConfArchUser, IdentityRole>(options =>
                options.Password.RequireNonAlphanumeric = false)
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddTransient<IUserClaimsPrincipalFactory<ConfArchUser>, ConfArchUserClaimsPrincipalFactory>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("OrganizerAccessPolicy", policy => policy.RequireRole("organizer"));

                options.AddPolicy("SpeakerAccessPolicy",
                    policy => policy.RequireAssertion(context => context.User.IsInRole("speaker")));

                //options.AddPolicy("YearsOfExperiencePolicy",
                //    policy => policy.AddRequirements(new YearsOfExperienceRequirement(6)));

                //options.AddPolicy("ProposalEditPolicy",
                //    policy => policy.AddRequirements(new ProposalRequirement(false)));
            });

            if (!env.IsDevelopment())
            {
                services.Configure<MvcOptions>(option => option.Filters.Add(new RequireHttpsAttribute()));
            }

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Conference}/{action=Index}/{id?}");
            });
        }
    }
}
