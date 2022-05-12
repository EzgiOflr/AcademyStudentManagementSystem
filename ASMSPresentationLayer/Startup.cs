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
using ASMSBusinessLayer.ContractsBLL;
using ASMSBusinessLayer.ImplementationsBLL;

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
            //Aspnet Core'un ConnectionString baðlantýsý yapabilmesi için 
            //yapýlandýrma servislerine dbContext nesnesini eklemesi gerekir.
            services.AddDbContext<MyContext>(options => options.UseSqlServer
            (Configuration.GetConnectionString("SqlConnection"))); 
            //lifetime larý farklý oldugu icin database ve unitof workun ayný hala getirdik yoksa hata veriyordu.
            //servicelifetime.scoped
            //context scoped oldu unit of work de scoped ona baðlý  business engine'in da unitof worke baglý bu yüzden üçü de 
            //scoped yaptýk.
            //yaþam þekilleri ayný deðilse hata verir. karsýlastýgýmýz gibi. second operation mistake.

            services.AddControllersWithViews();
            services.AddRazorPages();//sayfalama için
            services.AddMvc();
            services.AddSession(options
                => options.IdleTimeout = TimeSpan.FromSeconds(20));//oturum zamaný

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

            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddScoped<IStudentBusinessEngine, StudentBusinessEngine>();
            services.AddScoped<IUsersAddressBusinessEngine, UsersAddressBusinessEngine>();
            services.AddScoped<ASMSDataAccessLayer.ContractsDAL.IUnitOfWork, ASMSDataAccessLayer.ImplementationsDAL.UnitOfWork>();

        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env , RoleManager<AppRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles(); //wwwroot klasörünün eriþimi içindir.(statik olan hersey)
                                  //sabit dosyalarýn hepsi wwwrootun içine birakýlýr.
                                  //web config yok hrsey program cs ve startupda kullanýlýyor

            app.UseSession();//oturum mekanizmasýnýn kullanýlabilmesi için.
            app.UseRouting(); // controller/action/ýd kýsmý
            app.UseAuthentication(); // login logout iþlemlerinin gerektirtiði oturum iþleyiþlerini kullanabilmek icin
                                     // (kimlik dogrulamasý)
                                     //MVC ile ayný kod bloðu endpoint'in mekanizmasýnýn nasýl olacaðý belirleniyor.
                                     //rolleri olusturacak static metot caðýrýldý.
            app.UseAuthorization();//[authorize] attributesi için (yetki için)

            CreateDefaultData.CreateData.Create(roleManager);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}