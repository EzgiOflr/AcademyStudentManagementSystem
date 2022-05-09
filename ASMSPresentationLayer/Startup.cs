using ASMSDataAccessLayer;
using ASMSEntityLayer.IdentityModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ASMSEntityLayer.Mappings;
using ASMSBusinessLayer.EmailService;

namespace ASMSPresentationLayer
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
            //Aspnet Core'un ConnectionString ba�lant�s� yapabilmesi i�in 
            //yap�land�rma servislerine dbContext nesnesini eklemesi gerekir.
            services.AddDbContext<MyContext>(options => options.UseSqlServer
            (Configuration.GetConnectionString("SqlConnection")));

            services.AddControllersWithViews();
            services.AddRazorPages();//sayfalama i�in
            services.AddMvc();
            services.AddSession(options
                => options.IdleTimeout = TimeSpan.FromSeconds(20));//oturum zaman�

            //********************************************************//
            services.AddIdentity<AppUser, AppRole>(options =>
             {
                 options.User.RequireUniqueEmail = true;
                 options.Password.RequiredLength = 6;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = false;
                 options.Password.RequireDigit = false;
                 options.User.AllowedUserNameCharacters = 
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
             }).AddDefaultTokenProviders().AddEntityFrameworkStores<MyContext>();

            //Mapleme eklendi
            services.AddAutoMapper(typeof(Maps));

            services.AddScoped<IEmailSender, EmailSender>();
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
            app.UseStaticFiles(); //wwwroot klas�r�n�n eri�imi i�indir.(statik olan hersey)
                                  //sabit dosyalar�n hepsi wwwrootun i�ine birak�l�r.
                                  //web config yok hrsey program cs ve startupda kullan�l�yor

            app.UseSession();//oturum mekanizmas�n�n kullan�labilmesi i�in.
            app.UseRouting(); // controller/action/�d k�sm�

            app.UseAuthorization();//[authorize] attributesi i�in (yetki i�in)

            app.UseAuthentication(); // login logout i�lemlerinin gerektirti�i oturum i�leyi�lerini kullanabilmek icin
                                     // (kimlik dogrulamas�)
            //MVC ile ayn� kod blo�u endpoint'in mekanizmas�n�n nas�l olaca�� belirleniyor.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}