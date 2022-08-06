using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using RoadRecordViolationSystem.Data;
using RoadRecordViolationSystem.Models;
using RoadRecordViolationSystem.Services;
using RoadRecordViolationSystem.Services.Implementations;
using RoadRecordViolationSystem.Services.Interfaces;
using RoadRecordViolationSystem.Utils.Implementation;
using RoadRecordViolationSystem.Utils.Interface;
using RoadRecordViolationSystem.ViewModels;
using System;
using System.Net;

namespace RoadRecordViolationSystem
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
      //Newtonsoft JSon service
      services.AddControllersWithViews()
        .AddNewtonsoftJson(options => {
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });

       services.AddSession(opt => opt.IdleTimeout = TimeSpan.FromMinutes(120))
               .AddHttpContextAccessor();
            var username = "YOUR USERNAME HERE";
            var password = "YOUR PASSWORD HERE";

            services.AddFluentEmail("")
                    .AddRazorRenderer()
                    
                    .AddSmtpSender(new System.Net.Mail.SmtpClient("smtp.gmail.com")
                    {
                        UseDefaultCredentials = false,
                        Port = 587,
                        EnableSsl = true,
                        Credentials = new NetworkCredential(username,password)
                    });

            // Our Database Context
            services.AddDbContext<AppDBContext>(options =>
      options.UseSqlServer(Configuration.GetConnectionString("RoadRecordViolationConnection")));


      // ASP.NET CORE Identity Services
      services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<AppDBContext>()
        .AddDefaultTokenProviders();


      // Fluent Validation Service
      services.AddFluentValidation(options =>
      {
        options.DisableDataAnnotationsValidation = true;
        options.RegisterValidatorsFromAssemblyContaining<Startup>();
      });

    //api enabling cors
    services.AddCors();


      // Repository Services
      services.AddScoped<ILogProvider<LoginViewModel>, IdentityLogin>();
      services.AddScoped<IAuthorizedProvider<ApplicationUser>, IdentityAuthorize>();
      services.AddScoped<IRoleProvider<ApplicationUser>, IdentityRoleService>();
      services.AddScoped<ICrudRepository<UsersInformation>, UserInformationService>();
      services.AddScoped<IViolationTypesRepository<ViolationTypes>, ViolationTypesService>();
      services.AddScoped<ILicenseDetailsRepository<LicenseDetails>, LicenseDetailsService>();
      services.AddScoped<IViolatorRepository<Violator>, ViolatorService>();
      services.AddScoped<IViolationRepository<Violations>, ViolationService>();
      services.AddScoped<IInvolveRepository<AccidentInvolve>, AccidentInvolveService>();
      services.AddScoped<ICrudRepository<Accident>, AccidentService>();
      services.AddScoped<IAddRangeSupport<Violations>, ViolationService>();
      services.AddScoped<IFileManager, FileManager>();
      services.AddScoped<IlogHistory, LogHistory>();
      services.AddScoped<ILogHistoryProvider<UsersLogHistory>, LogHistoryService>();
      services.AddScoped<IUserOptions, Services.Implementations.UserOptions>();
      services.AddScoped<ICrudRepository<VehicleInformation>, VehicleInformationService>();
      services.AddScoped<ICrudRepository<Settings>, SettingService>();
      services.AddScoped<ITicketRepository<Contest>, ContestService>();
      services.AddScoped<ITicketRepository<ViolatorTicketInformation>, TicketInformationService>();
      services.AddScoped<IEmail, FluentEmailUtil>();
      services.AddScoped<ICrudRepository<ContestSchedule>, ContestScheduleService>();
      services.AddScoped<IOfficerInvolveRepository<OfficerInvolve>, OfficerInvolveService>();
       
    
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
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      app.UseSession();
      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseEndpoints(endpoints => 
      {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Log}/{action=SystemLogin}/{id?}"
        );
      });

      
    }
  }
}
