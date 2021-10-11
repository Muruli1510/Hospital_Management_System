using Entity_Model_Layer.Models;
using HMS_Repository_Layer;
using HMSBAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_Management_System
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
            services.AddMvc();
            //services.AddSwaggerGen();
            services.AddDbContext<Hosptial_Management_SystemContext>();
            services.AddScoped(typeof(Igeneric<>), typeof(Generic_Repo<>));
            services.AddScoped<HMSServicesPat>();
            services.AddScoped<HMSServicesDoc>();
            services.AddScoped<HMSServicesInPatient>();
            services.AddScoped<HMSServicesRoomData>();
            services.AddScoped<HMSServicesOutPatient>();
            services.AddScoped<HMSServicesLab>();
            services.AddScoped<HMSServicesBillData>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
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
            //app.UseSwagger();
            //app.UseSwaggerUI(op =>
            //{
            //    op.SwaggerEndpoint("/swagger/v1/swagger.json", "Hospital Management System APIs");
            //});
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=UI}/{action=showpatientdetails}/{id?}");
                  
            });
        }
    }
}
